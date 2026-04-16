using UnityEngine;
using UnityEngine.InputSystem;

public class LanceController : MonoBehaviour
{
    public float mouseSensitivity = 3f;
    public float horizontalRange = 400f;
    public float verticalRange = 200f;
    public float aimDepth = 10f;

    private Camera cam;
    private Vector2 screenPos;

    void Start()
    {
        cam = Camera.main;
        screenPos = new Vector2(Screen.width / 2f, Screen.height / 2f);
    }

    void Update()
    {
        var mouse = Mouse.current;
        if (mouse == null) return;

        Vector2 delta = mouse.delta.ReadValue() * mouseSensitivity;
        screenPos.x += delta.x;
        screenPos.y += delta.y;

        float centerX = Screen.width / 2f;
        float centerY = Screen.height / 2f;
        screenPos.x = Mathf.Clamp(screenPos.x, centerX - horizontalRange, centerX + horizontalRange);
        screenPos.y = Mathf.Clamp(screenPos.y, centerY - verticalRange, centerY + verticalRange);

        Vector3 targetWorld = cam.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, aimDepth));
        transform.LookAt(targetWorld);
        transform.rotation = transform.rotation * Quaternion.Euler(90f, 0f, 0f);
    }
}