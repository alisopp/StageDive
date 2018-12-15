using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Inputs
    public KeyCode UP;
    public KeyCode LEFT;
    public KeyCode RIGHT;
    public KeyCode DOWN;
    public KeyCode COMBO;

    //Variables
    public MasterSpawner ms;
    public Trigger topTrigger;
    public Trigger bottomTrigger;
    public float comboLvL = 0;
    public float crowdLvL = 0;
    public int speed = 5;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject top;
        if (topTrigger.triggered == null)
        {
            top = this.gameObject;
        }
        else
        {
            top = topTrigger.triggered;
        }

        GameObject bottom;
        if (bottomTrigger.triggered == null)
        {
            bottom = this.gameObject;
        }
        else
        {
            bottom = bottomTrigger.triggered;
        }

        if (Input.GetKeyDown(UP))
        {
            if (top.tag.Equals("UP") || bottom.tag.Equals("UP"))
            {
                UpdateCombo(true);
                if (top.tag.Equals("UP"))
                {
                    UpdateScore(top);
                }
                if (bottom.tag.Equals("UP"))
                {
                    UpdateScore(bottom);
                }
            }
            else
            {
                UpdateCombo(false);
            }
        }

        if (Input.GetKeyDown(LEFT))
        {
            if (top.tag.Equals("LEFT") || bottom.tag.Equals("LEFT"))
            {
                UpdateCombo(true);
                if (top.tag.Equals("LEFT"))
                {
                    UpdateScore(top);
                }
                if (bottom.tag.Equals("LEFT"))
                {
                    UpdateScore(bottom);
                }
            }
            else
            {
                UpdateCombo(false);
            }
        }

        if (Input.GetKeyDown(RIGHT))
        {
            if (top.tag.Equals("RIGHT") || bottom.tag.Equals("RIGHT"))
            {
                UpdateCombo(true);
                if (top.tag.Equals("RIGHT"))
                {
                    UpdateScore(top);
                }
                if (bottom.tag.Equals("RIGHT"))
                {
                    UpdateScore(bottom);
                }
            }
            else
            {
                UpdateCombo(false);
            }
        }

        if (Input.GetKeyDown(DOWN))
        {
            if (top.tag.Equals("DOWN") || bottom.tag.Equals("DOWN"))
            {
                UpdateCombo(true);
                if (top.tag.Equals("DOWN"))
                {
                    UpdateScore(top);
                }
                if (bottom.tag.Equals("DOWN"))
                {
                    UpdateScore(bottom);
                }
            }
            else
            {
                UpdateCombo(false);
            }
        }
    }

    void UpdateCombo(bool hit)
    {
        if (hit)
        {
            if (comboLvL < 0)
            {
                comboLvL = 0;
            }
            comboLvL += 0.1f;
        } else
        {
            if (comboLvL > 0)
            {
                comboLvL = 0;
            }
            comboLvL -= 0.1f;
        }
    }

    void UpdateScore(GameObject action)
    {
        action.GetComponent<Action>().HitAction();
    }
}
