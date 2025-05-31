using UnityEngine;

public class CameraPanel : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject cameraToDisable;
    [SerializeField] private Sprite offSprite;
    [SerializeField] private GameObject hackMinigame;

    private bool CanInteract = true;

    public void Interact()
    {
        if (cameraToDisable != null)
        {
            cameraToDisable.GetComponent<GuardVision>().detectionEnabled = false;
            cameraToDisable.GetComponentInChildren<SpriteRenderer>().color = Color.red;
        }
    }
}
