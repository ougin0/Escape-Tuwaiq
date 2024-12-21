using UnityEngine;

[CreateAssetMenu(fileName = "DialogueSequence", menuName = "Narrative/Dialogue Sequence")]
public class DialogueSequence : ScriptableObject
{
    [TextArea(2,5)]
    public string[] lines;
    public string[] voiceTracks; // same length as lines for voice clips
}
