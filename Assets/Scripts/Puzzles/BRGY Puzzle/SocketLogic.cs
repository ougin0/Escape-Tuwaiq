using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Events;

public class SocketLogic : MonoBehaviour
{
    public string expectedCubeID; // Assign in Inspector: e.g. "RedCube"
    public UnityEvent onSocketCorrect; // Called when correct cube placed

    private XRSocketInteractor socket;
    private bool isSolved = false;

    void Awake()
    {
        socket = GetComponent<XRSocketInteractor>();
        // Subscribe to when an object is placed
        socket.selectEntered.AddListener(OnItemPlaced);
        socket.selectExited.AddListener(OnItemRemoved);
    }

    void OnItemPlaced(SelectEnterEventArgs args)
    {
        if (isSolved) return; // Already solved, no need to re-check

        // Check if the placed object has the correct CubeID
        var placedObject = args.interactableObject.transform.gameObject;
        var cubeID = placedObject.GetComponent<CubeID>();
        if (cubeID != null && cubeID.cubeID == expectedCubeID)
        {
            isSolved = true;
            onSocketCorrect.Invoke();
        }
    }

    void OnItemRemoved(SelectExitEventArgs args)
    {
        // If the correct cube is removed, we’re no longer solved
        var placedObject = args.interactableObject.transform.gameObject;
        var cubeID = placedObject.GetComponent<CubeID>();
        if (cubeID != null && cubeID.cubeID == expectedCubeID)
        {
            isSolved = false;
            // If desired, notify puzzle we’re unsolved now.
        }
    }

    public bool IsSolved()
    {
        return isSolved;
    }
}
