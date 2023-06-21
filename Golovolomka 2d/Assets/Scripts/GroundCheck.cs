using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag != "Player")
            gameObject.GetComponentInParent<playerCntrl>().IsGrounded = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag != "Player")
            gameObject.GetComponentInParent<playerCntrl>().IsGrounded = false;
    }
}
