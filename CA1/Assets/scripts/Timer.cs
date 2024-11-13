using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Timer : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI countdownText;

    public float currentTime;
    public float startingTime = 120f;
    public PlayerController playerController;
    void Start()
    {
        currentTime = startingTime;
    }
    void Update()
    {

        currentTime -= Time.deltaTime;
        countdownText.text = Mathf.Ceil(currentTime).ToString();

        if (currentTime <= 0)
        {
            currentTime = 0;
            SceneManager.LoadScene("TIMESUP");
            Destroy(GameObject.Find("idle_00"));
        }
    }
}