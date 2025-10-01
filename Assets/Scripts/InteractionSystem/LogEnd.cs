using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LogEnd : MonoBehaviour , IInteractable {
    [HideInInspector]
    public bool pickedUp;

    public Joint logJoint;
    public GameObject barrier;

    public Transform interactParent;

    public void Interact(Interactor interactor, Transform interactPoint) {
        pickedUp = !pickedUp; //Switches bool from its current state

        if (pickedUp) {
            PickUpLog(interactPoint);
        } else {
            DropLog();
        }
    }

    /// <summary>
    /// Attatches the logJoint to the players rigid body
    /// </summary>
    void PickUpLog(Transform interactPoint) {
        transform.tag = "HeldLog";
        //Attatching logJoint
        Rigidbody rb = interactPoint.GetComponent<Rigidbody>();
        logJoint.connectedBody = rb;
        logJoint.autoConfigureConnectedAnchor = false;

        interactParent = interactPoint.parent;

        //Locking the players rotation to look at the log
        interactParent.GetComponent<ThirdPersonController>().lockedDirection = transform.position;
        interactParent.GetComponent<ThirdPersonController>().logPos = transform;

        barrier.SetActive(true);
    }

    /// <summary>
    /// Detatching the logJoint
    /// </summary>
    void DropLog() {
        Reset();
    }

    /// <summary>
    /// Resetting the
    /// </summary>
    public void Reset() {
        transform.tag = "LogEnd";
        //Reset the log end
        logJoint.connectedBody = null;
        logJoint.autoConfigureConnectedAnchor = true;

        barrier.SetActive(false);
        if(interactParent != null) {
            interactParent.GetComponent<Interactor>().state = Interactor.interactionState.idle;
        }
        interactParent = null;
        
        pickedUp = false;
    }
}
