using System.Collections;
using UnityEngine;

public class FootstepSound : MonoBehaviour
{
    [SerializeField] private AudioClip[] footstepSounds;  // Array of footstep sounds
    private AudioSource audioSource;
    private Vector3 lastPosition;  // To track the last position of the character

    [SerializeField] private float stepInterval = 0.5f;  // Time interval between each footstep sound
    private float timeSinceLastStep = 0f;  // Timer to control when to play the next footstep

    private void Start()
    {
        // Initialize necessary components
        audioSource = GetComponent<AudioSource>();
        lastPosition = transform.position;  // Set the initial position of the character
    }

    private void Update()
    {
        // Check if the player is moving (by comparing the current position with the last position)
        if (Vector3.Distance(lastPosition, transform.position) > 0.1f)
        {
            // If the player is moving, increment the time since last step
            timeSinceLastStep += Time.deltaTime;

            // Only play a footstep sound if enough time has passed (stepInterval)
            if (timeSinceLastStep >= stepInterval)
            {
                PlayFootstepSound();
                timeSinceLastStep = 0f;  // Reset the timer after playing the sound
            }

            lastPosition = transform.position;  // Update the last position to current position
        }
    }

    private void PlayFootstepSound()
    {
        if (footstepSounds.Length == 0) return;  // Ensure there are footstep sounds to play

        // Pick a random footstep sound from the array
        AudioClip clip = footstepSounds[Random.Range(0, footstepSounds.Length)];

        // Play the footstep sound
        audioSource.PlayOneShot(clip);
    }
}
