using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class IntroSceneManager : MonoBehaviour
{
    public TextMeshProUGUI textSpace;

    public List<string> sentences = new();
    public float TypingSpeed;
    public float DelayBetweenSentences;
    public Image fader;

    private void Start()
    {
        StartCoroutine(TypeText());
    }

    IEnumerator TypeText()
    {
        for (int i = 0; i < sentences.Count; i++)
        {
            textSpace.text = "";

            foreach (char letter in sentences[i])
            {
                textSpace.text += letter;
                yield return new WaitForSeconds(TypingSpeed);
            }

            yield return new WaitForSeconds(DelayBetweenSentences);
        }

        fader.DOFade(1f, 1f);
        yield return new WaitForSeconds(1f);
        
        SceneManager.LoadScene("GameScene");
    }
}
