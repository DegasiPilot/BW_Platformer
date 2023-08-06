using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowInfo : MonoBehaviour
{
    public GameObject info;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            info.SetActive(true);
            GetComponent<Collider2D>().enabled = false;
        }
        
    }
}
