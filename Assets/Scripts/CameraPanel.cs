using UnityEngine;

public class CameraPanel : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject cameraToDisable;
    [SerializeField] private Sprite cameraOffSprite;
    [SerializeField] private Sprite offSprite;
    [SerializeField] private GameObject hackingPanel;

    public bool CanInteract { get; set; }

    void Start()
    {
        CanInteract = true;
    }
    
    public void Interact()
    {
        if (!CanInteract) return;

        hackingPanel.SetActive(true);
        hackingPanel.GetComponent<HackingMinigame>().OnMinigameCompleted += Activate;
    }

    private void Activate()
    {
        CanInteract = false;
        
        if (cameraToDisable != null)
        {
            cameraToDisable.GetComponent<GuardVision>().TurnOffDetectionCone();
        }

        GetComponentInChildren<SpriteRenderer>().sprite = offSprite;
        cameraToDisable.GetComponentInChildren<SpriteRenderer>().sprite = cameraOffSprite;
    }
}
