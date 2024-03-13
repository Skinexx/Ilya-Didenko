using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float turnSpeed = 20f;
    private Quaternion rotation = Quaternion.identity;
    private Vector3 movementDirection;
    private Animator animator;
    private Rigidbody rigidbody;
    private AudioSource audioSource;

    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    
    void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        movementDirection.Set(horizontalInput, 0, verticalInput);
        movementDirection.Normalize();

        bool hasHorizontalInput = !Mathf.Approximately(horizontalInput, 0f);
        bool hasVerticalInput = !Mathf.Approximately(verticalInput, 0f);
        bool isWalking = hasHorizontalInput || hasVerticalInput;

        animator.SetBool("IsWalking", isWalking);

        if (isWalking)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            audioSource.Stop();
        }

        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, movementDirection, turnSpeed * Time.deltaTime, 0);
        rotation = Quaternion.LookRotation(desiredForward);

    }

    private void OnAnimatorMove()
    {
        rigidbody.MovePosition(rigidbody.position + movementDirection * animator.deltaPosition.magnitude);
        rigidbody.MoveRotation(rotation);
    }

}
