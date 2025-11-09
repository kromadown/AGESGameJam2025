using UnityEngine;

public class SpawnItem : MonoBehaviour
{
    public GameObject objectToSpawn;
    public Transform player;

    public void SpawnSphere()
    {
        Instantiate(objectToSpawn, transform.position, Quaternion.identity);
        Debug.Log("Spawned Sphere");

        DragAndThrow drag = objectToSpawn.GetComponent<DragAndThrow>();
        if (drag != null)
        {
            drag.boundaryCenter = player;
        }
    }
}