using UnityEngine;
using TMPro;  // Assuming you're using TextMeshPro for UI

public class GameManager : MonoBehaviour
{
    public int totalItems = 5;  // Total number of items to collect
    private int collectedItems = 0;  // Counter for items in the item zone

    public TextMeshProUGUI progressText;  // Text UI to show progress (e.g., "1/5")
    public GameObject winPopup;  // Popup to display when the game is won
    public GameObject zonePopup;  // Popup that appears when the player enters the zone

    void Start()
    {
        UpdateProgressText();
        winPopup.SetActive(false);  // Hide win popup at the start
        zonePopup.SetActive(false);  // Hide the zone entry popup at the start
    }

    // Call this function when an item is placed in the zone
    public void OnItemCollected()
    {
        collectedItems++;
        UpdateProgressText();

        // Check if all items are collected
        if (collectedItems >= totalItems)
        {
            ShowWinPopup();
        }
    }

    // Call this function when the player enters the item zone
    public void OnEnterZone()
    {
        zonePopup.SetActive(true);  // Show the zone entry popup
    }

    // Call this function when the player leaves the item zone
    public void OnExitZone()
    {
        zonePopup.SetActive(false);  // Hide the zone entry popup
    }

    private void UpdateProgressText()
    {
        progressText.text = $"{collectedItems}/{totalItems}";
    }

    private void ShowWinPopup()
    {
        winPopup.SetActive(true);  // Display the win popup
    }
}
