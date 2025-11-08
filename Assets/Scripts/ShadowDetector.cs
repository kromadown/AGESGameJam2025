using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ShadowDetector : MonoBehaviour
{
    public Light2D spotLight;
    public LayerMask shadowCasterLayer;

    bool isInShadow;

    void Update()
    {
        float distance = Vector2.Distance(transform.position, spotLight.transform.position);

        bool newShadowState = false;

        // Only check if within light radius
        if (distance <= spotLight.pointLightOuterRadius)
        {
            RaycastHit2D hit = Physics2D.Linecast(
                spotLight.transform.position,
                transform.position,
                shadowCasterLayer
            );

            if (hit.collider != null)
                newShadowState = true;
        }

        // Log only when state changes (prevents spam)
        if (newShadowState != isInShadow)
        {
            isInShadow = newShadowState;
            Debug.Log(isInShadow ? "Player ENTERED SHADOW" : "Player ENTERED LIGHT");
        }
    }
}
