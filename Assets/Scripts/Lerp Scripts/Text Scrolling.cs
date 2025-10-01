using System.Collections;
using UnityEngine;
using TMPro;

public class TextScrolling : MonoBehaviour
{
    public string desiredText;
    float lerpNum;

    public float duration;

    KMaths.Floats floats;

    public TMP_Text tmpText;

    //Sets the tmpText to a substring from the desiredText and the lerpNumber so it 'scrolls' through the  text
    private void Update() {
        if (tmpText != null) {
            tmpText.text = desiredText.Substring(0, (int)lerpNum);
        }
    }

    /// <summary>
    /// Starts the lerp with a text that is passed into it
    /// </summary>
    public void StartLerp(string text, float lerpDuration) {
        duration = lerpDuration;
        desiredText = text;
        floats.endValue = desiredText.Length;

        StartCoroutine(Lerps());
    }

    /// <summary>
    /// Performs the lerp based on the start and end values
    /// </summary>
    /// <returns></returns>
    IEnumerator Lerps() {
        float time = 0;
        float t;
        while (time < duration) {
            t = Easings.QuadInOut(time, duration);

            lerpNum = KMaths.Lerp(floats.startValue, floats.endValue, t);

            time += Time.deltaTime;
            yield return null;
        }
        lerpNum = floats.endValue;
    }
}
