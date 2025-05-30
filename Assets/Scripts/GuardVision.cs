using UnityEngine;

public class GuardVision : MonoBehaviour
{
    [Header("Detection Settings")]
    [SerializeField] private float viewRadius = 3f;
    [SerializeField] [Range(0, 360)] private float viewAngle = 90f;
    [SerializeField] private LayerMask targetMask; //Player layer
    [SerializeField] private LayerMask obstacleMask; //Walls/props

    [Header("References")]
    [SerializeField] private Transform visionOrigin;

    private GuardPatrol guardPatrol;

    public bool CanSeePlayer { get; private set; }

    void Awake()
    {
        guardPatrol = GetComponent<GuardPatrol>();
    }

    private void Update()
    {
        CanSeePlayer = false;

        Collider2D[] targets = Physics2D.OverlapCircleAll(visionOrigin.position, viewRadius, targetMask);

        foreach (Collider2D target in targets)
        {
            Vector2 dirToTarget = (target.transform.position - visionOrigin.position).normalized;
            Vector2 forward = guardPatrol.CurrentMoveDirection;
            if (forward.sqrMagnitude < 0.1f) forward = Vector2.right; // fallback default
            float angleToTarget = Vector2.Angle(forward, dirToTarget);


            if (angleToTarget < viewAngle / 2f)
            {
                float distanceToTarget = Vector2.Distance(visionOrigin.position, target.transform.position);


                RaycastHit2D hit = Physics2D.Raycast(visionOrigin.position, dirToTarget, distanceToTarget, obstacleMask);
                if (!hit)
                {
                    CanSeePlayer = true;
                    break;
                }
            }
        }

        if (CanSeePlayer)
        {
            Debug.Log("Player spotted!");
            // TODO: Call alarm, end game, chase player, etc.
        }
    }

    //Editor Only
    private void OnDrawGizmosSelected()
    {
        if (!visionOrigin) return;

        Gizmos.color = Color.yellow;

        Vector2 forward = Application.isPlaying && guardPatrol != null
            ? guardPatrol.CurrentMoveDirection
            : Vector2.right;

        float angleOffset = Mathf.Atan2(forward.y, forward.x) * Mathf.Rad2Deg;

        int segments = 30;
        float startAngle = -viewAngle / 2f + angleOffset;
        float endAngle = viewAngle / 2f + angleOffset;

        Vector3 origin = visionOrigin.position;
        Vector3 prevPoint = origin + DirFromAngle(startAngle) * viewRadius;

        Gizmos.color = new Color(1f, 1f, 0f, 0.25f);

        for (int i = 1; i <= segments; i++)
        {
            float angle = Mathf.Lerp(startAngle, endAngle, i / (float)segments);
            Vector3 nextPoint = origin + DirFromAngle(angle) * viewRadius;

            Gizmos.DrawLine(origin, prevPoint);
            Gizmos.DrawLine(prevPoint, nextPoint);
            Gizmos.DrawLine(nextPoint, origin);

            prevPoint = nextPoint;
        }

        Gizmos.color = Color.yellow;
        Vector3 leftDir = DirFromAngle(startAngle);
        Vector3 rightDir = DirFromAngle(endAngle);
        Gizmos.DrawLine(origin, origin + leftDir * viewRadius);
        Gizmos.DrawLine(origin, origin + rightDir * viewRadius);
    }

    private Vector3 DirFromAngle(float angleDegrees)
    {
        float rad = angleDegrees * Mathf.Deg2Rad;
        return new Vector3(Mathf.Cos(rad), Mathf.Sin(rad));
    }

}
