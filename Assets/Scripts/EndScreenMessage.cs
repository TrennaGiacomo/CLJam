using System.Collections;
using TMPro;
using UnityEngine;

public class EndScreenMessage : MonoBehaviour
{
    public int correctPaintings;
    public TextMeshProUGUI text;
    void Start()
    {
        StartCoroutine(ShowMessage());
    }

    private IEnumerator ShowMessage()
    {
        yield return new WaitForSeconds(0.1f);

        if (correctPaintings == 0)
        {
            text.text = "You should change job...";
        }
        else if (correctPaintings == 1 || correctPaintings == 2)
        {
            text.text = "Not bad, but could be better";
        }
        else if (correctPaintings == 3)
        {
            text.text = "So close...";
        }
        else if (correctPaintings >= 4)
        {
            text.text = "You finally achieved your dream!!";
        }
    }
}
