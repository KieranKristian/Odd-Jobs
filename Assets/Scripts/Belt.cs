using UnityEngine;

public class Belt : MonoBehaviour
{
    //If the player stays on the conveyor belt trigger, it adds a force to the rigidbody
    private void OnTriggerStay(Collider other) {
        if (other.CompareTag("Player")) {
            other.GetComponent<Rigidbody>().AddForce(transform.forward.normalized * 20, ForceMode.Acceleration);
        }
    }
}
