using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridOfChapters : MonoBehaviour
{
    public List<GameObject> levels;
    private void Start()
    {
        ShowChapters(); 
    }

    void ShowChapters()
    {
        GameObject newObj;
        foreach (GameObject lvl in levels)
        {
            newObj = (GameObject)Instantiate(lvl, transform);
        }
    }
    


}

