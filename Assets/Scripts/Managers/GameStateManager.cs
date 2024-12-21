using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance;

    [Header("Progress Flags")]
    public bool finalDoorUnlocked = false;
    public bool MapCollected = false;
    public DialogueSequence myDialogueSequenceAsset;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
    // By this time, StoryManager.Instance should be ready
    StoryManager.Instance.PlayDialogueSequence(myDialogueSequenceAsset);
    }

    public void MapToggler(){
        MapCollected = true;
    }


    public void CheckProgress()
    {
        // Example: If all certain puzzles are solved, unlock final door
        // This could be more complex: checking a list of required puzzle names
        if (PuzzleManager.Instance.IsSolved("PuzzleA") && PuzzleManager.Instance.IsSolved("PuzzleB"))
        {
            finalDoorUnlocked = true;
            // Trigger door unlock logic, or UI update
        }
    }
}
