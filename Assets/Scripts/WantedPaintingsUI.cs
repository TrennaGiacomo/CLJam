using UnityEngine;
using TMPro;

public class WantedPaintingsUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] clueTexts;

    // Call this when a painting with a matching tag/type is stolen
    public void MarkPaintingFound(string keyword)
    {
        foreach (var clue in clueTexts)
        {
            if (clue != null && clue.text.ToLower().Contains(keyword.ToLower()))
            {
                clue.text = $"<s>{clue.text}</s>";
                // clue.gameObject.SetActive(false);
            }
        }
    }
}
