using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AudioLibrary))]
public class AudioLibraryEditor : Editor
{
    private SerializedProperty tracksProp;

    void OnEnable()
    {
        tracksProp = serializedObject.FindProperty("tracks");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.LabelField("Audio Tracks", EditorStyles.boldLabel);

        for (int i = 0; i < tracksProp.arraySize; i++)
        {
            SerializedProperty trackProp = tracksProp.GetArrayElementAtIndex(i);
            SerializedProperty typeProp = trackProp.FindPropertyRelative("trackType");
            SerializedProperty nameProp = trackProp.FindPropertyRelative("trackName");
            SerializedProperty clipProp = trackProp.FindPropertyRelative("clip");
            SerializedProperty locationTypeProp = trackProp.FindPropertyRelative("locationType");
            SerializedProperty roomTransformProp = trackProp.FindPropertyRelative("roomTransform");

            SerializedProperty allowCustomTrackTypeProp = trackProp.FindPropertyRelative("allowCustomTrackType");

            SerializedProperty volumeProp = trackProp.FindPropertyRelative("volume");
            SerializedProperty pitchProp = trackProp.FindPropertyRelative("pitch");
            SerializedProperty spatialBlendProp = trackProp.FindPropertyRelative("spatialBlend");
            SerializedProperty minDistanceProp = trackProp.FindPropertyRelative("minDistance");
            SerializedProperty maxDistanceProp = trackProp.FindPropertyRelative("maxDistance");

            EditorGUILayout.BeginVertical("box");
            
            // Draw basic fields first
            EditorGUILayout.PropertyField(nameProp);
            EditorGUILayout.PropertyField(clipProp);

            EditorGUILayout.PropertyField(locationTypeProp);
            LocationType locType = (LocationType)locationTypeProp.enumValueIndex;

            // If scene-located
            if (locType == LocationType.SceneLocated)
            {
                // Show option to allow custom track type
                EditorGUILayout.PropertyField(allowCustomTrackTypeProp, new GUIContent("Allow Custom Track Type"));
                bool allowCustom = allowCustomTrackTypeProp.boolValue;

                if (!allowCustom)
                {
                    // Force SFX and grey out trackType selection
                    typeProp.enumValueIndex = (int)TrackType.SFX;
                    using (new EditorGUI.DisabledGroupScope(true))
                    {
                        EditorGUILayout.PropertyField(typeProp);
                    }
                }
                else
                {
                    // Allow changing trackType
                    EditorGUILayout.PropertyField(typeProp);
                }

                EditorGUILayout.PropertyField(roomTransformProp, new GUIContent("Room Transform"));

                EditorGUILayout.LabelField("Scene-Located Audio Settings", EditorStyles.boldLabel);
                EditorGUILayout.PropertyField(volumeProp, new GUIContent("Volume"));
                EditorGUILayout.PropertyField(pitchProp, new GUIContent("Pitch"));
                EditorGUILayout.PropertyField(spatialBlendProp, new GUIContent("Spatial Blend"));
                EditorGUILayout.PropertyField(minDistanceProp, new GUIContent("Min Distance"));
                EditorGUILayout.PropertyField(maxDistanceProp, new GUIContent("Max Distance"));

                if (roomTransformProp.objectReferenceValue != null)
                {
                    if (GUILayout.Button("Create Scene AudioSource"))
                    {
                        CreateSceneAudioSource((Transform)roomTransformProp.objectReferenceValue, nameProp.stringValue);
                    }
                }
                else
                {
                    EditorGUILayout.HelpBox("Assign a Room Transform to place the AudioSource.", MessageType.Info);
                }
            }
            else
            {
                // Player located, just show trackType normally
                EditorGUILayout.PropertyField(typeProp);
            }

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Remove Track"))
            {
                tracksProp.DeleteArrayElementAtIndex(i);
                break; // re-draw after removal
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();

            EditorGUILayout.Space();
        }

        EditorGUILayout.Space();
        if (GUILayout.Button("Add New Track"))
        {
            tracksProp.InsertArrayElementAtIndex(tracksProp.arraySize);
            SerializedProperty newTrack = tracksProp.GetArrayElementAtIndex(tracksProp.arraySize - 1);
            newTrack.FindPropertyRelative("trackName").stringValue = "NewTrack";
            newTrack.FindPropertyRelative("locationType").enumValueIndex = (int)LocationType.PlayerLocated;
        }

        serializedObject.ApplyModifiedProperties();
    }

    private void CreateSceneAudioSource(Transform roomTransform, string trackName)
    {
        GameObject audioObj = new GameObject("AudioSource_" + trackName);
        audioObj.transform.position = roomTransform.position; // Place in the middle of the room
        audioObj.AddComponent<AudioSource>(); // Add the audio source

        EditorGUIUtility.PingObject(audioObj);
        Debug.Log("Created AudioSource in scene for track: " + trackName);
    }
}
