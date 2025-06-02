using System.Collections.Generic;
using UnityEngine;

public class PaintingRandomizer : MonoBehaviour
{
    public List<PaintingData> paintingData = new();
    void Start()
    {
        RandomizePaintings();
    }

    private void RandomizePaintings()
    {
        var paintings = FindObjectsByType<Painting>(FindObjectsSortMode.None);

        foreach (var painting in paintings)
        {
            var randPaintingData = Random.Range(0, paintingData.Count);

            painting.keyword = paintingData[randPaintingData].keyword;
            painting.fullSprite = paintingData[randPaintingData].fullSprite;
            painting.emptySprite = paintingData[randPaintingData].emptySprite;
            painting.RefreshSprite();

            paintingData.Remove(paintingData[randPaintingData]);
        }
    }
}
