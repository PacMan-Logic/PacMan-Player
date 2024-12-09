using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pacman_Ani_Controller : MonoBehaviour
{
    // Start is called before the first frame update
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Models.Pacman.eaten)
        {
            animator.SetBool("isDeath", true);
        }
        else
        {
            animator.SetBool("isDeath", false);
        }
    }
}
