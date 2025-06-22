using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Result : MonoBehaviour
{
    public GameObject[] titles;

    public void Lose()
    {
        if (titles.Length > 0 && titles[0] != null)
            titles[0].SetActive(true);
    }

    public void Win()
    {
        if (titles.Length > 1 && titles[1] != null)
            titles[1].SetActive(true);
    }
}
