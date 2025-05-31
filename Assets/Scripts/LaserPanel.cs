using UnityEngine;

public class LaserPanel : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject[] lasersToDisable;

    public void Interact()
    {
        foreach (var laser in lasersToDisable)
        {
            laser.SetActive(false);
        }
    }
}
