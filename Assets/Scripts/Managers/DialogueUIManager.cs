using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;  // Import TextMeshPro namespace

public class DialogueUIManager : MonoBehaviour
{
    public TMP_Text dialogueText;    // TMP_Text instead of UnityEngine.UI.Text
    public Button nextButton;        // Advances to the next line
    public Button previousButton;    // Goes back to the previous line
    public Button closeButton;       // Closes the dialogue UI

    private DialogueSequence currentSeq;
    private int currentIndex = -1;

    void Start()
    {
        SetUIActive(false);
        if (nextButton != null) nextButton.onClick.AddListener(NextLine);
        if (previousButton != null) previousButton.onClick.AddListener(PreviousLine);
        if (closeButton != null) closeButton.onClick.AddListener(CloseDialogue);
    }

    public void StartDialogue(DialogueSequence seq)
    {
        currentSeq = seq;
        currentIndex = -1;
        SetUIActive(true);
        NextLine(); // Begin from the first line
    }

    void NextLine()
    {
        if (currentSeq == null) return;

        currentIndex++;
        if (currentIndex >= currentSeq.lines.Length)
        {
            CloseDialogue();
            return;
        }

        ShowLine(currentIndex);
    }

    void PreviousLine()
    {
        if (currentSeq == null) return;

        currentIndex--;
        if (currentIndex < 0)
        {
            currentIndex = 0;
            return; // Already at the first line
        }

        ShowLine(currentIndex);
    }

    void ShowLine(int index)
    {
        dialogueText.text = currentSeq.lines[index];

        // Play associated voice line if any
        if (currentSeq.voiceTracks != null && index < currentSeq.voiceTracks.Length && !string.IsNullOrEmpty(currentSeq.voiceTracks[index]))
        {
            SoundManager.Instance.PlayTrack(TrackType.Dialogue, currentSeq.voiceTracks[index]);
        }
    }

    void CloseDialogue()
    {
        SetUIActive(false);
        currentSeq = null;
    }

    void SetUIActive(bool active)
    {
        gameObject.SetActive(active);
    }
}
