using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform spawnpoint;
    public GameObject[] actions;
    public int direction;
    public float speed;

    public void SpawnRandomAction(bool end)
    {
        int index = Random.Range(0, 2);
        GameObject action = Instantiate(actions[index], spawnpoint);
        action.GetComponent<Action>().Spawn(direction);
        action.GetComponent<Action>().speed = this.speed;
        action.GetComponent<Action>().onFireFinish = end;
    }

    public void SpawnInteraction(int index)
    {
        GameObject action = Instantiate(actions[index], spawnpoint);
        action.GetComponent<Action>().Spawn(direction);
        action.GetComponent<Action>().speed = this.speed;
        action.GetComponent<Action>().interaction = true;
    }

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }
}
