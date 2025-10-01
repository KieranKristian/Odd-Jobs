using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

public class SplitScreenPlayerManager : MonoBehaviour
{
    [SerializeField]
    public List<PlayerInput> players = new List<PlayerInput>();

    [SerializeField]
    private List<Transform> startingPoints;
    [SerializeField]
    private List<LayerMask> playerLayers;

    public PlayerInputManager playerInputManager;

    [SerializeField]
    List<GameObject> playerModels;

    void Awake()
    {
        playerInputManager = FindObjectOfType<PlayerInputManager>();
    }

    private void OnEnable() {
        playerInputManager.onPlayerJoined += AddPlayer;
    }

    private void OnDisable() {
        playerInputManager.onPlayerJoined -= AddPlayer;
    }

    /// <summary>
    /// Adds a PlayerInput player to a list of players, also sets culling mask for the camera so that the two characters have seperate cameras which render separate UI elements
    /// </summary>
    /// <param name="player"></param>
    public void AddPlayer(PlayerInput player) {
        players.Add(player);
        
        //Sets the player model and spawn point of the player
        Instantiate(playerModels[players.Count - 1], player.transform);
        player.GetComponent<ThirdPersonController>().respawnPoint = startingPoints[players.Count - 1];

        //Convert Layer mask (bit) to an integer
        int layerToAdd = (int)Mathf.Log(playerLayers[players.Count - 1].value, 2);

        //Set the layer
        player.GetComponentInChildren<CinemachineFreeLook>().gameObject.layer = layerToAdd;
        player.GetComponentInChildren<Canvas>().gameObject.layer = layerToAdd;
        //Add the layer
        player.GetComponentInChildren<Camera>().cullingMask |= 1 << layerToAdd;
        //Set the action in the custom Cinemachine Input Handler
        player.GetComponentInChildren<CinemachineInputHandler>().horizontal = player.actions.FindAction("Look");
    }
}
