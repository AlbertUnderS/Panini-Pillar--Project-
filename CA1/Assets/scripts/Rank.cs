using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rank : MonoBehaviour
{
    Animator animator;
    public PlayerController playerController;

    int value;

    // Start is called before the first frame update
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        value = playerController.score;
        Debug.Log(value);
        animator.SetInteger("Score", value);
    }
}
