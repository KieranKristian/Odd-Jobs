using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : MonoBehaviour
{
    public Vector3 respawnPos;
    public LogEnd logEnd1;
    public LogEnd logEnd2;
    //[HideInInspector]
    public bool logHeld;

    /// <summary>
    /// Resets the connected anchor of each logEnd
    /// </summary>
    private void Start() {
        logEnd1.logJoint.autoConfigureConnectedAnchor = false;
        logEnd1.logJoint.autoConfigureConnectedAnchor = true;

        logEnd2.logJoint.autoConfigureConnectedAnchor = false;
        logEnd2.logJoint.autoConfigureConnectedAnchor = true;
    }

    /// <summary>
    /// Resets the logs position and resets the log ends as well
    /// </summary>
    public void ResetLog() {
        transform.localPosition = respawnPos;
        transform.eulerAngles = new Vector3(0, -110, 90);
        logEnd1.Reset();
        logEnd2.Reset();
    }

    private void Update() {
        logHeld = logEnd1.pickedUp || logEnd2.pickedUp ? true : false;
    }
}
