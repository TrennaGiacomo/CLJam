using UnityEngine;

public class CharacterMovement2D : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 3f;

    private Vector2 moveInput;
    private InputSystemActions inputActions;

    private void Awake()
    {
        inputActions = new InputSystemActions();
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

        if (moveInput.sqrMagnitude > 1)
            moveInput = moveInput.normalized;

        transform.position += (Vector3)(moveSpeed * Time.deltaTime * moveInput);
    }
}
