using UnityEngine;

public class CharacterMovement2D : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 3f;

    private PlayerInteraction playerInteraction;
    private Vector2 moveInput;
    private InputSystemActions inputActions;

    private void Awake()
    {
        inputActions = new InputSystemActions();
        playerInteraction = GetComponent<PlayerInteraction>();
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
        moveInput = inputActions.Player.Move.ReadValue<Vector2>();

        if (Mathf.Abs(moveInput.x) > Mathf.Abs(moveInput.y))
            moveInput.y = 0;
        else
            moveInput.x = 0;

        if (moveInput != Vector2.zero)
            playerInteraction.SetFacingDirection(moveInput);

        transform.position += (Vector3)(moveSpeed * Time.deltaTime * moveInput);
    }
}