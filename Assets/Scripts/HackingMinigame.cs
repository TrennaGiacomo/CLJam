using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class HackingMinigame : MonoBehaviour
{
    [Header("Sequence")]
    [SerializeField] private int sequenceLength = 5;
    [SerializeField] private List<Direction> possibleDirections = new();
    [SerializeField] private Image[] arrowImages;
    [SerializeField] private Sprite[] arrowSpritesGray; // Index: 0=Up, 1=Down, 2=Left, 3=Right
    [SerializeField] private Sprite[] arrowSpritesLit;
    [SerializeField] private GameObject redFilter;

    [Header("Buttons")]
    [SerializeField] private List<ButtonVisual> buttonVisuals;
    [SerializeField] private AudioClip[] pressSound;
    [SerializeField] private AudioClip errorSound;
    [SerializeField] private AudioClip SuccessSound;
    [SerializeField] private AudioSource audioSource;

    private int inputIndex = 0;
    private List<Direction> correctSequence = new();

    public event Action OnMinigameCompleted;

    void OnEnable()
    {
        InitializeMinigame();
    }

    private void InitializeMinigame()
    {
        correctSequence.Clear();
        inputIndex = 0;

        RandomizeSequence();

        for (int i = 0; i < arrowImages.Length; i++)
        {
            int dir = (int)correctSequence[i];
            arrowImages[i].sprite = arrowSpritesGray[dir];
        }

        foreach (var bv in buttonVisuals)
        {
            var captured = bv;
            bv.button.interactable = true;
            bv.button.onClick.RemoveAllListeners();
            bv.button.onClick.AddListener(() => OnButtonPressed(captured));
        }

        FindFirstObjectByType<CharacterMovement2D>().DisableInput();
    }

    private void RandomizeSequence()
    {
        for (int i = 0; i < sequenceLength; i++)
        {
            correctSequence.Add(possibleDirections[UnityEngine.Random.Range(0, possibleDirections.Count)]);
        }
    }

    private void OnButtonPressed(ButtonVisual bv)
    {
        StartCoroutine(HandleButtonVisual(bv));

        if (audioSource && pressSound.Length != 0)
            audioSource.PlayOneShot(pressSound[UnityEngine.Random.Range(0, pressSound.Length)]);

        if (bv.direction == correctSequence[inputIndex])
        {
            int dir = (int)bv.direction;
            arrowImages[inputIndex].sprite = arrowSpritesLit[dir];
            inputIndex++;

            if (inputIndex >= correctSequence.Count)
            {
                Debug.Log("Minigame completed!");
                audioSource.clip = SuccessSound;
                audioSource.Play();
                StartCoroutine(WaitAndGetOutOfThePanel(true));
            }
        }
        else
        {
            Debug.Log("Wrong input!");
            Shake();
            StartCoroutine(WaitAndGetOutOfThePanel(false));
        }
    }

    private IEnumerator WaitAndGetOutOfThePanel(bool won)
    {
        yield return new WaitForSeconds(1f);

        FindFirstObjectByType<CharacterMovement2D>().EnableInput();
        
        if (won)
            OnMinigameCompleted?.Invoke();

        gameObject.SetActive(false);
    }

    private void Shake()
    {
        StartCoroutine(ScreenFlash());

        transform.DOShakePosition(
            duration: 0.5f,
            strength: new Vector2(10f, 0f),
            vibrato: 10,
            randomness: 90,
            snapping: false,
            fadeOut: true
        );
        audioSource.clip = errorSound;
        audioSource.Play();
    }

    private IEnumerator ScreenFlash()
    {
        redFilter.SetActive(true);
        yield return new WaitForSeconds(.2f);
        redFilter.SetActive(false);
        yield return new WaitForSeconds(.2f);
        redFilter.SetActive(true);
        yield return new WaitForSeconds(.2f);
        redFilter.SetActive(false);
        yield return new WaitForSeconds(.2f);
    }

    private IEnumerator HandleButtonVisual(ButtonVisual bv)
    {
        Image image = bv.button.GetComponent<Image>();
        bv.button.interactable = false;
        image.sprite = bv.pressedSprite;

        yield return new WaitForSeconds(0.3f);

        image.sprite = bv.normalSprite;
        bv.button.interactable = true;
    }

    public void ClosePanel()
    {
        gameObject.SetActive(false);
        FindFirstObjectByType<CharacterMovement2D>().EnableInput();
    }

    public enum Direction { Up = 0, Down = 1, Left = 2, Right = 3 }
}
