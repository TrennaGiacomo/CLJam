using UnityEngine;

public class YSortingPlayer : MonoBehaviour
{
    [SerializeField] private int sortingOffset = 0;

    private SpriteRenderer sr;

    private void Awake()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
    }

    private void LateUpdate()
    {
        sr.sortingOrder = -(int)(transform.position.y * 100) + sortingOffset;
    }
}