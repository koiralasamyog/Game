using UnityEngine;

public class SceneSpawnHandler : MonoBehaviour
{
    void Start()
    {
        // Ensure SpawnManager and spawnPointName are set properly
        if (SpawnManager.instance == null || string.IsNullOrEmpty(SpawnManager.instance.spawnPointName))
        {
            Debug.LogWarning("SpawnManager or spawn point name is missing!");
            return;
        }

        string baseName = SpawnManager.instance.spawnPointName;

        // Find the spawn points for both the player and camera pivot
        GameObject playerSpawn = GameObject.Find(baseName + "_Player");  // BossDoor_Player
        GameObject cameraPivotSpawn = GameObject.Find(baseName + "_CameraPivot");  // BossDoor_CameraPivot

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        GameObject cameraPivot = GameObject.FindGameObjectWithTag("CameraPivot");

        if (player != null && playerSpawn != null)
        {
            // Position the player at the spawn point
            player.transform.position = playerSpawn.transform.position;
            player.transform.rotation = playerSpawn.transform.rotation;
        }

        // Adjust the camera pivot (parent of the camera)
        if (cameraPivot != null && cameraPivotSpawn != null)
        {
            // Position the camera pivot at the spawn point
            cameraPivot.transform.position = cameraPivotSpawn.transform.position;
            cameraPivot.transform.rotation = cameraPivotSpawn.transform.rotation;
        }
    }
}
