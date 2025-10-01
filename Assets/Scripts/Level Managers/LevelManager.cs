using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] GameObject[] levels;
    [SerializeField] AudioSource fanfare;
    [SerializeField] GameObject[] uiObjectives;
    [SerializeField] GameObject[] levelPointers;

    public enum level {
        First,
        Second
    }
    public level currentLevel;

    public void StartLevel() {
        levels[(int)currentLevel].GetComponent<Level>().LevelStart();
        levelPointers[(int)currentLevel].SetActive(false);
        uiObjectives[(int)currentLevel].SetActive(true);
    }

    public void LevelWon() {
        fanfare.Play();
        levels[(int)currentLevel].GetComponent<Level>().LevelWin();
        uiObjectives[(int)currentLevel].SetActive(false);
        if (currentLevel == level.First) {
            currentLevel++;
            levelPointers[(int)currentLevel].SetActive(true);
        }
    }

    public void LevelFailed() {
        levels[(int)currentLevel].GetComponent<Level>().LevelLose();
        levelPointers[(int)currentLevel].SetActive(true);
        uiObjectives[(int)currentLevel].SetActive(false);
    }
}
