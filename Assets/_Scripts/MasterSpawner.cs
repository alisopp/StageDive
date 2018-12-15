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
                    }
                    else if (fireTimer <= 0)
                    {
                        EndOnFire();
                    }
                    else
                    {
                        fireTimer -= timeFrame;
                    }
                }
                else if (fireIsOn)
                {
                    SpawnAction();

                    //TODO check this out later
                    if (fireTimer < -fireDelay)
                    {
                        EndOnFire();
                    }
                    else
                    {
                        fireTimer -= timeFrame;
                    }
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

                if (fireTimer <= 0)
                {
                    onFire = false;
                    fireIsOn = false;
                    fireTimer = fireDelay;
                }

            }
            
            actionTimer -= timeFrame;
            InteractionDummySpawner(timeFrame);
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

        if (!onFire)
        {
            SetInterval();
        }
    }

    void SetInterval()
    {
        spawnInterval = spawnBaseInterval - (comboLvL * comboMod) - (crowdLvL * crowdMod);
        topSpawner.SetSpeed(speed);
        bottomSpawner.SetSpeed(speed);
    }

    public void OnFire(bool onFire)
    {
        if (onFire)
        {
            this.onFire = true;
            spawnInterval += onFireMod;
        } else
        {
            fireIsOn = true;
        }
    }

    public void EndOnFire()
    {
        onFire = false;
        fireIsOn = false;

        SetInterval();
    }

    void SpawnAction()
    {
        topSpawner.SpawnRandomAction(false);
        bottomSpawner.SpawnRandomAction(false);
        SetInterval();
    }

    void SpawnLastAction()
    {
        topSpawner.SpawnRandomAction(true);
        bottomSpawner.SpawnRandomAction(true);
    }

    void SpawnInteraction()
    {
        int a1 = Random.Range(0, 2);
        int a2 = Random.Range(0, 2);
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

    //TODO remove me when interaction is handeld somewhere more intelligent!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    void InteractionDummySpawner(float deltaTime)
    {
        if (interactionTimer <= 0)
        {
            if (!interactionReady)
            {
                InteractionWhenReady();
            }

            interactionTimer = interactionInterval;
        } else
        {
            interactionTimer -= deltaTime;
        }
    }
}
