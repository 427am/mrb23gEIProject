using UnityEngine;

public class Item : MonoBehaviour
{
    
    private void OnValidate()
    {
        if (!CompareTag("Item"))
        {
            gameObject.tag = "Item"; 
        }
    }
}

