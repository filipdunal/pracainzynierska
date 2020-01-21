using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceScript : MonoBehaviour
{
    public Material idleMaterial;
    public Material blinkMaterial;
    public float timeAmountOfBlink;
    public float speedOfShowingBlink;

    Material currentMaterial;
    Material desiredMaterial;
    private void Start()
    {
        currentMaterial = transform.GetChild(0).gameObject.GetComponent<Renderer>().material;

        desiredMaterial = currentMaterial;
    }
    private void Update()
    {
        currentMaterial.Lerp(currentMaterial, desiredMaterial, speedOfShowingBlink * Time.deltaTime);
    }

    public void Blink()
    {
        desiredMaterial = blinkMaterial;
        StopAllCoroutines();
        StartCoroutine(SwitchToDefaultMaterial());
    }

    IEnumerator SwitchToDefaultMaterial()
    {
        Debug.Log("Coroutine started");
        yield return new WaitForSeconds(timeAmountOfBlink);
        desiredMaterial = idleMaterial;
        Debug.Log("Coroutine finished");
    }

}
