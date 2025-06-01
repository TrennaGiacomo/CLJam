using System.Collections.Generic;
using UnityEngine;

public class PaintingsManager : MonoBehaviour
{
    public static PaintingsManager Instance;
    private HashSet<string> collectedPaintings = new();

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    public void MarkCollected(string paintingKeyword)
    {
        collectedPaintings.Add(paintingKeyword);
    }

    public bool isCollected(string paintingKeyword)
    {
        return collectedPaintings.Contains(paintingKeyword);
    }
}
