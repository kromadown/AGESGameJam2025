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
    private LineRenderer line;

    // Boundary (world space)
    public Vector2 boundaryExtents = new Vector2(2f, 1f);

    // The object (or player) this boundary follows
    [SerializeField] public Transform boundaryCenter;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        line = GetComponent<LineRenderer>();
        line.positionCount = 5; // 4 corners + repeat first to close loop
        line.startColor = Color.green;
        line.endColor = Color.green;

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

            Vector3 targetPos = currentMouseWorldPos + offset;

            Vector3 center = boundaryCenter.position;

            float minX = center.x - boundaryExtents.x;
            float maxX = center.x + boundaryExtents.x;
            float minY = center.y - boundaryExtents.y;
            float maxY = center.y + boundaryExtents.y;

            // Clamp position inside boundary
            targetPos.x = Mathf.Clamp(targetPos.x, minX, maxX);
            targetPos.y = Mathf.Clamp(targetPos.y, minY, maxY);

            // Move object to follow mouse
            rb.MovePosition(targetPos);

            if (line == null) return;

            line.SetPosition(0, new Vector3(minX, minY, 0));
            line.SetPosition(1, new Vector3(minX, maxY, 0));
            line.SetPosition(2, new Vector3(maxX, maxY, 0));
            line.SetPosition(3, new Vector3(maxX, minY, 0));
            line.SetPosition(4, new Vector3(minX, minY, 0)); // close loop
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
