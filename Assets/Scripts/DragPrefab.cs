using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class DragPrefab : MonoBehaviour
{
    private Camera cam;
    private Rigidbody2D rb;

    private bool dragging = false;
    private Vector3 offset;
    private Vector3 lastMouseWorldPos;
    private Vector3 mouseVelocity;

    [Range(0f, 2f)]
    public float throwStrength = 0.3f;

    void Start()
    {
        cam = Camera.main;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!dragging) return;

        // Current mouse world position
        Vector3 currentMouseWorldPos = cam.ScreenToWorldPoint(Input.mousePosition);
        currentMouseWorldPos.z = 0;

        // Calculate velocity (for throw)
        mouseVelocity = (currentMouseWorldPos - lastMouseWorldPos) / Time.deltaTime;
        lastMouseWorldPos = currentMouseWorldPos;

        // Apply offset dragging
        Vector3 targetPos = currentMouseWorldPos + offset;

        // Move object while physics is paused
        transform.position = targetPos;
    }

    void OnMouseDown()
    {
        dragging = true;
        rb.isKinematic = true; // pause physics while dragging

        Vector3 mouseWorldPos = cam.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0;

        offset = transform.position - mouseWorldPos;
        lastMouseWorldPos = mouseWorldPos;
    }

    void OnMouseUp()
    {
        dragging = false;
        rb.isKinematic = false; // restore physics

        // Apply throw force
        rb.linearVelocity = mouseVelocity * throwStrength;
    }
}
