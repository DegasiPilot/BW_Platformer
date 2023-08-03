using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BonusType { None, Slow, SpeedUp};

public class Bonus : MonoBehaviour
{
    public BonusType bonusType;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<playerCntrl>().PickUpBonus(bonusType);
            Destroy(gameObject);
        }
    }
}

