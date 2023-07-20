using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalScript : MonoBehaviour
{
    public GameObject SecondPortal;
    public Transform ExitPoint;

    private PortalScript secondPortalScript;

    private void Start()
    {
        secondPortalScript = SecondPortal.GetComponent<PortalScript>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.position = secondPortalScript.ExitPoint.position;
        }
    }
}
