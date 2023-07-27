using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;
    public float LeftXBorder;
    public float RigthXBorder;
    private SpriteRenderer spriteRenderer;

    private int dir; // -1=left 1=rigth
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        float positionX = gameObject.transform.position.x;
        if (positionX >= RigthXBorder)
        {
            dir = -1;
        }
        else if (positionX <= LeftXBorder)
        {
            dir = 1;
        }
        rb.velocity = new Vector2(dir * speed, rb.velocity.y);
        flip();
    }

    private void flip()
    {
        if (dir > 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (dir < 0)
        {
            spriteRenderer.flipX = false;
        }
    }
}
