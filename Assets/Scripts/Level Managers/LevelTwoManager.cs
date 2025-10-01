using UnityEngine;
using UnityEngine.InputSystem;

public class LevelTwoManager : MonoBehaviour, Level
{
    //Object References
    [SerializeField] Talking beaver;
    [SerializeField] SplitScreenPlayerManager playerManager;
    [SerializeField] GameObject objectiveBox;
    [SerializeField] GameObject cutscene;
    [SerializeField] TextScrolling text;
    [SerializeField] Log log;
    //Timer
    [Header("Timer")]
    [SerializeField] Timer timer;
    [SerializeField] int minutes;
    [SerializeField] int seconds;

    /// <summary>
    /// Allows the players to chop down the tree, sets the objective active as well
    /// </summary>
    public void LevelStart() { 
        //Gives players the axe
        foreach(PlayerInput player in playerManager.players) {
            player.GetComponent<Interactor>().GiveAxe();
        }
        //Sets the objective active
        objectiveBox.SetActive(true);

        //Starts the timer
        timer.gameObject.SetActive(true);
        timer.StartTimer(minutes, seconds);
    }
    public void LevelWin() {
        cutscene.SetActive(true);
        text.StartLerp("Thank you for playing!!", 6);
    }

    /// <summary>
    /// Resets the level if the player does not complete it in time
    /// </summary>
    public void LevelLose() {
        beaver.ResetLevel();
        ResetLevel();
    }

    /// <summary>
    /// Deactivates all objective objects
    /// </summary>
    void ResetLevel() {
        objectiveBox.SetActive(false);
        timer.gameObject.SetActive(false);
        log.ResetLog();
    }
}
