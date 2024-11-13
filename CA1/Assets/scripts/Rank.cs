using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Rank : MonoBehaviour
{
    Animator animator;
    public PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
 
    } 
    // Update is called once per frame
    void Update()
    {
        int value = playerController.score;
        animator.SetInteger("Score", value);
    }
}
