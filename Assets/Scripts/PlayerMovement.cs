using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Variables ------------------------------
    [SerializeField] private GameController gameController;
    [Space]
    [SerializeField] private CharacterController2D characterController2D;
    [SerializeField] private float runSpeed = 40f;
    [SerializeField] private Animator animator;
    private bool climb = false;
    private float horizontalMove = 0;

    #endregion

    #region Private Methods -----------------------------
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "OutOfBounds")
        {
            gameController.ResetCurrentLevel();
        }
    }

    #endregion

    #region Public Methods ----------------------------------------
    public void ResetPlayer(Vector2 resetPos)
    {
        characterController2D.Move(0, false, false, false);
        this.transform.localPosition = resetPos;
    }
    #endregion
}
