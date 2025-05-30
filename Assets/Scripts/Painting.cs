using UnityEngine;

public class Painting : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Debug.Log("You stole the painting!");
        gameObject.SetActive(false); // or play animation, etc.
    }
}
