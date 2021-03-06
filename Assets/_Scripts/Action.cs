﻿using System.Collections;
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
    public Sprite[] normalSprite;
    public Sprite[] interactSprite;

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

    public void SetSpeed(float speed)
    {
        if (!destroyMe)
        {
            this.speed = speed;
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(this.speed * direction, 0);
        }
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
        if (!destroyMe)
        {
            if (collision.gameObject.tag.Equals("Trigger"))
            {
                ChangeRender(5);
                destroyMe = true;
                GetComponent<Rigidbody2D>().gravityScale = Random.Range(1.5f, 3);
                transform.parent.transform.parent.transform.parent.GetComponent<Player>().MissHit(1);
            }
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
            GetComponent<SpriteRenderer>().sprite = interactSprite[index];
        } else
        {
            GetComponent<SpriteRenderer>().sprite = normalSprite[index];
        }
    }

    public void HitAction()
    {
        if (perfect) ChangeRender(4);
        else ChangeRender(3);
        destroyMe = true;
        Destroy(GetComponent<Collider2D>());
        GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
        GetComponent<Rigidbody2D>().gravityScale = Random.Range(-2.5f, -2.25f);
    }
}
