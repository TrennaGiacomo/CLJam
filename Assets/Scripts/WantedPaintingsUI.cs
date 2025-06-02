using UnityEngine;
using TMPro;

public class WantedPaintingsUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] clueTexts;
    private bool correctPainting = false;

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
        {
            Debug.Log("Wrong Painting");
            painting.WrongPainting();
        }

        correctPainting = false;
    }
}