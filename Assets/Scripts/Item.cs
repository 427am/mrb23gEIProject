using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class Item : MonoBehaviour
{
    public bool isHeld = false;  

    private XRGrabInteractable grabInteractable; 

    private void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();

        grabInteractable.selectEntered.AddListener(OnSelectEntered);
        grabInteractable.selectExited.AddListener(OnSelectExited);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ItemZone") && isHeld)
        {
    
            GameManager gameManager = FindObjectOfType<GameManager>();
            if (gameManager != null)
            {
                gameManager.OnItemCollected();
            }

          
            gameObject.SetActive(false);  
        }
    }

    // This method is called when the item is selected (picked up) by the VR controller
    private void OnSelectEntered(SelectEnterEventArgs args)
    {
        isHeld = true;
    }

    // This method is called when the item is deselected (dropped) by the VR controller
    private void OnSelectExited(SelectExitEventArgs args)
    {
        isHeld = false;
    }
}
