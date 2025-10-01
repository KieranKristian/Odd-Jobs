using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DisableOnJoin : MonoBehaviour
{
    public bool enable;
    public int players;
    private int playerCount;

    PlayerInputManager playerInputManager;

    void Awake() {
        //Setting playerInputManager
        playerInputManager = FindObjectOfType<PlayerInputManager>();
        playerInputManager.onPlayerJoined += Disable;
        this.gameObject.SetActive(!enable);
    }

    void OnDestroy() {
        playerInputManager.onPlayerJoined -= Disable;
    }

    /// <summary>
    /// Sets the object active/unactive depending on the boolean enable
    /// </summary>
    void Disable(PlayerInput player) {
        if (playerCount == players - 1) {
            this.gameObject.SetActive(enable);
        } else {
            playerCount++;
        }
    }
}
