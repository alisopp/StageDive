using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform spawnpoint;
    public GameObject[] actions;
    public int direction;
    public float speed;
    public int spanChance;
    bool hasMissedLast = false;
    int mod = 0;

    public void SpawnRandomAction(bool end)
    {
        if (hasMissedLast)
        {
            mod += Random.Range(1, 5);
        } else
        {
            mod = 0;
        }
        if (Random.Range(0, 100) + mod > spanChance)
        {
            int index = Random.Range(0, 2);
            GameObject action = Instantiate(actions[index], spawnpoint);
            action.GetComponent<Action>().onFireFinish = end;
            action.GetComponent<Action>().Spawn(direction, speed);
            hasMissedLast = false;
        } else
        {
            hasMissedLast = true;
        }
    }

    public void SpawnInteraction(int index)
    {
        GameObject action = Instantiate(actions[index], spawnpoint);
        action.GetComponent<Action>().interaction = true;
        action.GetComponent<Action>().Spawn(direction,speed);
    }

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }
}
