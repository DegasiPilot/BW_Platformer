using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MixingPortals : MonoBehaviour
{
    public GameObject[] Portals;
    public float MixTime;
    public float Speed;
    public float LeftX;
    public float RightX;
    public float UpY;
    public float DownY;

    private List<Vector2> StartPositions;
    private List<PortalScript> PortalsScripts;
    private GameManager gameManager;

    private void Start()
    {
        StartPositions = new List<Vector2>();
        PortalsScripts = new List<PortalScript>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        for(int i = 0; i < Portals.Length; i++)
        {
            StartPositions.Add((Vector2)Portals[i].transform.position);
            PortalsScripts.Add(Portals[i].GetComponent<PortalScript>());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(StartMix());
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }

    public IEnumerator StartMix()
    {
        gameManager.DestroyTip(0);
        for (int i = 0; i < Portals.Length; i++)
        {
            StartCoroutine(MovingPortal(PortalsScripts[i]));
        }
        yield return new WaitForSeconds(MixTime);
        StopAllCoroutines();
        for (int i = 0; i < Portals.Length; i++)
        {
            PortalsScripts[i].StopAllCoroutines();
            int posIndex = Random.Range(0, StartPositions.Count);
            Vector2 randomPos = StartPositions[posIndex];
            StartPositions.RemoveAt(posIndex);
            StartCoroutine(PortalsScripts[i].MoveToPosition(randomPos, Speed));
        }
    }

    public IEnumerator MovingPortal(PortalScript portal)
    {
        while (true)
        {
            StartCoroutine(portal.MoveToPosition(GetRandomPos(), Speed));
            yield return new WaitWhile(() => portal.IsMoving);
        }
    }

    public Vector2 GetRandomPos()
    {
        return new Vector2(
            Random.Range(LeftX, RightX),
            Random.Range(DownY, UpY));
    }
}
