using UnityEngine;

public class PaintingDisplay : MonoBehaviour
{
    [SerializeField] private string paintingKeyword;
    [SerializeField] private Sprite collectedSprite;
    [SerializeField] private Sprite emptySprite;

    private void Awake()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (PaintingsManager.Instance != null && PaintingsManager.Instance.isCollected(paintingKeyword))
        {
            sr.sprite = collectedSprite;
            FindFirstObjectByType<EndScreenMessage>().correctPaintings++;
        }
        else
            sr.sprite = emptySprite;
    }
}