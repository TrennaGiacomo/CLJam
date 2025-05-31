using UnityEngine;
using TMPro;

public class WantedPaintingsUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] clueTexts;

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
