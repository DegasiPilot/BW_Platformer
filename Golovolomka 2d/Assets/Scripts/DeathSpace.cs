using UnityEngine;

public class DeathSpace : MonoBehaviour
{

    public GameObject respawn;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            other.SendMessage("Death");
        }
    }
}
