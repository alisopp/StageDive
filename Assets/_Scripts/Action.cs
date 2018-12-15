using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action : MonoBehaviour
{
    public int direction;
    public float speed;
    public bool onFireFinish = false;
    public bool interaction = false;
    public bool destroyMe = false;
    public float destroyTimer;

    void Update()
    {
        if (destroyMe)
        {
            if (destroyTimer <= 0)
            {
                DestroyMe();
            }
            destroyTimer -= Time.deltaTime;
        }
    }

        public void Spawn(int direction, float speed)
    {
        this.direction = direction;
        this.speed = speed;
        this.GetComponent<Rigidbody2D>().velocity = new Vector2(this.speed * this.direction, 0);
    }

    public void SetSpeed(int speed)
    {
        this.speed = speed;
        this.GetComponent<Rigidbody2D>().velocity = new Vector2(this.speed*direction,0);
    }

    //TODO make action change colore
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Trigger"))
        {
            destroyMe = true;
        }
    }

    void DestroyMe()
    {
        //TODO fancy animationes
        Destroy(this.gameObject);
    }
}
