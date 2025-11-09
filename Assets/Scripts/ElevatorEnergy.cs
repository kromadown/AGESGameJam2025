using UnityEngine;
using UnityEngine.UI;

public class ElevatorEnergy : MonoBehaviour
{
    public float progress = 0f;
    public float maxProgress = 2f;
    public Image progressBar;
    public Transform elevator;
    public float elevatorHeight = 5f;
    public float moveSpeed = 1f;

    private bool elevatorActive = false;
    private Vector3 targetPosition;

    void Update()
    {
        progressBar.fillAmount = Mathf.Clamp(progress / maxProgress, 0, 1);

        if (progress >= maxProgress && elevatorActive == false)
        {
            Debug.Log("Progress complete!");

            elevatorActive = true;

            // Tell elevator where to go
            targetPosition = new Vector3(
                elevator.position.x,
                elevator.position.y + elevatorHeight,
                elevator.position.z
            );
        }

        if (elevatorActive)
        {
            elevator.position = Vector3.MoveTowards(
                elevator.position,
                targetPosition,
                moveSpeed * Time.deltaTime
            );
        }
    }

    public void AddProgress(float amount)
    {
        progress += amount;
    }
}
