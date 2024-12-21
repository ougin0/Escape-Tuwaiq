using UnityEngine;

public class CanvasFollowsCamera : MonoBehaviour
{
    public Transform xrCamera;            // Assign the XR/VR camera in the Inspector
    public float forwardDistance = 2.0f;  // How far in front of the camera
    public float verticalOffset = -0.5f;  // Height adjustment (negative = lower than eye-level)
    public Vector3 rotationOffsetEuler = Vector3.zero; // Additional rotation offsets if needed

    void Update()
    {
        if (xrCamera == null) return;

        // Calculate a forward direction ignoring camera tilt if you don't want to go "down"
        Vector3 forward = xrCamera.forward;
        forward.y = 0; // Flatten the forward vector so the UI doesn't tilt down/up if camera looks up/down
        forward.Normalize();

        // Position the UI at a fixed forward and vertical offset from the camera
        Vector3 targetPosition = xrCamera.position + forward * forwardDistance + Vector3.up * verticalOffset;

        // Rotate UI to face camera direction, plus any specified rotation offset
        Quaternion targetRotation = Quaternion.LookRotation(forward, Vector3.up) * Quaternion.Euler(rotationOffsetEuler);

        // Apply computed transform
        transform.position = targetPosition;
        transform.rotation = targetRotation;
    }
}
