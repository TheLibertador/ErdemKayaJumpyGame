using UnityEngine;

public class HorizontalObstacle : BaseObstacle
{
    public override void StartMovement()
    {
        MoveObstacle(Vector3.right);
        RotateObstacle();
    }

    void Start()
    {
        StartMovement();
    }
}
