using UnityEngine;

public class BreakOnImpact : MonoBehaviour
{
    public float breakForce = 10f;

    private DragAndThrow throwScript;

    void Start()
    {
        throwScript = GetComponent<DragAndThrow>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Only break if the object has been thrown
        if (throwScript == null || !throwScript.hasBeenThrown)
            return;

        // Don't break if hitting the player
        if (collision.collider.CompareTag("Player"))
            return;

        float impact = collision.relativeVelocity.magnitude;
        Debug.Log(impact);

        if (impact >= breakForce)
        {
            Destroy(gameObject);

            // Optional effects
            // Instantiate(breakEffect, transform.position, Quaternion.identity);
        }
    }
}
