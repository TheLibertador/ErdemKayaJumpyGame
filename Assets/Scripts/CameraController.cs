using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform playerTransform; // The character to follow, assignable in the Inspector
    private float smoothSpeed = 0.5f; // The speed of the smooth follow
    
    private void Start()
    {
        if(GameManager.Instance != null)
        {
            playerTransform = GameManager.Instance.player.transform;
        }
    }

    private void LateUpdate()
    {
        if (playerTransform != null)
        {
            // Desired position only changing in Z axis
            Vector3 desiredPosition = new Vector3(transform.position.x, transform.position.y, playerTransform.position.z);
            // Smoothly move the camera towards the desired position using Lerp
            transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        }
    }
}
