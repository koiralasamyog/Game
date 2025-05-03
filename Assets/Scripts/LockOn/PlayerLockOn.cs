using UnityEngine;

public class PlayerLockOn : MonoBehaviour
{
    public Transform target;
    public float rotationSpeed = 5f;
    public bool isLockedOn = false;

    void Update()
    {
        if (isLockedOn && target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            direction.y = 0; // Keep only horizontal rotation
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
        }
    }
}
