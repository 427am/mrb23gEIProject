using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using TMPro;

public class RatMovement : MonoBehaviour
{
    public float stepDistance = 1f;  
    public float moveSpeed = 3f;      
    public int steps = 10;           
    public TextMeshProUGUI messageText;

    private Vector3 initialPosition;   
    private Quaternion initialRotation; 

    private void Start()
    {
        
        initialPosition = transform.position;
        initialRotation = transform.rotation;
        messageText.gameObject.SetActive(false);

        
        StartCoroutine(WalkForwardAndBackward());
    }

    private IEnumerator WalkForwardAndBackward()
    {
        
        while (true) 
        {
            
            for (int i = 0; i < steps; i++)
            {
                yield return StartCoroutine(MoveBackward(stepDistance));
            }

           
            Turn(-90);

            for (int i = 0; i < steps/2; i++)
            {
                yield return StartCoroutine(MoveBackward(stepDistance));
            }

            
            Turn(180);

            for (int i = 0; i < steps/2; i++)
            {
                yield return StartCoroutine(MoveBackward(stepDistance));
            }

           
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
        
        float movedDistance = 0f;

        while (movedDistance < distance)
        {
            float step = moveSpeed * Time.deltaTime;
            transform.Translate(-transform.forward * step); 
            movedDistance += step;
            yield return null; 
        }
    }

    private void Turn(float angle)
    {
        
        Quaternion currentRotation = transform.rotation;

        
        transform.rotation = Quaternion.Euler(currentRotation.eulerAngles.x, currentRotation.eulerAngles.y + angle, currentRotation.eulerAngles.z);
    }

    
    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.CompareTag("Player"))
        {
            ShowMessage("CAUGHT BY THE RAT!");
            ResetGame(); 
        }
    }

    
    private void ResetGame()
    {
        
        transform.position = initialPosition;
        transform.rotation = initialRotation;

        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void ShowMessage(string message)
    {
        Debug.Log("Displaying message: " + message); 
        messageText.text = message;
        messageText.gameObject.SetActive(true);
    }
}
