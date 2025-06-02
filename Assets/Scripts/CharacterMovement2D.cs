using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterMovement2D : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 3f;

    private PlayerInteraction playerInteraction;
    private Vector2 moveInput;
    private InputSystemActions inputActions;
    private Rigidbody2D rb;
    private Animator animator;

    private bool inputEnabled = true;

    public GameObject InGameMenu;

    public GameObject miniGame;

    private void Awake()
    {
        inputActions = new InputSystemActions();
        playerInteraction = GetComponent<PlayerInteraction>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
    }

    private void OnDisable()
    {
        inputActions.Player.Disable();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (miniGame.activeSelf)
            {
                miniGame.SetActive(false);
                inputEnabled = true;
            }
            else
            {
                InGameMenu.SetActive(!InGameMenu.activeSelf);

                if (InGameMenu.activeSelf)
                    Time.timeScale = 0;
                else
                    Time.timeScale = 1;
            }
        }

        if (!inputEnabled)
        {
            moveInput = Vector2.zero;
            animator.SetBool("IsMoving", false);
            animator.SetFloat("MoveX", 0);
            animator.SetFloat("MoveY", 0);
            return;
        }

        if (Time.timeScale == 1)
            moveInput = inputActions.Player.Move.ReadValue<Vector2>();

        if (Mathf.Abs(moveInput.x) > Mathf.Abs(moveInput.y))
            moveInput.y = 0;
        else
            moveInput.x = 0;

        if (moveInput != Vector2.zero)
        {
            playerInteraction.SetFacingDirection(moveInput);
            animator.SetBool("IsMoving", true);
        }
        else
        {
            animator.SetBool("IsMoving", false);
        }

        animator.SetFloat("MoveX", moveInput.x);
        animator.SetFloat("MoveY", moveInput.y);
    }


    private void FixedUpdate()
    {
        Vector2 targetPos = rb.position + moveInput.normalized * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(targetPos);
    }

    public void DisableInput()
    {
        inputEnabled = false;
    }

    public void EnableInput()
    {
        inputEnabled = true;
    }

    public void Resume()
    {
        Time.timeScale = 1;
    }
}