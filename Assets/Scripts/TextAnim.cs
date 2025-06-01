using UnityEngine;
using DG.Tweening;

public class TextAnim : MonoBehaviour
{
    void Start()
    {
        gameObject.transform
            .DOScale(Vector3.one * 1.05f, 1f)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo);
    }
}
