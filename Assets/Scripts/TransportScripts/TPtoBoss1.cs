using UnityEngine;
using UnityEngine.SceneManagement;

public class TPtoBoss1 : MonoBehaviour
{
    [Header("Scene to Load Automatically")]
    public string sceneToLoad = "BossArena1";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered the trigger zone. Loading scene: " + sceneToLoad);
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
