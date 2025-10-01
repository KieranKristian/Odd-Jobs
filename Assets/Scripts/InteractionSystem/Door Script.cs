using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject door;
    bool open;

    /// <summary>
    /// Opens a door when the player interacts
    /// </summary>
    public void Interact(Interactor interactor, Transform interactPoint) {
        if (!open) {
            door.transform.localEulerAngles = new Vector3(0, 100, 0);
            door.layer = 0; //Stops the interaction prompt from appearing on the open door
        }
    }
}
