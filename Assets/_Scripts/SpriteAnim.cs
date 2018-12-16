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
    private Animator animator;
    private bool onFire;


    public void Start()
    {
        onFire = false;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        NextFrame();
    }

    void NextFrame()
    {
        if (ms.spawning || !start)
        {
            //this.GetComponent<SpriteRenderer>().sprite = sprites[actFrame++];
            if (ms.onFire && !onFire)
            {
                onFire = true;
                Debug.Log(" I am on Fire");
                animator.SetBool("Hype", true);
            }else if(!ms.onFire && onFire)
            {
                onFire = false;
                Debug.Log("Not anymore on Fire");
                animator.SetBool("Hype", false);
            }
            //else if (actFrame > idle) actFrame = actFrame % sprites.Length;
            //else actFrame = actFrame % idle;
            if (ms.spawning) start = true;
        }
        else
        {
            animator.SetTrigger("Jump");
            /*if (actFrame != 0 && !end)
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
            }*/
        }
    }
}
