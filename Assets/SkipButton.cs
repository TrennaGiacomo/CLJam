using UnityEngine;

public class SkipButton : MonoBehaviour
{
    void Start()
    {
        if (PersistantSceneManager.Instance != null)
            gameObject.SetActive(false);
    }

    public void Skip()
    {
        PersistantSceneManager.Instance.LoadScene("GameScene");
    }
}
