using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportToSceneVR : MonoBehaviour
{
    [SerializeField] private string sceneName; // اسم المشهد المطلوب الانتقال إليه

    private void OnTriggerEnter(Collider other)
    {
        // تحقق إذا كان الكائن الذي دخل هو اللاعب أو أي جزء من جسمه
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}

