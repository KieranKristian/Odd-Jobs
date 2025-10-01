using UnityEngine;

public class ObjectiveBox : MonoBehaviour
{
    Collider coll;
    [SerializeField] string objectTag;
    [SerializeField] LevelManager levelManager;

    private void Start() {
        coll = GetComponent<Collider>();
    }

    /// <summary>
    /// Checks whether the objective object is fully contained in the box, if so, calls LevelWon on the level manager
    /// </summary>
    private void OnTriggerStay(Collider other) {
        if (other.tag == objectTag) {
            if(coll.bounds.Contains(other.bounds.max) && coll.bounds.Contains(other.bounds.min)) {
                if (!other.transform.parent.GetComponent<Log>().logHeld) {
                    levelManager.LevelWon();
                    other.transform.parent.GetComponent<Log>().ResetLog();
                    other.gameObject.SetActive(false);
                }
            } 
        }
    }
}
