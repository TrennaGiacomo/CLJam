using UnityEngine;

public class CameraPanel : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject cameraToDisable;

    public void Interact()
    {
        if (cameraToDisable != null)
        {
            cameraToDisable.GetComponent<GuardVision>().detectionEnabled = false;
            cameraToDisable.GetComponentInChildren<SpriteRenderer>().color = Color.red;
        }
    }
}
