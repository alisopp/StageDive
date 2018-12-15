using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action : MonoBehaviour
{
    public int direction;
    public float speed;
    public float perfectDistance;
    public bool perfect = false;
    public bool onFireFinish = false;
    public bool interaction = false;
    public bool destroyMe = false;
    public float destroyTimer;
    public Material[] normalMats;
    public Material[] interactMats;

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
        ChangeRender(0);
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
        if (collision.gameObject.tag.Equals("Trigger"))
        {
            CalcHitDistance(collision.transform.position.x);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Trigger"))
        {
            ChangeRender(0);
            destroyMe = true;
        }
    }

    void DestroyMe()
    {
        //TODO fancy animationes
        Destroy(this.gameObject);
    }

    private void CalcHitDistance(float triggerX)
    {
        if (Mathf.Abs(transform.position.x - triggerX) < perfectDistance)
        {
            perfect = true;
            ChangeRender(2);
        } else
        {
            perfect = false;
            ChangeRender(1);
        }
    }

    private void ChangeRender(int index)
    {
        if (interaction)
        {
            GetComponent<Renderer>().sharedMaterial = interactMats[index];
        } else
        {
            GetComponent<Renderer>().sharedMaterial = normalMats[index];
        }
    }
}
