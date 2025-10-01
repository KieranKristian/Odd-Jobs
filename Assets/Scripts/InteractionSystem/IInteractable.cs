using UnityEngine;

/// <summary>
/// Interface which contains an interact function
/// This interface is inherited from which gives objects in the scene the interact function
/// </summary>
public interface IInteractable {
    public void Interact(Interactor interactor, Transform interactPoint);
}
