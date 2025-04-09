using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement; // For scene reloading
using TMPro;

public class RatMovement : MonoBehaviour
{
    public float stepDistance = 1f;   // Distance per step
    public float moveSpeed = 3f;       // Speed at which the rat moves
    public int steps = 10;             // Number of steps to walk forward and then backward
    public TextMeshProUGUI messageText;

    private Vector3 initialPosition;   // To store the initial position of the rat
    private Quaternion initialRotation; // To store the initial rotation of the rat

    private void Start()
    {
        // Save the initial position and rotation
        initialPosition = transform.position;
        initialRotation = transform.rotation;
        messageText.gameObject.SetActive(false);

        // Start the movement sequence as a coroutine
        StartCoroutine(WalkForwardAndBackward());
    }

    private IEnumerator WalkForwardAndBackward()
    {
        // Repeat the movement and turning sequence indefinitely
        while (true) // This makes the sequence repeat indefinitely
        {
            // Move backward for the specified number of steps
            for (int i = 0; i < steps; i++)
            {
                yield return StartCoroutine(MoveBackward(stepDistance));
            }

            // After moving backward, turn the rat 90 degrees to the left
            Turn(-90);

            for (int i = 0; i < steps/2; i++)
            {
                yield return StartCoroutine(MoveBackward(stepDistance));
            }

            // Turn 180 degrees
            Turn(180);

            for (int i = 0; i < steps/2; i++)
            {
                yield return StartCoroutine(MoveBackward(stepDistance));
            }

            // Turn 90 degrees to the right
            Turn(90);

            for (int i = 0; i < steps; i++)
            {
                yield return StartCoroutine(MoveBackward(stepDistance));
            }

            Turn(180);
        }
    }

    private IEnumerator MoveBackward(float distance)
    {
        // Move the rat backward over time based on its local forward direction (opposite)
        float movedDistance = 0f;

        while (movedDistance < distance)
        {
            float step = moveSpeed * Time.deltaTime;
            transform.Translate(-transform.forward * step); // Move backward relative to its rotation
            movedDistance += step;
            yield return null; // Yield for one frame
        }
    }

    private void Turn(float angle)
    {
        // Get the current rotation
        Quaternion currentRotation = transform.rotation;

        // Set the rotation to a new one, maintaining the current X and Z values, but rotating Y by the given angle
        transform.rotation = Quaternion.Euler(currentRotation.eulerAngles.x, currentRotation.eulerAngles.y + angle, currentRotation.eulerAngles.z);
    }

    // Collision detection method to reset the game when the player collides with the rat
    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collision is with the player (assuming the player has a tag "Player")
        if (collision.gameObject.CompareTag("Player"))
        {
            ShowMessage("CAUGHT BY THE RAT!");
            ResetGame(); // Call reset method
        }
    }

    // Reset the game (reload the scene)
    private void ResetGame()
    {
        // Reset the rat's position and rotation
        transform.position = initialPosition;
        transform.rotation = initialRotation;

        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void ShowMessage(string message)
    {
        Debug.Log("Displaying message: " + message); // Debug log for showing the message
        messageText.text = message;
        messageText.gameObject.SetActive(true);
    }
}
