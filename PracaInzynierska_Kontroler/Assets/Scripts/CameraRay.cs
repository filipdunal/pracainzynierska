using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraRay : MonoBehaviour
{
    public Camera myCamera;
    public GameObject quad;
    public Text wynik;

    public Vector2 cords;

    float vertMax;
    float horMax;

    private void Start()
    {
        Renderer rend = quad.GetComponent<Renderer>();
        vertMax = rend.bounds.max.y;
        horMax = rend.bounds.max.x;
    }
    private void Update()
    {
        RaycastHit hit;
        Ray forwardRay = new Ray(myCamera.transform.position, myCamera.transform.up);
        if (Physics.Raycast(forwardRay, out hit, 100f))
        {
            if(hit.collider.name=="Shooting Target Quad (1)")
            {
                //wynik.text = "X: " + (int)hit.point.x + " Z: " + (int)hit.point.z;
                GetCordInPercent(hit.point.x,hit.point.y);
                wynik.text = "X: "+(int)cords.x +" Y: "+(int)cords.y;
            }
            else
            {
                wynik.text = "NO HIT";
            }
        }
    }

    void GetCordInPercent(float x, float y)
    {
        cords.x =100f*x/vertMax;
        cords.y =100f*y/horMax;

        cords.x = (float)System.Math.Round(cords.x, 1);
        cords.y = (float)System.Math.Round(cords.y, 1);
    }
}
