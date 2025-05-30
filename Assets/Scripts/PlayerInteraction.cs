using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private KeyCode interactKey = KeyCode.E;
    [SerializeField] private float interactRange = 1.5f;
    [SerializeField] private LayerMask interactableLayer;

    [SerializeField] private Transform lookDirectionOrigin; 
    [SerializeField] private Vector2 facingDirection = Vector2.down; 

    private void Update()
    {
        // OPTIONAL: Update facingDirection based on last move input
        // facingDirection = ...

        if (Input.GetKeyDown(interactKey))
        {
            Vector2 origin = lookDirectionOrigin ? lookDirectionOrigin.position : transform.position;
            RaycastHit2D hit = Physics2D.Raycast(origin, facingDirection, interactRange, interactableLayer);

            if (hit.collider != null && hit.collider.TryGetComponent(out IInteractable interactable))
            {
                interactable.Interact();
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Vector2 origin = lookDirectionOrigin ? lookDirectionOrigin.position : transform.position;
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(origin, origin + facingDirection.normalized * interactRange);
    }

    // Call this from movement script when player moves
    public void SetFacingDirection(Vector2 dir)
    {
        if (dir.sqrMagnitude > 0.01f)
            facingDirection = dir.normalized;
    }
}
