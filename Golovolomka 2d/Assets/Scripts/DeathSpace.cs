using UnityEngine;

public class DeathSpace : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.SendMessage("Death");
        }
        else
        {
            Destroy(other);
        }
    }
}
