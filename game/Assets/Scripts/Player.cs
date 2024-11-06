using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5f;
    public float interactionDistance = 2f;

    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool isGrounded;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        MovePlayer();
        CheckInteraction();
    }

    void MovePlayer()
    {
        // Check if the player is grounded
        isGrounded = controller.isGrounded;
        if (isGrounded && playerVelocity.y < 0)
            playerVelocity.y = 0f;

        // Get input for movement along local forward and right directions
        float moveForward = Input.GetAxis("Vertical"); // W/S or Up/Down Arrow
        float moveRight = Input.GetAxis("Horizontal"); // A/D or Left/Right Arrow

        // Calculate movement direction based on player's facing direction
        Vector3 move = transform.forward * moveForward + transform.right * moveRight;
        controller.Move(move * Time.deltaTime * speed);

        // Apply gravity
        playerVelocity.y += Physics.gravity.y * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    void CheckInteraction()
    {
        if (Input.GetButtonDown("Interact"))
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, interactionDistance))
            {
                IInteractable interactable = hit.transform.GetComponent<IInteractable>();
                interactable?.Interact();
            }
        }
    }

    public void ResetPlayer(Vector3 resetPosition)
    {
        transform.position = resetPosition;
    }
}

public interface IInteractable
{
    void Interact();
}
