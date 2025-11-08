using UnityEngine;

public class Damage : MonoBehaviour
{
    public PlayerHealth pHealth;
    public float damagePerSecond;

    private void OnTriggerStay2D(Collider2D other)
    {
        // Make sure the object we touched is the player
        // (Replace "Player" with your actual player tag if different)
        if (!other.CompareTag("Light Area"))
            return;

        // Player is in darkness → deal damage
        pHealth.health -= damagePerSecond * Time.deltaTime;
    }
}
