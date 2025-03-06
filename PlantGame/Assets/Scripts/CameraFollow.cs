using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraFollow : MonoBehaviour
{
    // Private variables (not visible in the Inspector)
    private Transform cameraTarget; // Target for the camera to follow (usually the player)
    private float cameraStart; // Start boundary for camera movement
    private float cameraEnd; // End boundary for camera movement
    private Vector3 velocity = Vector3.zero; // Velocity reference for SmoothDamp
    private Vector3 originalPosition; // Original position of the camera
    private float originalZoom; // Original zoom level of the camera
    private bool isGreenhouseLevel = false; // Flag to check if the current level is the greenhouse

    // Enum to manage camera states
    private enum CameraState { Idle, ZoomingIn, ZoomingOut }
    private CameraState currentState = CameraState.Idle; // Current state of the camera

    private float smoothTime = 2f; // Time for smooth camera transitions
    private float targetZoom = 0.5f; // Target zoom level for the camera
    private float zoomVelocity=.5f; // Velocity reference for zoom smoothing

    private Vector3 specifiedLocation = new Vector3(23,(float)2.5, 0); // Specific location to check for the plant
    private GameObject plant; // Reference to the plant object
    public MonoBehaviour playerMovementScript; // Reference to the player's movement script

    void Start()
    {
        // Initialize camera target and original position/zoom
        plant = GameObject.FindGameObjectWithTag("p13");
        cameraTarget = GameObject.FindGameObjectWithTag("Player").transform;
        originalPosition = transform.position;
        originalZoom = Camera.main.orthographicSize;

        // Set camera boundaries based on the active scene
        if (SceneManager.GetActiveScene().name == "ForestLevel")
        {
            cameraStart = 3f;
            cameraEnd = 125f;
        }
        else
        {
            cameraStart = -80f;
            cameraEnd = 12f;
            isGreenhouseLevel = true; // Mark as greenhouse level
        }

        // Check if the plant is in the specified location and trigger zoom-in
        if (IsPlantInSpecifiedLocation())
        {
            currentState = CameraState.ZoomingIn;
            if (playerMovementScript != null)
            {
                playerMovementScript.enabled = false; // Disable player movement during zoom
            }
        }
    }

    void Update()
    {
        // Handle camera behavior based on its current state
        switch (currentState)
        {
            case CameraState.Idle:
                FollowCameraTargetHorizontally(); // Follow the player horizontally
                break;
            case CameraState.ZoomingIn:
                ZoomIn(); // Zoom in to the specified location
                break;
            case CameraState.ZoomingOut:
                ZoomOut(); // Zoom out to the original position
                break;
        }
    }

    private void ZoomIn()
    {
        // Smoothly move the camera to the specified location
        Vector3 targetPosition = new Vector3(
            specifiedLocation.x,
            specifiedLocation.y,
            -10f // Ensure z position stays at -10
        );
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

        // Smoothly zoom the camera to the target zoom level
        Camera.main.orthographicSize = Mathf.SmoothDamp(Camera.main.orthographicSize, targetZoom, ref zoomVelocity, smoothTime);

        // Check if the camera has reached the target position and zoom level
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f && Mathf.Abs(Camera.main.orthographicSize - targetZoom) < 0.1f)
        {
            currentState = CameraState.ZoomingOut; // Transition to zooming out
        }
    }

    private void ZoomOut()
    {
        // Smoothly move the camera back to its original position
        Vector3 targetPosition = new Vector3(
            originalPosition.x,
            originalPosition.y,
            -10f // Ensure z position stays at -10
        );
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

        // Smoothly zoom the camera back to its original zoom level
        Camera.main.orthographicSize = Mathf.SmoothDamp(Camera.main.orthographicSize, originalZoom, ref zoomVelocity, smoothTime);

        // Check if the camera has returned to the original position and zoom level
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f && Mathf.Abs(Camera.main.orthographicSize - originalZoom) < 0.1f)
        {
            currentState = CameraState.Idle; // Transition to idle state
            if (playerMovementScript != null)
            {
                playerMovementScript.enabled = true; // Re-enable player movement
            }
        }
    }

    private void FollowCameraTargetHorizontally()
    {
        Vector3 targetPosition = transform.position;

        // Adjust target position for greenhouse level
        if (isGreenhouseLevel)
        {
            targetPosition.y = cameraTarget.position.y + 3f; // Offset for vertical follow
            targetPosition.z = -10f; // Maintain camera depth
        }

        // Restrict camera movement within boundaries
        if (cameraTarget.position.x < cameraStart || cameraTarget.position.x >= cameraEnd)
        {
            targetPosition.x = transform.position.x; // Keep camera at current X position
        }
        else
        {
            targetPosition.x = cameraTarget.position.x; // Follow the target horizontally
        }

        transform.position = targetPosition;
    }

    public bool IsPlantInSpecifiedLocation()
    {
        // Check if the plant is near the specified location
        if (plant != null)
        {
            float distance = Vector3.Distance(plant.transform.position, specifiedLocation);
            return distance < 0.1f; // Return true if the plant is close enough
        }
        return false;
    }
}