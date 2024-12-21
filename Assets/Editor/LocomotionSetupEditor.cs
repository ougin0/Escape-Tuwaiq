using UnityEngine;
using UnityEditor;
using UnityEngine.XR.Interaction.Toolkit;
using Unity.XR.CoreUtils;

public class LocomotionSetupEditor : EditorWindow
{
    [MenuItem("VR Tools/Setup Locomotion")]
    static void Init()
    {
        LocomotionSetupEditor window = (LocomotionSetupEditor)EditorWindow.GetWindow(typeof(LocomotionSetupEditor));
        window.Show();
    }

    void OnGUI()
    {
        GUILayout.Label("Locomotion Setup", EditorStyles.boldLabel);
        if (GUILayout.Button("Add Teleportation to Scene"))
        {
            var xrRig = FindObjectOfType<XROrigin>();
            if (xrRig && xrRig.GetComponent<LocomotionSystem>() == null)
            {
                xrRig.gameObject.AddComponent<LocomotionSystem>();
                xrRig.gameObject.AddComponent<TeleportationProvider>();
                EditorUtility.DisplayDialog("Success", "Locomotion System Added!", "OK");
            }
            else
            {
                EditorUtility.DisplayDialog("Info", "No XR Rig found or Locomotion already exists.", "OK");
            }
        }
    }
}
