using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform cameraTarget;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    public void FollowCameraTargetHorizontally()
    {
        if (cameraTarget.position.x < 2)
        {
            return;
        }

        Vector3 targetPosition = transform.position;
        if ((cameraTarget.position.x != targetPosition.x))
        {
            targetPosition.x = cameraTarget.position.x;
            transform.position = targetPosition;
        }
        else
        { transform.position = targetPosition; }

    }

    // Update is called once per frame
    void Update()
    {
        FollowCameraTargetHorizontally();

}
}
