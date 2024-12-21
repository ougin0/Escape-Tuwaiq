using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DialogueSequence))]
public class DialogueSequenceEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        DialogueSequence seq = (DialogueSequence)target;
        if (seq.lines.Length != seq.voiceTracks.Length)
        {
            EditorGUILayout.HelpBox("Lines and voiceTracks arrays should match in length.", MessageType.Warning);
            if (GUILayout.Button("Match Length"))
            {
                System.Array.Resize(ref seq.voiceTracks, seq.lines.Length);
                EditorUtility.SetDirty(seq);
            }
        }
    }
}
