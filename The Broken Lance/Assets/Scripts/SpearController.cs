using UnityEngine;
using UnityEngine.InputSystem;

public class SpearController : MonoBehaviour
{
    [Header("Spear")]
    public Transform spear;
    public float mouseSensitivity = 2f;
    public float verticalClamp = 45f;

    [Header("Strike")]
    public float strikeDistance = 1.5f;
    public float strikeSpeed = 10f;
    public float returnSpeed = 5f;

    [Header("Animator")]
    public Animator characterAnimator;
    public string strikeAnimationName = "GirlIdleMounted";

    private Camera cam;
    private float xRotation = 0f;
    private Vector3 spearRestPosition;
    private bool isStriking = false;
    private bool returning = false;

    void Start()
    {
        cam = GetComponentInChildren<Camera>();

        if (spear != null)
            spearRestPosition = spear.localPosition;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        HandleMouseLook();
        HandleStrike();
    }

void HandleMouseLook()
{
    var mouse = Mouse.current;
    if (mouse == null) return;

    Vector2 mouseDelta = mouse.delta.ReadValue() * mouseSensitivity * 0.1f;

    xRotation -= mouseDelta.y;
    xRotation = Mathf.Clamp(xRotation, -verticalClamp, verticalClamp);

    if (spear != null)
        spear.localRotation = Quaternion.Euler(xRotation, mouseDelta.x * 10f, 0f);
}

    void HandleStrike()
    {
        if (spear == null) return;

        var mouse = Mouse.current;
        if (mouse == null) return;

        if (mouse.rightButton.wasPressedThisFrame && !isStriking)
        {
            isStriking = true;
            returning = false;

            if (characterAnimator != null)
                characterAnimator.Play(strikeAnimationName);
        }

        if (isStriking && !returning)
        {
            Vector3 strikeTarget = spearRestPosition + Vector3.forward * strikeDistance;
            spear.localPosition = Vector3.MoveTowards(
                spear.localPosition, strikeTarget, strikeSpeed * Time.deltaTime);

            if (Vector3.Distance(spear.localPosition, strikeTarget) < 0.01f)
                returning = true;
        }

        if (returning)
        {
            spear.localPosition = Vector3.MoveTowards(
                spear.localPosition, spearRestPosition, returnSpeed * Time.deltaTime);

            if (Vector3.Distance(spear.localPosition, spearRestPosition) < 0.01f)
            {
                isStriking = false;
                returning = false;
                spear.localPosition = spearRestPosition;
            }
        }
    }
}