using UnityEngine;

public class BreakOnImpact : MonoBehaviour
{
    public float breakForce = 10f;
    public string breakerTag;

    private DragAndThrow throwScript;
    private SpawnItem spawnItemScript;

    void Start()
    {
        throwScript = GetComponent<DragAndThrow>();
        spawnItemScript = GetComponent<SpawnItem>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Only break if the object has been thrown
        // if (throwScript == null || !throwScript.hasBeenThrown)
        //     return;

        // Don't break if hitting the player
        if (!collision.collider.CompareTag(breakerTag))
            return;

        float impact = collision.relativeVelocity.magnitude;
        Debug.Log(impact);

        if (impact >= breakForce)
        {
            spawnItemScript.SpawnSphere();
            Destroy(gameObject);

            // Optional effects
            // Instantiate(breakEffect, transform.position, Quaternion.identity);
        }
    }
}
