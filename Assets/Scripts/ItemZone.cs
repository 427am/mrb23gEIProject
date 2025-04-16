using UnityEngine;
using TMPro;
using System.Collections;

public class ItemZone : MonoBehaviour
{
    public int itemCount = 0;
    public TextMeshProUGUI itemCountText;
    public GameObject winPopup;
    public TextMeshProUGUI messageText;

    private void Start()
    {
        winPopup.SetActive(false);
        messageText.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            itemCount++;
            UpdateItemCountUI();
            Destroy(other.gameObject);

            if (itemCount >= 5)
            {
                ShowWinPopup();
                StartCoroutine(ExitGameAfterDelay(15f)); 
            }
        }
        else if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered the Item Zone!");
            ShowMessage("DROP ITEMS HERE");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player left the Item Zone!");
            messageText.gameObject.SetActive(false);
        }
    }

    private void UpdateItemCountUI()
    {
        itemCountText.text = itemCount + "/5";
    }

    private void ShowWinPopup()
    {
        winPopup.SetActive(true);
        messageText.gameObject.SetActive(false);
    }

    private void ShowMessage(string message)
    {
        Debug.Log("Displaying message: " + message);
        messageText.text = message;
        messageText.gameObject.SetActive(true);
    }

    
    private IEnumerator ExitGameAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Debug.Log("Exiting game...");
        Application.Quit();

        
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}