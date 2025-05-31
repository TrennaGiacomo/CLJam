using UnityEngine;

public class LaserPanel : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject objectToDisable;
    [SerializeField] private GameObject objectToEnable;
    [SerializeField] private Sprite offSprite;
    [SerializeField] private GameObject hackingPanel;

    private bool CanInteract = true;


    public void Interact()
    {
        if (!CanInteract) return;

        hackingPanel.SetActive(true);
        hackingPanel.GetComponent<HackingMinigame>().OnMinigameCompleted += Activate;
    }

    private void Activate()
    {
        objectToDisable.GetComponentInParent<Laser>().enabled = false;
        objectToDisable.SetActive(false);
        objectToEnable.SetActive(true);
        CanInteract = false;
        GetComponentInChildren<SpriteRenderer>().sprite = offSprite;
    }
}
