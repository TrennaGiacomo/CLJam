using UnityEngine;

public class ExitTrigger : MonoBehaviour
{
    public GameObject confirmationPanel;
    private CharacterMovement2D playerMovement;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<CharacterMovement2D>())
        {
            Debug.Log("Player has triggered exit");
            playerMovement = other.gameObject.GetComponent<CharacterMovement2D>();
            playerMovement.DisableInput();
            confirmationPanel.SetActive(true);
        }
            
    }

    public void ClickNo()
    {
        if(playerMovement != null)
            playerMovement.EnableInput();

        confirmationPanel.SetActive(false);
    }

    public void ClickYes()
    {
        PersistantSceneManager.Instance.LoadScene("EndScene");
    }
}
