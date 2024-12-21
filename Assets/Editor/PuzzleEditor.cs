using UnityEngine;
using UnityEditor;

public class PuzzleEditor : EditorWindow
{
    string puzzleName = "NewPuzzle";
    string solvedSFX = "";

    [MenuItem("VR Tools/Create Puzzle")]
    static void Init()
    {
        PuzzleEditor window = (PuzzleEditor)EditorWindow.GetWindow(typeof(PuzzleEditor));
        window.Show();
    }

    void OnGUI()
    {
        GUILayout.Label("Create a New Puzzle", EditorStyles.boldLabel);
        puzzleName = EditorGUILayout.TextField("Puzzle Name:", puzzleName);
        solvedSFX = EditorGUILayout.TextField("Solved SFX Track:", solvedSFX);

        if (GUILayout.Button("Create Puzzle SO"))
        {
            var puzzleSO = ScriptableObject.CreateInstance<PuzzleDefinition>();
            puzzleSO.puzzleName = puzzleName;
            puzzleSO.solvedTrackName = solvedSFX;
            AssetDatabase.CreateAsset(puzzleSO, "Assets/Puzzles/" + puzzleName + ".asset");
            AssetDatabase.SaveAssets();
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = puzzleSO;
        }
    }
}
