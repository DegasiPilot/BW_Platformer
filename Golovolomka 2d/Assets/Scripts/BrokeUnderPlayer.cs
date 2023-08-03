using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokeUnderPlayer : MonoBehaviour
{
    public float timeToBroke = 1f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(BrokePlatform());
        }
    }

    private IEnumerator BrokePlatform()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            StartCoroutine(transform.GetChild(i).GetComponent<IBroking>().Broke(timeToBroke));
            yield return new WaitForSeconds(0.15f);
        }
        while(transform.childCount > 0)
        {
            yield return new WaitForFixedUpdate();
        }
        Destroy(gameObject);
    }
}
