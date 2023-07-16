using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushBtnScript : MonoBehaviour
{
    public List<GameObject> Blocks;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
            DeleteBlocks();
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
