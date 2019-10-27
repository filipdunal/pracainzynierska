using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISteeringScript : MonoBehaviour
{
    CanvasGroup cg;
    private void Start()
    {
        cg = GetComponent<CanvasGroup>();
    }
    private void Update()
    {
        if(Input.GetButtonDown("Pause"))
        {
            if(cg.interactable)
            {
                cg.interactable = false;
                cg.alpha = 0;
                cg.blocksRaycasts = false;
            }
            else
            {
                cg.interactable = true;
                cg.alpha = 1;
                cg.blocksRaycasts = true;

            }
        }
    }
}
