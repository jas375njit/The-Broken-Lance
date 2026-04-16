using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    float velocity = 0.0f;
    public float accelaration = 0.1f;
    int VelocityHash;

    private CharacterController controller;
    Animator myAnimator;

    void Start()
    {
        myAnimator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();

        if (controller == null)
            Debug.LogError("No CharacterController found on " + gameObject.name);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        VelocityHash = Animator.StringToHash("Velocity");
    }

    void Update()
    {
        var keyboard = Keyboard.current;
        if (keyboard == null) return;

        bool forwardPressed = keyboard.wKey.isPressed || keyboard.upArrowKey.isPressed;
        bool backwardPressed = keyboard.sKey.isPressed || keyboard.downArrowKey.isPressed;

        if (forwardPressed) 
        {
            velocity += 0.5f; 
        } 
        else 
        {
            velocity -= Time.deltaTime * accelaration;
        }

        velocity = Mathf.Clamp(velocity, 0.0f, 1.0f);

        myAnimator.SetFloat(VelocityHash, velocity);

        if (controller == null) return;

        float v = 0f;
        if (forwardPressed) v = 1f;
        else if (backwardPressed) v = -1f;

        Vector3 move = transform.forward * v;
        controller.Move(move * moveSpeed * Time.deltaTime);

        float yVelocity = -2f;
        if (controller.isGrounded)
        {
            if (keyboard.spaceKey.wasPressedThisFrame)
                yVelocity = Mathf.Sqrt(jumpForce * 2f * 9.81f);
        }

        yVelocity -= 9.81f * Time.deltaTime;
        controller.Move(Vector3.up * yVelocity * Time.deltaTime);

        if (keyboard.escapeKey.wasPressedThisFrame)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}