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
    float fireTimer = 0;
    public bool end = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (end) return;

        if (Input.GetKeyDown(COMBO))
        {
            if (!ms.onFire && !ms.fireIsOn && fireTimer <= 0) gc.OnFire(this);
            fireTimer = ms.fireDelay;
        }

        if (!ms.onFire && fireTimer > 0)
        {
            fireTimer -= Time.deltaTime;
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
                    comboLvL = comboLvL * 0.5f;
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
        points *= (1 + (int)comboLvL);

        points *= timeMod;

        score += points;

        if (a.onFireFinish)
        {
            if (comboLvL > 2 && crowdLvL > 2)
            {
                gc.EndGame();
            }
            else if (comboLvL > 2)
            {
                score += 200;
            }
            gc.CoolDown();
        }

        if (a.interaction)
        {
            bool top = a.transform.parent.tag.Equals("Top");
            if (top == gc.topGood)
            {
                score += 50;
            }
            else
            {
                score -= 50;
            }
        }

        a.HitAction();
    }

    public void MissHit(int points)
    {
        points = points * (1 + Mathf.Abs((int)comboLvL));

        points *= timeMod;

        score -= points;

        if (ms.onFire)
        {
            gc.CoolDown();
            if (crowdLvL > 0) score -= 300;
        }

        if (score < 0) score = 0;
    }

    public void InteractionTime()
    {
        ms.InteractionWhenReady();
    }

    public void UpdateLvLs(int crowdLvl)
    {
        crowdLvL = crowdLvl;
        speed = 5 + ((crowdLvL + comboLvL) / 2);

        ms.SetLevel(crowdLvL, (int)comboLvL, speed);
        SetSpeeds();
    }

    void SetSpeeds()
    {
        foreach (Action a in ms.topSpawner.transform.GetComponentsInChildren<Action>())
        {
            a.SetSpeed(speed);
        }
        foreach (Action a in ms.bottomSpawner.transform.GetComponentsInChildren<Action>())
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