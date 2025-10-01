using UnityEngine;

public class RespawnTrigger : MonoBehaviour
{
    public Log log, bench;
    public SplitScreenPlayerManager playerManager;
    public Transform spawnPos1, spawnPos2;

    /// <summary>
    /// Resets the player and objective objects if a player falls off the map
    /// </summary>
    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player") {
            //Resetting the player
            Debug.Log("Respawned");
            other.GetComponent<ThirdPersonController>().Respawn();

            //Resetting the log
            log.ResetLog();
            bench.ResetLog();
        }
    }
}
