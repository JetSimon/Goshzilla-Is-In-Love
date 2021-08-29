using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    private Animator animator;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    public float playerSpeed = 2.0f;
    private float gravityValue = -9.81f * 10;
    private Camera cam;

    void Start()
    {
        cam = Camera.main;
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        groundedPlayer = controller.isGrounded;

        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }
        
        Vector3 move = Vector3.zero;

        if(!GameManager.gameManager.inUI)
        {
            move = Input.GetAxis("Horizontal") * cam.transform.right + Input.GetAxis("Vertical") * cam.transform.forward;
            controller.Move(move * Time.deltaTime * playerSpeed);
        }

        animator.SetBool("Running", move != Vector3.zero);

        if (move != Vector3.zero)
        {
            GameManager.gameManager.PlaySound("walk", Random.Range(0.8f,1.2f));
            gameObject.transform.forward = move;
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;

        InactiveUntilPlayerTouches detector = hit.gameObject.GetComponent<InactiveUntilPlayerTouches>();

        // no rigidbody
        if (detector == null || body == null || !body.isKinematic)
            return;

        detector.Activate();

        
    }
}


