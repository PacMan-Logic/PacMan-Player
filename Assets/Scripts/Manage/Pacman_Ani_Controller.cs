using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pacman_Ani_Controller : MonoBehaviour
{
    // Start is called before the first frame update
    Animator animator;
    public bool played_once;
    public RuntimeAnimatorController new_controller_0;
    public RuntimeAnimatorController new_controller_1;
    private int idx;

    void Start()
    {
        animator = GetComponent<Animator>();
        played_once = false;
        idx = 0;
        animator.runtimeAnimatorController = new_controller_0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (idx == 0)
            {
                animator.runtimeAnimatorController = new_controller_1;
                idx = 1;
            }
            else if (idx == 1)
            {
                animator.runtimeAnimatorController = new_controller_0;
                idx = 0;
            }
        }
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
