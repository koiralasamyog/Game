using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class ManageMusic : MonoBehaviour
{
    public Animator animator;
    public AudioSource audioSource;

    public void EnableMusic()
    {
        if (animator.GetBool("isChasing"))
        {
            audioSource.enabled = true;
        }
        else
        {
            audioSource.enabled = false;
        }
    }
}
