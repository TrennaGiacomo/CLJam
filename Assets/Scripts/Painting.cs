using UnityEngine;

public class Painting : MonoBehaviour, IInteractable
{
    [SerializeField] private string paintingKeyword = "sky";
    [SerializeField] private Sprite emptySprite;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip CrumplingPaper;
    public Transform exclamationMarkPos;

    public void Interact()
    {
        Debug.Log("You stole the painting!");
        audioSource.clip = CrumplingPaper;
        audioSource.Play();
        GetComponentInChildren<SpriteRenderer>().sprite = emptySprite;
        FindFirstObjectByType<WantedPaintingsUI>().MarkPaintingFound(paintingKeyword, this);
    }

    public void WrongPainting()
    {
        GameManager.Instance.EndGame(gameObject);
    }
}
