using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterSpawner : MonoBehaviour
{
    //Controll
    public bool spawning; //if game is on
    float actionTimer;
    bool interactionReady = false;
    float fireTimer;
    public float spawnLimiter;
    public float speed;

    public Spawner topSpawner;
    public Spawner bottomSpawner;

    //Spawn Actions: Variables and Modifiers
    float spawnInterval;
    public float spawnBaseInterval;

    public int comboLvL;
    public float comboMod;
    public int crowdLvL;
    public float crowdMod;

    //Spawn Interactions
    float interactionTimer;
    public float interactionInterval;

    //Spawn ONFIRE
    public bool onFire; //if you are on fire
    public bool fireIsOn; //if oponent is on fire
    public float onFireMod; //spawnmodifire for onFire
    public int count;   //actual fire count
    public int fireCount; //count to win
    public float fireDelay;
    
    // Start is called before the first frame update
    void Start()
    {
        spawning = false;
        SetInterval();
        interactionTimer = interactionInterval;
        count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (spawning)
        {
            float timeFrame = Time.deltaTime;

            if (fireTimer > 0)
            {
                fireTimer -= timeFrame;
                return;
            }

            if (actionTimer <= 0)
            {
                if (onFire)
                {
                    if (count < fireCount)
                    {
                        SpawnAction();
                        count++;
                    }
                    else if (count == fireCount) {
                        SpawnLastAction();
                        count++;
                        fireTimer = fireDelay;
                    } else
                    {
                       this.gameObject.transform.parent.GetComponent<Player>().gc.CoolDown();
                    }
                }
                else if (fireIsOn)
                {
                    SpawnAction();
                }
                else if (interactionReady)
                {
                    interactionReady = false;
                    SpawnInteraction();
                }
                else
                {
                    SpawnAction();
                }

                SetTimer();
            }
            
            actionTimer -= timeFrame;
        }
    }

    void SetTimer()
    {
        if (spawnInterval < spawnLimiter)
        {
            actionTimer = spawnLimiter;
        } else
        {
            actionTimer = spawnInterval;
        }
    }

    public void SetLevel(int crowdLvl, int comboLvl, float speed)
    {
        crowdLvL = crowdLvl;
        comboLvL = comboLvl;
        this.speed = speed;
        
        SetInterval();
    }

    void SetInterval()
    {
        if (!onFire)
        {
        spawnInterval = spawnBaseInterval - (comboLvL * comboMod) - (crowdLvL * crowdMod);
        topSpawner.SetSpeed(speed);
        bottomSpawner.SetSpeed(speed);
        }
    }

    public void OnFire(bool onFire)
    {
        if (onFire)
        {
            this.onFire = true;
            spawnInterval -= onFireMod;
        } else
        {
            fireIsOn = true;
        }
    }

    public void EndOnFire()
    {
        onFire = false;
        fireIsOn = false;
        count = 0;

        SetInterval();
    }

    void SpawnAction()
    {
        topSpawner.SpawnRandomAction(false);
        bottomSpawner.SpawnRandomAction(false);
    }

    void SpawnLastAction()
    {
        topSpawner.SpawnRandomAction(true);
        bottomSpawner.SpawnRandomAction(true);
    }

    void SpawnInteraction()
    {
        int a1 = Random.Range(0, 3);
        int a2 = Random.Range(0, 3);
        if (a1 == a2)
        {
            a2 = (a2 + 1) % 3;
        }

        topSpawner.SpawnInteraction(a1);
        bottomSpawner.SpawnInteraction(a2);
    }

    public void InteractionWhenReady()
    {
        interactionReady = true;
    }
}
