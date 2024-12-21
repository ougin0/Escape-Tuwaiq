using UnityEngine;
using UnityEngine.Events;

public class PuzzleInstance : MonoBehaviour
{
    public PuzzleDefinition definition;
    public UnityEvent onPuzzleSolved;

    public void SolvePuzzle()
    {
        onPuzzleSolved.Invoke();
    }
}
