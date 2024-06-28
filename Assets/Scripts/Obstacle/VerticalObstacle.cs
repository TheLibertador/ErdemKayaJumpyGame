using UnityEngine;

public class VerticalObstacle : BaseObstacle
{
    public override void StartMovement()
    {
        MoveObstacle(Vector3.up);
        RotateObstacle();
    }

    void Start()
    {
        StartMovement();
    }
}
