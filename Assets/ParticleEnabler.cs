using UnityEngine;

public class ParticleEnabler : MonoBehaviour
{
    IputManager inputManager;

    public void Awake()
    {
        inputManager = GetComponent<IputManager>();
    }
    void Start()
    {
        
        // Replace "ChildObjectName" with the actual name of the child GameObject
        Transform child = transform.Find("Charge slash purple");

        if (inputManager.a_input)
        {
            child.gameObject.SetActive(true);
            Debug.Log("Activated");
        }
        else
        {
            Debug.LogWarning("Child not found!");
        }
    }
}
