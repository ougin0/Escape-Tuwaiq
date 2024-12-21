using UnityEngine;

[CreateAssetMenu(fileName = "PuzzleDefinition", menuName = "Puzzles/Puzzle Definition")]
public class PuzzleDefinition : ScriptableObject
{
    public string puzzleName;
    public string[] requiredItems; // items needed to solve (optional)
    public string solvedTrackName; // sound track to play when solved
}
