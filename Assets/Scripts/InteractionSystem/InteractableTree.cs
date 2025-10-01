using UnityEngine;

public class InteractableTree : MonoBehaviour, IInteractable
{
    int health = 2;
    [SerializeField] GameObject log;
    [SerializeField] GameObject treeTrunk;
    /// <summary>
    /// Takes health from the tree, if the tree is hit once, make it lean, if twice, make it fall down
    /// </summary>
    public void Interact(Interactor interactor, Transform interactPoint) {
        health--;
        if (health == 1) {
            treeTrunk.transform.localEulerAngles -= new Vector3(10, 0, 0);
        }
        if (health <= 0) {
            ChopDown();
        }
    }

    /// <summary>
    /// Makes the tree "fall down" and sets the interactable log active
    /// </summary>
    void ChopDown() {
        this.gameObject.SetActive(false);
        log.SetActive(true);
    }

    /// <summary>
    /// Resets the tree and the log
    /// </summary>
    public void ResetTree() {
        treeTrunk.transform.localEulerAngles = Vector3.zero;
        this.gameObject.SetActive(true);
        log.GetComponent<Log>().ResetLog();
        health = 2;
    }
}