using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelOneManager : MonoBehaviour, Level
{
    //Object References
    [Header("Object References")]
    [SerializeField] Talking mayor;
    [SerializeField] GameObject brokenBridge, fixedBridge;
    [SerializeField] GameObject bench;
    [SerializeField] Transform respawnPoints;
    [SerializeField] GameObject cutscenes;
    [SerializeField] GameObject house;

    //Objective
    [Header("Objective")]
    [SerializeField] GameObject objectiveBox;
    [SerializeField] MeshRenderer objectivePointer;
    //Timer
    [Header("Timer")]
    [SerializeField] Timer timer;
    [SerializeField] int minutes;
    [SerializeField] int seconds;

    /// <summary>
    /// Sets the objective, the UI and the bench active
    /// </summary>
    public void LevelStart() {
        bench.SetActive(true);

        objectiveBox.SetActive(true);
        objectivePointer.enabled = true;

        timer.gameObject.SetActive(true);
        timer.StartTimer(minutes, seconds);
    }

    /// <summary>
    /// Sets the fixed bridge active so the player can cross to the next level
    /// </summary>
    public void LevelWin() {
        brokenBridge.SetActive(false);
        fixedBridge.SetActive(true);
        cutscenes.SetActive(true);
        house.SetActive(true);
        objectivePointer.enabled = false;
        ResetLevel();

        respawnPoints.position = new Vector3(-88.6f, 11.49841f, 16.5f);
        mayor.gameObject.layer = 0;
    }

    /// <summary>
    /// Resets the level if the level is lost
    /// </summary>
    public void LevelLose() {
        mayor.ResetLevel();
        ResetLevel();
    }

    /// <summary>
    /// Deactivates all objective objects
    /// </summary>
    void ResetLevel() {
        objectiveBox.SetActive(false);
        objectivePointer.enabled = false;
        timer.gameObject.SetActive(false);

        bench.GetComponent<Log>().ResetLog();
    }
}
