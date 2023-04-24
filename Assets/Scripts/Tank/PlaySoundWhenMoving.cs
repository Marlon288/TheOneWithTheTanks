using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundWhenMoving : MonoBehaviour
{
    private AudioSource audioSource;
    private Vector3 previousPosition;
    public float movementThreshold = 0.01f;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        previousPosition = transform.position;
    }

    void Update()
    {
        // Calculate the distance the object has moved since the last frame
        float distanceMoved = Vector3.Distance(transform.position, previousPosition);

        // If the object has moved more than the movement threshold, play the sound
        if (distanceMoved > movementThreshold){
            if (!audioSource.isPlaying){
                audioSource.Play();
            }
        }
        else{
            if (audioSource.isPlaying){
                audioSource.Stop();
            }
        }

        // Update the previous position for the next frame
        previousPosition = transform.position;
    }
}
