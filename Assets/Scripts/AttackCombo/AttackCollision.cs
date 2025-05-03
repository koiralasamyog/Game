using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollision : MonoBehaviour
{
    public int damageAmount = 20;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Boss")
        {
            other.GetComponent<DragonBoss>().TakeDamage(damageAmount);
            Debug.Log("hit");
        }
    }
}
