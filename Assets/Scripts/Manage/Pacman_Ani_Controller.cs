using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pacman_Ani_Controller : MonoBehaviour
{
    // Start is called before the first frame update
    Animator animator;
    public bool played_once;

    void Start()
    {
        animator = GetComponent<Animator>();
        played_once = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Models.Pacman.eaten && !played_once)
        {
            played_once = true;
            animator.SetBool("isDeath", true);
            animator.SetBool("PlayOnce", true);
            played_once = true;
        }
        else if(!Models.Pacman.eaten)
        {
            animator.SetBool("PlayOnce", false);
            played_once= false;
            animator.SetBool("isDeath", false);
        }
    }
}
