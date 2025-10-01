using UnityEngine;
using UnityEngine.UI;

public class InteractionPromptUI : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] RawImage interactPrompt;
    [SerializeField] Texture2D[] promptTextures;
    [SerializeField] Interactor player;

    //Makes the interaction prompt face the players camera
    private void LateUpdate() {
        var rotation = mainCamera.transform.rotation;

        transform.LookAt(transform.position + rotation * Vector3.forward, rotation * Vector3.up);

        //Switches the prompt based on the players state
        interactPrompt.texture = promptTextures[(int)player.state];
    }
}
