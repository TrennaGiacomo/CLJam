using UnityEngine;

public class GuardPatrol : MonoBehaviour
{
    [Header("Patrol Path")]
    [SerializeField] private Transform[] patrolPoints;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float waitTimeAtPoint = 1f;
    [SerializeField] private bool loop = true;
    [SerializeField] private bool pingpong = false;

    private int currentPointIndex = 0;
    private float waitTimer = 0f;
    private bool isWaiting = false;
    private int direction = 1; //1 = forward, -1 = backward (so we can pingpong)

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

        var targetPoint = patrolPoints[currentPointIndex];
        var directionToTarget = (targetPoint.position - transform.position).normalized;
        transform.position += moveSpeed * Time.deltaTime * directionToTarget;

        //face movement direction
        if (Mathf.Abs(directionToTarget.x) > Mathf.Abs(directionToTarget.y))
            transform.localScale = new Vector3(Mathf.Sign(directionToTarget.x), 1, 1);

        //reached point?
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
                if (loop)
                    currentPointIndex = 0;
                else
                    currentPointIndex = patrolPoints.Length - 1;
            }
        }
    }
}
