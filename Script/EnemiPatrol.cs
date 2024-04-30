using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiPatrol : MonoBehaviour
{
    public float speed;
    public Transform[] wayPoint;

    private Transform target;
    private int destPoint;
    public int damageOnCollision = 10;

    public SpriteRenderer graphics;

    
    void Start()
    {
        target = wayPoint[0];
    }

    void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

      /*  Si l'enemi est quasiment arrivé a sa destination '*/
        if(Vector3.Distance(transform.position, target.position) < 0.3)
        {
            destPoint = (destPoint + 1) % wayPoint.Length;
            target = wayPoint[destPoint];
            graphics.flipX = !graphics.flipX;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.transform.GetComponent<PlayerHealth>();
            playerHealth.TakeDamage(damageOnCollision);

        }
    }
}
