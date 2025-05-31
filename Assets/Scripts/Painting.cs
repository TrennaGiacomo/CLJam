using UnityEngine;

public class Painting : MonoBehaviour, IInteractable
{
    [SerializeField] private string paintingKeyword = "sky";

    public void Interact()
    {
        Debug.Log("You stole the painting!");
        FindFirstObjectByType<WantedPaintingsUI>().MarkPaintingFound(paintingKeyword);
        gameObject.SetActive(false); // or play animation, etc.
    }
}
