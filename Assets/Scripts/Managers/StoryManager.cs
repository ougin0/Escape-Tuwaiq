using UnityEngine;

public class StoryManager : MonoBehaviour
{
    public static StoryManager Instance;
    public DialogueUIManager dialogueUIManager;

    void Awake()
    {
        Instance = this;
    }

    public void PlayDialogueSequence(DialogueSequence seq)
    {
        dialogueUIManager.StartDialogue(seq);
    }
}
