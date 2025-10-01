using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Talking : MonoBehaviour, IInteractable
{
    //Dialogue
    [Header("Dialogue")]
    [SerializeField] GameObject dialogueBox;
    [SerializeField] AudioSource speechAudio;
    [SerializeField] float dialogueDuration;
    //Dialogue Text
    [SerializeField] TextScrolling textUI;
    [SerializeField] string dialogueText;

    [SerializeField] CanvasGroup ObjectiveUI;
    [SerializeField] CanvasGroup questMarker;

    [Header("Level Manager Reference")]
    [SerializeField] LevelManager levelManager;

    bool interacted;

    /// <summary>
    /// Will be called if the player interacts with the NPC
    /// Sets interacted to true so player can't interact again
    /// Starts the dialogue 
    /// </summary>
    public void Interact(Interactor interactor, Transform interactPoint) {
        if (!interacted) {
            StartCoroutine(UIElements());
            interacted = true;
            this.gameObject.layer = LayerMask.NameToLayer("Default");
        }
    }

    public void ResetLevel() {
        interacted = false;
        this.gameObject.layer = LayerMask.NameToLayer("Interactable");
        questMarker.alpha = 1;
    }

    /// <summary>
    /// Sets the dialogue UI elements active and plays the audio, also calls the StartLevel function once the dialogue is done
    /// </summary>
    IEnumerator UIElements() {
        //Setting dialogue
        dialogueBox.SetActive(true);
        ObjectiveUI.alpha = 0;
        questMarker.alpha = 0;
        textUI.StartLerp(dialogueText, dialogueDuration);
        if (speechAudio != null) {
            speechAudio.Play();
        }
        yield return new WaitForSeconds(dialogueDuration + 2);
        //Deactivating dialogue
        dialogueBox.SetActive(false);
        ObjectiveUI.alpha = 1;
        levelManager.StartLevel();
    }
}
