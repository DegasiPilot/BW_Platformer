using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public int UnicueIndex;

    private void Start()
    {
        if (GameData.CollectedCoins[UnicueIndex])
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameData.CollectedCoins[UnicueIndex] = true;
            Destroy(gameObject);
        }
    }
}
