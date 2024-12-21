using UnityEngine;
using System.Collections.Generic;

public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager Instance;
    private HashSet<string> solvedPuzzles = new HashSet<string>();

    void Awake()
    {
        Instance = this;
    }

    public void MarkSolved(PuzzleDefinition def)
    {
        solvedPuzzles.Add(def.puzzleName);

        // Play sound if needed
        if (!string.IsNullOrEmpty(def.solvedTrackName))
        {
            SoundManager.Instance.PlayTrack(TrackType.SFX, def.solvedTrackName);
        }

        // Trigger global events, e.g. check if all puzzles solved.
        GameStateManager.Instance.CheckProgress();
    }

    public bool IsSolved(string puzzleName) => solvedPuzzles.Contains(puzzleName);
}
