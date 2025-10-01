using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuNavigation : MonoBehaviour
{
    [SerializeField] GameObject cutscenes;
    public float duration;
    bool playing;
    /// <summary>
    /// Loads the main game scene
    /// </summary>
    void OnJump() {
        if (!playing) {
            playing = true;
            cutscenes.SetActive(true);
            StartCoroutine(LoadGame());
        }
    }

    IEnumerator LoadGame() {
        yield return new WaitForSeconds(duration);
        SceneManager.LoadScene("MainGame");
    }
}
