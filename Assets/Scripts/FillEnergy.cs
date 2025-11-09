using UnityEngine;

public class FillEnergy : MonoBehaviour
{
    public float progressAmount = 1f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Elevator"))
        {
            Debug.Log("IT HITS");
            ElevatorEnergy bar = FindObjectOfType<ElevatorEnergy>();
            bar.AddProgress(progressAmount);

            Destroy(gameObject);
        }
    }
}
