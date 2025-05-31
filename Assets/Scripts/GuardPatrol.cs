using UnityEngine;

public class GuardPatrol : MonoBehaviour
{
    [Header("Patrol Path")]
    [SerializeField] private Transform[] patrolPoints;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float waitTimeAtPoint = 1f;
    [SerializeField] private bool loop = true;
    [SerializeField] private bool pingpong = false;

    [Header("References")]
    [SerializeField] private Animator animator;

    public Vector2 CurrentMoveDirection => lastDirection;

    private int currentPointIndex = 0;
    private float waitTimer = 0f;
    private bool isWaiting = false;
    private int direction = 1; // 1 = forward, -1 = backward (for pingpong)
    private Vector2 lastDirection = Vector2.down;

    private void Update()
    {
        if (patrolPoints.Length == 0) return;

        if (isWaiting)
        {
            waitTimer -= Time.deltaTime;
            if (waitTimer <= 0f)
            {
                isWaiting = false;
                AdvanceToNextPoint();
            }

            // Stop animation while waiting
            animator.SetBool("IsMoving", false);
            return;
        }

        Transform targetPoint = patrolPoints[currentPointIndex];
        Vector3 directionToTarget = targetPoint.position - transform.position;
        Vector3 move = directionToTarget.normalized * moveSpeed * Time.deltaTime;

        // Clamp movement to avoid overshooting
        if (move.magnitude > directionToTarget.magnitude)
            move = directionToTarget;

        transform.position += move;

        // Update animation parameters only when actually moving
        if (move.sqrMagnitude > 0.0001f)
        {
            lastDirection = directionToTarget.normalized;

            animator.SetFloat("MoveX", lastDirection.x);
            animator.SetFloat("MoveY", lastDirection.y);
            animator.SetBool("IsMoving", true);
        }
        else
        {
            animator.SetBool("IsMoving", false);
        }

        // Arrived at target point
        if (Vector3.Distance(transform.position, targetPoint.position) < 0.05f)
        {
            transform.position = targetPoint.position;
            isWaiting = true;
            waitTimer = waitTimeAtPoint;
        }
    }

    private void AdvanceToNextPoint()
    {
        if (pingpong)
        {
            if (currentPointIndex + direction >= patrolPoints.Length || currentPointIndex + direction < 0)
                direction *= -1;

            currentPointIndex += direction;
        }
        else
        {
            currentPointIndex++;

            if (currentPointIndex >= patrolPoints.Length)
            {
                currentPointIndex = loop ? 0 : patrolPoints.Length - 1;
            }
        }
    }
}
