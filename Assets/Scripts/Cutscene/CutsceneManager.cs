using UnityEngine;

public class CutsceneManager : MonoBehaviour
{
    [SerializeField] Cutscene[] cutscenes;
    int index;
    bool playing;

    private void Awake() {
        PlayCutscenes();
    }

    private void Update() {
        //Checks whether the current cutscene has stopped playing, if so, play the next cutscene
        if (index < cutscenes.Length) {
            playing = cutscenes[index].playingCutscene;
        }
        if (!playing) {
            index++;
            if (index < cutscenes.Length) {
                PlayCutscenes();
            }
        }
    }

    /// <summary>
    /// Calls the PlayCutscene function on the current cutscene
    /// </summary>
    void PlayCutscenes() {
        cutscenes[index].PlayCutscene();
    }
}
