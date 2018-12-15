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
        if (Input.GetKeyDown(UP))
        {
            if (topTrigger.tag.Equals("UP") || bottomTrigger.tag.Equals("UP"))
            {
                UpdateCombo(true);
            } else
            {
                UpdateCombo(false);
            }
        }
        if (Input.GetKeyDown(LEFT))
        {
            if (topTrigger.tag.Equals("LEFT") || bottomTrigger.tag.Equals("LEFT"))
            {
                UpdateCombo(true);
            }
            else
            {
                UpdateCombo(false);
            }
        }
        if (Input.GetKeyDown(RIGHT))
        {
            if (topTrigger.tag.Equals("RIGHT") || bottomTrigger.tag.Equals("RIGHT"))
            {
                UpdateCombo(true);
            }
            else
            {
                UpdateCombo(false);
            }
        }
        if (Input.GetKeyDown(DOWN))
        {
            if (topTrigger.tag.Equals("DOWN") || bottomTrigger.tag.Equals("DOWN"))
            {
                UpdateCombo(true);
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
}
