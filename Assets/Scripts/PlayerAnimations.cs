using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    // Start is called before the first frame update
    Animator animator;
    public bool isRunning;
    void Start()
    {
        animator = GetComponent<Animator>();
        //isRunning = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            isRunning = true;
            animator.SetBool("isRunning", true);
        }

        if (Input.GetKey(KeyCode.A))
        {
            isRunning = true;
            animator.SetBool("isRunning", true);
        }

        if (Input.GetKey(KeyCode.S))
        {
            isRunning = true;
            animator.SetBool("isRunning", true);
        }

        if (Input.GetKey(KeyCode.D))
        {
            isRunning = true;
            animator.SetBool("isRunning", true);
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            isRunning = false;
            animator.SetBool("isRunning", false);
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            isRunning = false;
            animator.SetBool("isRunning", false);
        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            isRunning = false;
            animator.SetBool("isRunning", false);
        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            isRunning = false;
            animator.SetBool("isRunning", false);
        }

    }
}
