using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ActionProperty : MonoBehaviour
{
    public InputActionReference InputAction;
    public GameObject MapUI;
    public GameObject Player;
    public GameObject Map3D;
    // Start is called before the first frame update
    void Update(){
        if (InputAction.action.WasPressedThisFrame() && Map3D.activeInHierarchy == false){
            Debug.Log("Pressed!");
            MapUI.SetActive(!MapUI.activeSelf);
            MapUI.transform.position = Player.transform.position;
            //MainMenu.transform.rotation = Player.transform.rotation;
            //UI.SetActive(false);
            //UI.transform.position = Player.transform.position;
            //UI.transform.rotation = Player.transform.rotation;
        }
    }
}
