using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sheild : MonoBehaviour
{
    // Start is called before the first frame update
    public bool is_sh;
    Animator animator;

    void Start()
    {
        is_sh = false;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!is_sh && Models.Pacman.Shield > 0)
        {
            is_sh=true;
        }else if(Models.Pacman.Shield <= 0)
        {
            is_sh=false;
        }

        if (is_sh)
        {
            animator.SetBool("is_shield", true);
        }
        else
        {
            animator.SetBool("is_shield", false);
        }

        if (Models.Pacman.shield_destroy)
        {
            animator.SetBool("destroy", true);
        }
        else
        {
            animator.SetBool("destroy", false);
        }
    }
}
