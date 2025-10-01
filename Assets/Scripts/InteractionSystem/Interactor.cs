using System.Collections;
using UnityEngine;

public class Interactor : MonoBehaviour {
    [Header("Interaction")]
    [SerializeField] Transform interactionPoint;
    [SerializeField] float interactionRadius;
    [SerializeField] LayerMask interactionLayerMask;

    private readonly Collider[] colliders = new Collider[3];
    private int numFound;

    IInteractable interactable; //Reference for the Interactable script on the object

    [SerializeField] CanvasGroup interactPromptUI;
    [SerializeField] ThirdPersonController thirdPersonController;
    [SerializeField] AudioSource axeChopNoise;

    bool interacting;
    public bool hasAxe;
    public GameObject axe;

    public enum interactionState {
        idle, holding, talking, chopping, door
    }
    public interactionState state;

    private void Update() {
        //Looks for objects with the interactable layer mask
        if (!interacting) {
            numFound = Physics.OverlapSphereNonAlloc(interactionPoint.position, interactionRadius, colliders, (int)interactionLayerMask);
            if (numFound > 0) {
                interactable = colliders[0].GetComponent<IInteractable>();

                //Switches the interaction state based on the tag of the object, this is used by the Interaction UI prompt
                switch (colliders[0].tag) {
                    case "LogEnd":
                        state = interactionState.idle;
                        break;
                    case "HeldLog":
                        state = interactionState.holding;
                        break;
                    case "NPC":
                        state = interactionState.talking;
                        break;
                    case "Tree":
                        if (!hasAxe) {
                            state = interactionState.idle;
                            return;
                        } else {
                            state = interactionState.chopping;
                            break;
                        }
                    case "Door":
                        state = interactionState.door;
                        break;
                }

                interactPromptUI.alpha = 1;
            } else {
                interactable = null;
                interactPromptUI.alpha = 0;
            }
        }
    }

    /// <summary>
    /// Calls the interact function on the current interactable object that the player is looking at
    /// </summary>
    void OnInteract() {
        if (interactable != null) {
            if (!interacting) {
                //If the object is a tree it calls SwingAxe instead
                if (colliders[0].tag == "Tree") {
                    if (hasAxe) {
                        StartCoroutine(SwingAxe());
                    }
                    return;
                }
                interactable.Interact(this, interactionPoint);
                interactable = null;
            }
        }
    }

    /// <summary>
    /// Sets the axe object in the players hand to true
    /// </summary>
    public void GiveAxe() {
        hasAxe = true;
        transform.GetComponentInChildren<AxeLocator>().Axe.SetActive(true);
    }

    /// <summary>
    /// Plays the axe swing animation and sets a delay on being able to interact again
    /// </summary>
    IEnumerator SwingAxe() {
        //Playing animation
        thirdPersonController.animator.SetTrigger("SwingAxe");
        interacting = true;
        yield return new WaitForSeconds(0.75f);
        //Performing interaction
        axeChopNoise.Play();
        interactable.Interact(this, interactionPoint);
        yield return new WaitForSeconds(0.75f);
        interacting = false;
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(interactionPoint.position, interactionRadius);
    }
}
