using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnim : MonoBehaviour
{
    public Sprite[] sprites;
    public Sprite[] spriteJump;
    int actFrame = 0;
    public int idle;
    float time;
    public float speed;
    public MasterSpawner ms;
    bool end = false;
    bool start = false;

    // Update is called once per frame
    void Update()
    {
        if (!end)
        {
            if (time <= 0)
            {
                NextFrame();
                time = speed;
            }
            else
            {
                time -= Time.deltaTime;
            }
        }
    }

    void NextFrame()
    {
        if (ms.spawning || !start)
        {
            this.GetComponent<SpriteRenderer>().sprite = sprites[actFrame++];
            if (ms.onFire) actFrame = actFrame % sprites.Length;
            else if (actFrame > idle) actFrame = actFrame % sprites.Length;
            else actFrame = actFrame % idle;
            if (ms.spawning) start = true;
        }
        else
        {
            if (actFrame != 0 && !end)
            {
                this.GetComponent<SpriteRenderer>().sprite = sprites[actFrame++];
                if (actFrame > idle) actFrame = actFrame % sprites.Length;
                else actFrame = actFrame % idle;
            }
            else
            {
                end = true;
                this.GetComponent<SpriteRenderer>().sprite = spriteJump[actFrame++];
                actFrame = actFrame % spriteJump.Length;
            }
        }
    }
}
