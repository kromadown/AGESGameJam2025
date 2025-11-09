using UnityEngine;

public class PlayerLightDetector : MonoBehaviour
{
    public bool isLit = false;
    public PlayerHealth pHealth;
    public float damagePerSecond;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Light Area"))
            isLit = true;
            //Debug.Log("Enter");
    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        // Only damage if NOT in light
        if (!isLit)
        {
            pHealth.health -= damagePerSecond * Time.deltaTime;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Light Area"))
            isLit = false;
            //Debug.Log("Exit");
    }
}

