using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class DragAndThrow : MonoBehaviour
{
    private bool dragging = false;
    private Vector3 offset;
    private Vector3 lastMouseWorldPos;
    private Vector3 mouseVelocity;
    private Rigidbody2D rb;
    public bool hasBeenThrown = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (dragging)
        {
            // Where the mouse is in world space this frame
            Vector3 currentMouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Calculate velocity of mouse movement
            mouseVelocity = (currentMouseWorldPos - lastMouseWorldPos) / Time.deltaTime;
            lastMouseWorldPos = currentMouseWorldPos;

            // Move object to follow mouse
            transform.position = currentMouseWorldPos + offset;
        }
    }

    private void OnMouseDown()
    {
        // Convert mouse to world
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Always allow grabbing, even while airborne
        dragging = true;
        rb.isKinematic = true; // Stop physics while we hold it

        offset = transform.position - mouseWorldPos;
        lastMouseWorldPos = mouseWorldPos;
    }

    private void OnMouseUp()
    {
        dragging = false;
        rb.isKinematic = false;

        // Apply throw force
        rb.linearVelocity = mouseVelocity * 0.3f;

        hasBeenThrown = true;
    }
}
