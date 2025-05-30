using UnityEngine;

public class GuardPatrol : MonoBehaviour
{
    [Header("Patrol Path")]
    [SerializeField] private Transform[] patrolPoints;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float waitTimeAtPoint = 1f;
    [SerializeField] private bool loop = true;
    [SerializeField] private bool pingpong = false;

    public Vector2 CurrentMoveDirection => lastDirection;

    private int currentPointIndex = 0;
    private float waitTimer = 0f;
    private bool isWaiting = false;
    private int direction = 1; // 1 = forward, -1 = backward (for pingpong)
    private Vector2 lastDirection = Vector2.right;

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
            return;
        }

        Transform targetPoint = patrolPoints[currentPointIndex];
        Vector3 directionToTarget = targetPoint.position - transform.position;
        Vector3 move = directionToTarget.normalized * moveSpeed * Time.deltaTime;

        // Clamp movement to avoid overshooting
        if (move.magnitude > directionToTarget.magnitude)
            move = directionToTarget;

        transform.position += move;

        // Update last move direction if moving
        if (move.sqrMagnitude > 0.0001f)
            lastDirection = directionToTarget.normalized;

        // Flip sprite only if moving mostly horizontally
        if (Mathf.Abs(lastDirection.x) > Mathf.Abs(lastDirection.y))
        {
            transform.localScale = new Vector3(Mathf.Sign(lastDirection.x), 1, 1);
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
