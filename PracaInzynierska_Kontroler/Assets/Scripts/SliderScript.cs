using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderScript : MonoBehaviour
{
    public float delayToReset = 1f;
    Slider sl;
    

    void Start()
    {
        sl = GetComponent<Slider>();
        
    }
    
    public void CountToResetSlider()
    {
        StartCoroutine("ResetSlider");
    }

    IEnumerator ResetSlider()
    {
        yield return new WaitForSeconds(delayToReset);
        sl.value = 0f;
    }

    private void Update()
    {
        //float myHeight = transform.parent.parent.GetComponent<RectTransform>().rect.height;
        //GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, myHeight / 2f);
    }
}
