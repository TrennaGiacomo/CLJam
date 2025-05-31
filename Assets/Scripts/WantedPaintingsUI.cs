using UnityEngine;
using TMPro;

public class WantedPaintingsUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] clueTexts;
    private bool correctPainting;

    public void MarkPaintingFound(string keyword, Painting painting)
    {
        foreach (var clue in clueTexts)
        {
            if (clue != null && clue.text.ToLower().Contains(keyword.ToLower()))
            {
                clue.text = $"<s>{clue.text}</s>";
                correctPainting = true;
            }
        }

        if (!correctPainting)
            painting.WrongPainting();

    }
}
