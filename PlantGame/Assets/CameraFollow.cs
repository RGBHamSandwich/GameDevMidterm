using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform cameraTarget;
    public float camerashake;
    public float cameraStart;
    public float cameraEnd;
    public Vector3 specifiedLocation; // The specified location to check
    public float smoothTime = 0.3f; // Time for the camera to reach the target
    private Vector3 velocity = Vector3.zero; // Velocity reference for SmoothDamp
    private enum CameraState { Idle, ZoomingIn, ZoomingOut }
    private CameraState currentState = CameraState.Idle; // Current state of the camera
    public float targetZoom;
    public float zoomVelocity;
    public GameObject plant; // Reference to the plant object
    public MonoBehaviour playerMovementScript; // Reference to the player's movement script
    private Vector3 originalPosition;
    private float originalZoom;

    void Start()
    {
        originalPosition = transform.position;
        originalZoom = Camera.main.orthographicSize;

        if (IsPlantInSpecifiedLocation())
        {
            currentState = CameraState.ZoomingIn;
            if (playerMovementScript != null)
            {
                playerMovementScript.enabled = false; // Disable player movement
            }
        }
    }

    void Update()
    {
        switch (currentState)
        {
            case CameraState.Idle:
                FollowCameraTargetHorizontally();
                break;
            case CameraState.ZoomingIn:
                ZoomIn();
                break;
            case CameraState.ZoomingOut:
                ZoomOut();
                break;
        }
    }

    private void ZoomIn()
    {
        // Smoothly pan the camera
        transform.position = Vector3.SmoothDamp(transform.position, specifiedLocation, ref velocity, smoothTime);

        // Smoothly zoom the camera
        Camera.main.orthographicSize = Mathf.SmoothDamp(Camera.main.orthographicSize, targetZoom, ref zoomVelocity, smoothTime);

        // Check if the camera has reached the specified location and zoom level
        if (Vector3.Distance(transform.position, specifiedLocation) < 0.1f && Mathf.Abs(Camera.main.orthographicSize - targetZoom) < 0.1f)
        {
            currentState = CameraState.ZoomingOut; // Transition to zooming out
        }
    }

    private void ZoomOut()
    {
        // Smoothly pan the camera back to the original position
        transform.position = Vector3.SmoothDamp(transform.position, originalPosition, ref velocity, smoothTime);

        // Smoothly zoom the camera back to the original zoom level
        Camera.main.orthographicSize = Mathf.SmoothDamp(Camera.main.orthographicSize, originalZoom, ref zoomVelocity, smoothTime);

        // Check if the camera has reached the original position and zoom level
        if (Vector3.Distance(transform.position, originalPosition) < 0.1f && Mathf.Abs(Camera.main.orthographicSize - originalZoom) < 0.1f)
        {
            currentState = CameraState.Idle; // Transition to idle
            if (playerMovementScript != null)
            {
                playerMovementScript.enabled = true; // Re-enable player movement
            }
        }
    }

    public void FollowCameraTargetHorizontally()
    {
        if (cameraTarget.position.x < cameraStart || cameraTarget.position.x >= cameraEnd)
        {
            return;
        }
        if (Mathf.Abs(cameraTarget.position.x - transform.position.x) < camerashake)
        {
            return; // Don't move if the camera is almost in the target's position
        }

        Vector3 targetPosition = transform.position;

        if ((cameraTarget.position.x != targetPosition.x))
        {
            targetPosition.x = cameraTarget.position.x;
            transform.position = targetPosition;
        }
        else
        {
            transform.position = targetPosition;
        }
    }

    public bool IsPlantInSpecifiedLocation()
    {
        if (plant != null)
        {
            float distance = Vector3.Distance(plant.transform.position, specifiedLocation);
            return distance != 0;
        }
        return false;
    }
}
