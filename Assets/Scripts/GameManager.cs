using System.Collections;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public AudioClip spotted;
    public GameObject exclamationMark;
    public Image fader;
    private AudioSource audioSource;
    private bool isGameOver = false;
    private GameObject exclamationMarkInstance;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    void Start()
    {
        audioSource = Camera.main.GetComponent<AudioSource>();
    }

    public void EndGame(GameObject spotter)
    {
        if (isGameOver) return;

        if (spotter.TryGetComponent<Laser>(out var laser))
            exclamationMarkInstance = Instantiate(exclamationMark, laser.exclamationMarkPos);
        else if (spotter.TryGetComponent<GuardVision>(out var guard))
            exclamationMarkInstance = Instantiate(exclamationMark, guard.exclamationMarkPos);
        else if (spotter.TryGetComponent<Painting>(out var painting))
            exclamationMarkInstance = Instantiate(exclamationMark, painting.exclamationMarkPos);
        else
            return;

        isGameOver = true;
        StartCoroutine(EndGameCoroutine());
    }

    private IEnumerator EndGameCoroutine()
    {
        audioSource.clip = spotted;
        audioSource.Play();
        var seq = DOTween.Sequence();

        seq.Append(exclamationMarkInstance.transform.DOScale(Vector3.one, 0.15f).SetEase(Ease.OutBack));
        seq.Join(exclamationMarkInstance.transform.DOPunchScale(new Vector3(1f, 1f, 0), 0.3f, 10, 0.5f));
        seq.Join(exclamationMarkInstance.transform.DOShakeRotation(0.3f, new Vector3(0, 0, 15), 20, 90));

        var player = FindFirstObjectByType<CharacterMovement2D>();
        player.DisableInput();

        var allGuards = FindObjectsByType<GuardVision>(FindObjectsSortMode.None);
        foreach (var guard in allGuards)
            guard.detectionEnabled = false;

        var allGuardsPatrol = FindObjectsByType<GuardPatrol>(FindObjectsSortMode.None);
        foreach (var guardPatrol in allGuardsPatrol)
        {
            guardPatrol.canMove = false;
            guardPatrol.animator.SetBool("IsMoving", false);
        }

        yield return new WaitForSeconds(2f);

        fader.gameObject.SetActive(true);

        fader.DOFade(1f, 1f);
        yield return new WaitForSeconds(1f);

        PersistantSceneManager.Instance.LoadScene("EndScene");
    }
}
