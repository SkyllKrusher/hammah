using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController2D characterController2D;
    [SerializeField] private float runSpeed = 40f;
    [SerializeField] private Animator animator;
    private bool climb = false;
    private float horizontalMove = 0;

    private void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (Input.GetButtonDown("Climb"))
        {
            climb = true;
            animator.SetBool("RotateHammer", true);
        }

        if (Input.GetButtonUp("Climb"))
        {
            climb = false;
            animator.SetBool("RotateHammer", false);
        }
        // Debug.Log(horizontalMove);
    }

    private void FixedUpdate()
    {
        characterController2D.Move(horizontalMove * Time.fixedDeltaTime, false, false, climb);
    }
}
