using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class movetoplay : MonoBehaviour
{
        public void StartGame()
    {
        SceneManager.LoadScene("Play");
    }
}

