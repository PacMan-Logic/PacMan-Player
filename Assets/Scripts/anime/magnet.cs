using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class magnet : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Models.Pacman.Magnet > 0)
        {
            animator.SetBool("magnet", true);
        }
        else
        {
            animator.SetBool("magnet", false);
        }
    }
}
