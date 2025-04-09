using UnityEngine;

public class Item : MonoBehaviour
{
    // This script is just to ensure the item is tagged correctly
    private void OnValidate()
    {
        if (!CompareTag("Item"))
        {
            gameObject.tag = "Item"; // Ensure the item has the correct tag
        }
    }
}

