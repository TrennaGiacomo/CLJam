using UnityEngine;

public class LaserPanel : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject objectToDisable;
    [SerializeField] private GameObject objectToEnable;

    public void Interact()
    {
        objectToDisable.SetActive(false);
        objectToEnable.SetActive(true);
    }
}
