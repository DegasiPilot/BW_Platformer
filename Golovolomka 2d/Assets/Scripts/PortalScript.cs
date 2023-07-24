using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalScript : MonoBehaviour
{
    public GameObject SecondPortal;
    public Transform ExitPoint;

    [NonSerialized] public bool IsMoving;

    private PortalScript secondPortalScript;
    private Vector2 currentPos;

    private void Start()
    {
        secondPortalScript = SecondPortal.GetComponent<PortalScript>();
        IsMoving = false;
        currentPos = gameObject.transform.position;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.position = secondPortalScript.ExitPoint.position;
        }
    }

    public IEnumerator MoveToPosition(Vector2 finalPos, float Speed)
    {
        IsMoving = true;
        do
        {
            currentPos = Vector2.MoveTowards(currentPos, finalPos, Speed * Time.deltaTime);
            gameObject.transform.position = currentPos;
            yield return new WaitForEndOfFrame();
        } while (currentPos != finalPos);
        IsMoving = false;
    }
}