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
    public GameController gc;
    public MasterSpawner ms;
    public Trigger topTrigger;
    public Trigger bottomTrigger;
    public int score = 0;
    public float comboLvL = 0;
    public int crowdLvL = 0;
    public float speed = 5;
    public int timeMod = 1;
    int missed = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(COMBO))
        {
            if (!ms.onFire) gc.OnFire(this);
        }

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
            else
            {
                comboLvL += 0.1f;
                comboLvL = Mathf.Min(comboLvL, 5);
            }
            missed = 0;
        }
        else
        {
            if (comboLvL > 0)
            {
                if (missed >= 2)
                {
                    comboLvL = 0;
                    missed = 0;
                }
                else
                {
                    comboLvL = comboLvL * 0.75f;
                    missed++;
                }
            }
            else
            {
                comboLvL -= 0.1f;
                comboLvL = Mathf.Max(comboLvL, -5);
            }
            MissHit(2);
        }
        if (ms.comboLvL != (int)comboLvL)
        {
            UpdateLvLs(crowdLvL);
        }
    }

    void UpdateScore(GameObject action)
    {
        Action a = action.GetComponent<Action>();
        int points = 1;
        if (a.perfect) points += points;
        points *= (1+(int)comboLvL);

        points *= timeMod;

        score += points;

        if (a.onFireFinish)
        {
            if (comboLvL > 2 && crowdLvL > 0)
            {
                gc.EndGame();
            } else if (comboLvL > 2)
            {
                score += 200;
            }
            gc.CoolDown(this);
        }

        a.HitAction();
    }

    public void MissHit(int points)
    {
        points = points * (1+(int)comboLvL);

        points *= timeMod;

        score -= points;

        if (ms.onFire)
        {
            gc.CoolDown(this);
            if (crowdLvL > 0) score -= 100;
        }
    }

    public void InteractionTime()
    {
        ms.InteractionWhenReady();
    }

    public void UpdateLvLs(int crowdLvl)
    {
        crowdLvL = crowdLvl;
        speed = 5 + ((crowdLvL + comboLvL)/2);

        ms.speed = speed;
        ms.comboLvL = (int)comboLvL;
        ms.crowdLvL = crowdLvL;
        SetSpeeds();
    }

    void SetSpeeds()
    {
        foreach(Action a in ms.topSpawner.GetComponentsInChildren<Action>())
        {
            a.SetSpeed(speed);
        }
        foreach (Action a in ms.bottomSpawner.GetComponentsInChildren<Action>())
        {
            a.SetSpeed(speed);
        }
    }

    public void OnFire(bool fire)
    {
        ms.OnFire(fire);
    }

    public void CoolDown()
    {
        ms.EndOnFire();
    }
}
