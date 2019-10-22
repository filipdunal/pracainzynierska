using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelMainOpener : MonoBehaviour
{
    Animator animator;
    bool isOpen;
    public Button OpenPanelButton;
    public void OpenPanel()
    {
        if(animator!=null)
        {
            isOpen= animator.GetBool("open");
            animator.SetBool("open", !isOpen);
        }
    }

    private void OnGUI()
    {
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

}
