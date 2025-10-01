using System.Collections;
using UnityEngine;

public class Cutscene : MonoBehaviour
{
    [SerializeField] float cutsceneLength;
    [SerializeField] AudioSource cutsceneDialogue;
    [SerializeField] GameObject cutscene;
    [SerializeField] TextScrolling dialogueTextScroller;
    [SerializeField] string dialogueText;

    public bool playingCutscene;

    /// <summary>
    /// Plays the cutscene for the desired length, sets playingCutscene to false when it is finished
    /// </summary>
    IEnumerator PlayGameCutscene() {
        //Activating each component
        playingCutscene = true;
        cutscene.SetActive(true);
        if (cutsceneDialogue != null) {
            cutsceneDialogue.Play();
        }
        if (dialogueTextScroller != null) {
            dialogueTextScroller.StartLerp(dialogueText, cutsceneLength - 1);
        }
        yield return new WaitForSeconds(cutsceneLength + 2);
        //Deactivating each component
        cutscene.SetActive(false);
        playingCutscene = false;
    }

    public void PlayCutscene() {
        StartCoroutine(PlayGameCutscene());
    }
}
