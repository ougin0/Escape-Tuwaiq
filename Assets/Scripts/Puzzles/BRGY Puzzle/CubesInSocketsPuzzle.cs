using UnityEngine;
using UnityEngine.Events;

public class CubesInSocketsPuzzle : MonoBehaviour
{
    public SocketLogic[] sockets; // Assign all sockets here in the Inspector
    public PuzzleInstance puzzleInstance; // Link the associated PuzzleInstance

    void Start()
    {
        // Subscribe to each socket’s onSocketCorrect event
        foreach (var socket in sockets)
        {
            socket.onSocketCorrect.AddListener(CheckAllSockets);
        }
    }

    void CheckAllSockets()
    {
        // Check if all sockets are solved
        foreach (var socket in sockets)
        {
            if (!socket.IsSolved())
            {
                return; // If any not solved, puzzle not done
            }
        }

        // All sockets solved, mark the puzzle as solved via PuzzleInstance
        puzzleInstance.SolvePuzzle();
    }
}
