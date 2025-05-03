using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance;
    public string spawnPointName;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Persist between scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetSpawnPoint(string name)
    {
        spawnPointName = name;
    }
}
