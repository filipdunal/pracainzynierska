using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowDebugging : MonoBehaviour
{
    public GameObject mainCanvas;
    CanvasGroup cg;

    private void Start()
    {
        cg = mainCanvas.GetComponent<CanvasGroup>();
    }

    public void ShowMainUI()
    {
        
        cg.alpha = 1f;
        cg.interactable = true;
        cg.blocksRaycasts = true;
    }

    public void HideMainUI()
    {
        cg.alpha = 0f;
        cg.interactable = false;
        cg.blocksRaycasts = false;
    }


}
