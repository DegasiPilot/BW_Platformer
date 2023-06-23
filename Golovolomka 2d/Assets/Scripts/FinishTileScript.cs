using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishTileScript : MonoBehaviour
{
    public GameObject finishPanel;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            finishPanel.SetActive(true);
            collision.collider.gameObject.GetComponent<playerCntrl>().Finish();
        }
    }

}
