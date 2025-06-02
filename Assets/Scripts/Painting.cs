using UnityEngine;

public class Painting : MonoBehaviour, IInteractable
{
    public string keyword;
    public Sprite fullSprite;
    public Sprite emptySprite;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip CrumplingPaper;
    public Transform exclamationMarkPos;
    public GameObject wrongPainting;
    public bool CanInteract { get; set; }

    void Start()
    {
        if (audioSource == null)
            audioSource = Camera.main.GetComponent<AudioSource>();
        CanInteract = true;        
    }

    public void RefreshSprite()
    {
        GetComponentInChildren<SpriteRenderer>().sprite = fullSprite;
    }

    public void Interact()
    {
        if (!CanInteract) return;

        Debug.Log("You stole the painting!");
        audioSource.clip = CrumplingPaper;
        audioSource.Play();
        GetComponentInChildren<SpriteRenderer>().sprite = emptySprite;
        FindFirstObjectByType<WantedPaintingsUI>().MarkPaintingFound(keyword, this);
        PaintingsManager.Instance.MarkCollected(keyword);
        CanInteract = false;
    }

    public void WrongPainting()
    {
        wrongPainting.SetActive(true);
        GameManager.Instance.EndGame(gameObject);
    }
}
