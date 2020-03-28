using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoSystem : MonoBehaviour
{
    public float speedOfFading=10f;
    bool turnedOn;
    CanvasGroup canvasGroup;
    Text text;

    private void Start()
    {
        text = GetComponent<Text>();
        canvasGroup = GetComponent<CanvasGroup>();
    }
    public void ShowMessage(string message, float time, Color color)
    {
        turnedOn = true;
        text.text = message;
        text.color = color;

        StopCoroutine(HideMessage(time));
        StartCoroutine(HideMessage(time));
    }

    IEnumerator HideMessage(float time)
    {
        yield return new WaitForSeconds(time);
        turnedOn = false;
    }

    private void Update()
    {
        canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, turnedOn ? 1.0f : 0.0f,Time.deltaTime*speedOfFading);
    }
}
