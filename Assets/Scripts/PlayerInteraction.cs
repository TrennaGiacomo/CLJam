using UnityEngine;
using DG.Tweening;
using System.Linq;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Input & Raycast")]
    [SerializeField] private KeyCode interactKey = KeyCode.E;
    [SerializeField] private float interactRange = 1.5f;
    [SerializeField] private LayerMask interactableLayer;

    [Header("References")]
    [SerializeField] private Transform lookDirectionOrigin;
    [SerializeField] private Vector2 facingDirection = Vector2.down;
    [SerializeField] private GameObject helperText;

    private IInteractable currentTarget;
    private Transform currentTargetTransform;
    private Tween helperTween;
    private Tween targetTween;

    private void Update()
    {
        CheckForInteractables();

        if (Input.GetKeyDown(interactKey) && currentTarget != null)
        {
            currentTarget.Interact();
            StopAllBreathing();
        }
    }

    private void CheckForInteractables()
    {
        Vector2 origin = lookDirectionOrigin ? lookDirectionOrigin.position : transform.position;
        RaycastHit2D hit = Physics2D.Raycast(origin, facingDirection, interactRange, interactableLayer);

        if (hit.collider != null && hit.collider.TryGetComponent(out IInteractable interactable))
        {
            if (interactable != currentTarget)
            {
                if(interactable.CanInteract)
                    SetNewTarget(interactable, hit.collider.transform);
            }

            return;
        }

        ClearTarget(); // No interactable directly ahead
    }


    private void SetNewTarget(IInteractable interactable, Transform targetTransform)
    {
        StopAllBreathing();

        currentTarget = interactable;
        currentTargetTransform = targetTransform;

        // Show and animate helper
        helperText.SetActive(true);
        helperTween = helperText.transform
            .DOScale(0.6f, 0.5f)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo);

        // Animate the interactable (optional, assuming it's a transform you can scale)
        if (currentTargetTransform != null)
        {
            targetTween = currentTargetTransform
                .DOScale(1.1f, 0.5f)
                .SetEase(Ease.InOutSine)
                .SetLoops(-1, LoopType.Yoyo);
        }
    }

    private void ClearTarget()
    {
        if (currentTarget != null)
        {
            StopAllBreathing();
            currentTarget = null;
            currentTargetTransform = null;
        }
    }

    private void StopAllBreathing()
    {
        helperTween?.Kill();
        helperText.SetActive(false);

        targetTween?.Kill();
        if (currentTargetTransform != null)
            currentTargetTransform.localScale = Vector3.one;
    }

    private void OnDrawGizmosSelected()
    {
        Vector2 origin = lookDirectionOrigin ? lookDirectionOrigin.position : transform.position;
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(origin, origin + facingDirection.normalized * interactRange);
        Gizmos.DrawWireSphere(origin, interactRange);
    }

    public void SetFacingDirection(Vector2 dir)
    {
        if (dir.sqrMagnitude > 0.01f)
            facingDirection = dir.normalized;
    }
}
