using UnityEngine;

public class LockOnSystem : MonoBehaviour
{
    public Transform player;
    public float rotationSpeed = 5f;
    public float lockOnRange = 15f;

    private Transform target;
    private bool isLockedOn = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (!isLockedOn)
            {
                target = FindClosestEnemy();
                isLockedOn = target != null;
            }
            else
            {
                isLockedOn = false;
                target = null;
            }
        }

        if (isLockedOn && target != null)
        {
            // Rotate camera to look at the target
            Vector3 camDirection = (target.position - transform.position).normalized;
            Quaternion camRotation = Quaternion.LookRotation(camDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, camRotation, Time.deltaTime * rotationSpeed);

            // Rotate player to face the target
            Vector3 playerDir = (target.position - player.position).normalized;
            playerDir.y = 0; // Keep rotation horizontal
            Quaternion playerRot = Quaternion.LookRotation(playerDir);
            player.rotation = Quaternion.Slerp(player.rotation, playerRot, Time.deltaTime * rotationSpeed);
        }
    }

    Transform FindClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Boss");
        Transform closest = null;
        float minDist = lockOnRange;

        foreach (GameObject enemy in enemies)
        {
            float dist = Vector3.Distance(player.position, enemy.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                closest = enemy.transform;
            }
        }

        return closest;
    }
}
