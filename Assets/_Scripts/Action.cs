using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action : MonoBehaviour
{
    public int direction;
    public float speed;
    public bool onFireFinish = false;
    public bool interaction = false;

    public void Spawn(int direction)
    {
        this.direction = direction;
    }

    public void SetSpeed(int speed)
    {
        this.speed = speed;
        this.GetComponent<Rigidbody2D>().velocity = new Vector2(this.speed*direction,0);
    }

    //TODO make action change colore

}
