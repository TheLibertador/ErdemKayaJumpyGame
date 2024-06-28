using UnityEngine;
using DG.Tweening;

public abstract class BaseObstacle : MonoBehaviour
{
    protected float moveDistance = 8f;
    protected float moveDuration = 3f;
    protected float rotationSpeed = 360f;

    public abstract void StartMovement();

    protected void MoveObstacle(Vector3 direction)
    {
        Vector3 endPosition = transform.position + direction * moveDistance;
        transform.DOMove(endPosition, moveDuration).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
    }

    protected void RotateObstacle()
    {
        transform.DORotate(Vector3.forward * rotationSpeed, 1f, RotateMode.FastBeyond360)
                 .SetLoops(-1, LoopType.Incremental)
                 .SetEase(Ease.Linear);
    }
}
