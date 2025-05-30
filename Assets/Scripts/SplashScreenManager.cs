using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class SplashScreenManager : MonoBehaviour
{
    public GameObject Logo;
    public Image Fader;
    public float FadeDuration;
    public TextMeshProUGUI Text;
    public float TypingSpeed;

    private string _name = "Spicy Ice";

    void Start()
    {
        StartCoroutine(FadeScreenIn());
    }

    IEnumerator FadeScreenIn()
    {
        float t = 0f;
        Color color = Fader.color;

        while (t < FadeDuration)
        {
            t += Time.deltaTime;
            color.a = Mathf.Lerp(1f, 0f, t / FadeDuration);
            Fader.color = color;
            yield return null;
        }

        StartCoroutine(TypeText());
        Logo.GetComponent<Animator>().SetTrigger("Anim");
    }

    IEnumerator FadeScreenOut()
    {
        float t = 0f;
        Color color = Fader.color;

        while (t < FadeDuration)
        {
            t += Time.deltaTime;
            color.a = Mathf.Lerp(0f, 1f, t / FadeDuration);
            Fader.color = color;
            yield return null;
        }

        SceneManager.LoadScene("MainMenu");
    }

    IEnumerator TypeText()
    {
        Text.text = "";
        foreach (char letter in _name)
        {
            Text.text += letter;
            yield return new WaitForSeconds(TypingSpeed);
        }

        yield return new WaitForSeconds(2);
        StartCoroutine(FadeScreenOut());
    }
}
