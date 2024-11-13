using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class timeup : MonoBehaviour
{

    public float currentTime;
    public float startingTime = 5f;
    void Start()
    {
        currentTime = startingTime;
    }
    void Update()
    {
        currentTime -= Time.deltaTime;

        if (currentTime <= 0)
        {
            SceneManager.LoadScene("Menu");
        }
    }
}