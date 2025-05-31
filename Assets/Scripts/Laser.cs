using UnityEngine;

public class Laser : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float rayLength = 4.5f;
    [SerializeField] private Transform rayOrigin;
    [SerializeField] private LayerMask targetLayer; //Player layer
    [SerializeField] private Vector2 facingDir;

    private bool isEnabled = true;

    private void Update()
    {
        if (!isEnabled) return;

        RaycastHit2D hit = Physics2D.Raycast(rayOrigin.position, facingDir, rayLength, targetLayer);

        if (hit.collider != null && hit.collider.TryGetComponent(out PlayerInteraction playerInteraction))
        {
            GameManager.Instance.EndGame();
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (rayOrigin == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawLine(rayOrigin.position, rayOrigin.position + (Vector3)(facingDir.normalized * rayLength));
    }
}