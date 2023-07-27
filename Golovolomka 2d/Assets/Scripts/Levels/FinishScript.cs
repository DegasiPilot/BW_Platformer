using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishScript : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Player"))
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().Finish();
    }
}
