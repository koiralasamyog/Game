using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class IputManager : MonoBehaviour
{
    PlayerControls playerControls;
    PlayerLocomotion playerLocomotion;
    AnimatorManager animatorManager;

    public GameObject objectToEnable;
    public GameObject random;


    public Vector2 movementInput;
    public Vector2 cameraInput;

    public float cameraInputX;
    public float cameraInputY;

    public float moveAmount;
    public float verticalInput;
    public float horizontalInput;

    public bool b_input;
    public bool x_input;
    public bool a_input;
    public bool q_input;
    public bool e_input;
    public bool jump_Input;

    private void Awake()
    {
        animatorManager = GetComponent<AnimatorManager>(); 
        playerLocomotion = GetComponent<PlayerLocomotion>();
    }

    private void OnEnable()
    {
        if (playerControls == null)
        {
            playerControls = new PlayerControls();

            playerControls.PlayerMovement.Movement.performed += i=> movementInput = i.ReadValue<Vector2>();
            playerControls.PlayerMovement.Camera.performed += i=> cameraInput = i.ReadValue<Vector2>();

            playerControls.PlayerActions.Sprint.performed += i => b_input = true;
            playerControls.PlayerActions.Sprint.canceled += i => b_input = false;

            playerControls.PlayerActions.Jump.performed += i => jump_Input = true;
            
            playerControls.PlayerActions.Dodge.performed += i => x_input = true;

            playerControls.PlayerActions.Attack.performed += i => a_input = true;

            playerControls.PlayerActions.Skill.performed += i => q_input = true;
        }

        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }
    
    public void HandleAllInputs()
    {
        HandleMovementInput();
        HandleSprintingInput();
        HandleJumpInput();
        HandleDodgeInput();
        HandleAttackInput();
        HandleSkillInput();
  
    }
    private void HandleMovementInput()
    {
        verticalInput = movementInput.y;
        horizontalInput = movementInput.x;

        cameraInputX = cameraInput.x;
        cameraInputY = cameraInput.y;

        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput)+Mathf.Abs(verticalInput));
        animatorManager.UpdateAnimatorValues(0, moveAmount, playerLocomotion.isSprinting);
    }

    private void HandleSprintingInput()
    {
        if (b_input && moveAmount > 0.5f)
        {
            playerLocomotion.isSprinting = true;
        }
        else
        {
            playerLocomotion.isSprinting = false;
        }
    }

    private void HandleJumpInput()
    {
        if (jump_Input)
        {
            jump_Input = false;
            playerLocomotion.HandleJumping();
        }
    }
    private void HandleDodgeInput()
    {
        if(x_input)
        {
            x_input = false;
            playerLocomotion.HandleDodge();
        }
    }
    private void HandleAttackInput()
    {
        if(a_input)
        {
            
            a_input = false;
            if (objectToEnable != null)
            {
                objectToEnable.SetActive(true);
                Invoke(nameof(DisableEffect), 0.5f);
            }

            playerLocomotion.HandleAttack();
        }
    }

    private void HandleSkillInput()
    {
        if (q_input)
        {

            q_input = false;
            if (random != null)
            {
                random.SetActive(true);
                Invoke(nameof(DisableRandom), 4f);
            }

            playerLocomotion.HandleSkill();
        }
    }
    private void DisableEffect()
    {
        if (objectToEnable != null)
        {
            objectToEnable.SetActive(false);
        }
    }
    private void DisableRandom()
    {
        if (random != null)
        {
            random.SetActive(false);
        }
    }
}
