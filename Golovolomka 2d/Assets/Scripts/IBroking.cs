using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IBroking : MonoBehaviour
{
    public IEnumerator Broke(float timeToBroke)
    {
        yield return new WaitForSeconds(timeToBroke);
        Destroy(gameObject);
    }
}
