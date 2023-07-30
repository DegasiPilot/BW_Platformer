using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushBtnScript : MonoBehaviour
{
    public List<GameObject> Blocks;

    private Animator animator;
    private Collider2D myCollider;
    private bool isPressed;

    private void Start()
    {
        animator = GetComponent<Animator>();
        myCollider = GetComponent<Collider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            StopAllCoroutines();
            isPressed = true;
            animator.SetBool("IsPressed", isPressed);
            DeleteBlocks();
            StartCoroutine(CheckButtonState(collision.collider));
        }
    }

    IEnumerator CheckButtonState(Collider2D other)
    {
        yield return new WaitForFixedUpdate();
        while (isPressed)
        {
            if (myCollider.IsTouching(other))
            {
                yield return new WaitForFixedUpdate();
            }
            else
            {
                isPressed = false;
                animator.SetBool("IsPressed", isPressed);
            }
        }
    }

    private void DeleteBlocks()
    {
        if (Blocks.Count != 0)
        {
            foreach (GameObject block in Blocks)
                Destroy(block);
            Blocks.Clear();
        }
    }
}
