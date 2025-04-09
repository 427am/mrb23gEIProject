using UnityEngine;
using TMPro; // For TextMeshPro

public class ItemZone : MonoBehaviour
{
    public int itemCount = 0;
    public TextMeshProUGUI itemCountText; // TextMeshPro for displaying the item count
    public GameObject winPopup; // UI Panel for the win popup
    public TextMeshProUGUI messageText; // TextMeshPro for displaying a message when the player enters the zone

    private void Start()
    {
        // Initially hide the win popup and message
        winPopup.SetActive(false);
        messageText.gameObject.SetActive(false);
    }

    // Trigger when an item enters the zone
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item")) // Make sure the object is tagged as "Item"
        {
            // Increase item count
            itemCount++;
            UpdateItemCountUI();

            // Optionally, destroy the item or disable it
            Destroy(other.gameObject); // or other.gameObject.SetActive(false);

            // Check if item count is 5 to show win popup
            if (itemCount >= 5)
            {
                ShowWinPopup();
            }
        }
        else if (other.CompareTag("Player")) // Check when the player enters the zone
        {
            Debug.Log("Player entered the Item Zone!"); // Debug log for checking player entry
            ShowMessage("DROP ITEMS HERE");
        }
    }

    // Trigger when an item leaves the zone (optional)
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Hide the message when the player leaves the zone
            Debug.Log("Player left the Item Zone!"); // Debug log for checking player exit
            messageText.gameObject.SetActive(false);
        }
    }

    // Update the item count UI using TextMeshPro
    private void UpdateItemCountUI()
    {
        itemCountText.text = itemCount + "/5";
    }

    // Show the win popup
    private void ShowWinPopup()
    {
        winPopup.SetActive(true);
        messageText.gameObject.SetActive(false); // Hide the message when the game ends
    }

    // Show the message when the player enters the zone
    private void ShowMessage(string message)
    {
        Debug.Log("Displaying message: " + message); // Debug log for showing the message
        messageText.text = message;
        messageText.gameObject.SetActive(true);
    }
}
