using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public Player playerL;
    public Player playerR;
    public GameObject crowd;
    public int crowdLvL = 0;
    public float crowdScore = 0;
    public Object interactor;
    bool topGood;
    float gameTime = 0;
    public int minPlaytime;
    public int timeModInterval;
    public int maxTimeMod;
    public GameObject[] lights;
    public GameObject[] lightsL;
    public GameObject[] lightsR;
    bool fire = false;
    bool onFire = false;
    public Sprite[] crowdSprites;
    bool running = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) StartGame();
        if (!running) return;

        crowd.GetComponent<SpriteRenderer>().sprite = crowdSprites[(int)gameTime % 4];

        HandleCrowd(playerL.score, playerR.score);
        if (crowdLvL != (int)crowdScore)
        {
            crowdLvL = (int)crowdScore;
            playerL.UpdateLvLs(-Mathf.Min(crowdLvL, 0));
            playerR.UpdateLvLs(Mathf.Max(crowdLvL, 0));
        }

        if (Mathf.Abs(crowdScore) >= 5 && !fire && !onFire)
        {
           fire = true;
           foreach (GameObject go in lights)
            {
                go.SetActive(true);
            }
        } else if (Mathf.Abs(crowdScore) < 5)
        {
            fire = false;
            foreach (GameObject go in lights)
            {
                go.SetActive(false);
            }
        }

        gameTime += Time.deltaTime;
        if (minPlaytime < gameTime)
        {
            int timeMod = 1 + Mathf.Min((int)gameTime - minPlaytime / timeModInterval, maxTimeMod);
            playerL.timeMod = timeMod;
            playerR.timeMod = timeMod;
        }
    }

    void StartGame()
    {
        playerL.end = false;
        playerR.end = false;
        playerL.ms.spawning = true;
        playerR.ms.spawning = true;
        running = true;
    }

    public void EndGame()
    {
        playerL.end = true;
        playerL.ms.spawning = false;
        playerR.end = true;
        playerR.ms.spawning = false;
    }

    void HandleCrowd(int pL, int pR)
    {
        crowdScore = Mathf.Clamp((((float)(pR - pL))/100),-6,6);
        crowd.transform.position = new Vector2(Mathf.Clamp(crowdScore,-5,5), -7);
    }

    public void OnFire(Player player)
    {
        if (fire)
        {
            fire = false;
            onFire = true;
            if (player == playerL)
            {
                playerL.OnFire(true);
                playerR.OnFire(false);
                foreach (GameObject go in lightsL)
                {
                    go.SetActive(true);
                }
            }
            else
            {
                playerL.OnFire(false);
                playerR.OnFire(true);
                foreach (GameObject go in lightsR)
                {
                    go.SetActive(true);
                }
            }
            foreach (GameObject go in lights)
            {
                go.SetActive(false);
            }
        }
    }

    public void CoolDown()
    {
        onFire = false;
        playerL.CoolDown();
        playerR.CoolDown();

        foreach (GameObject go in lightsL)
        {
            go.SetActive(false);
        }
        
        foreach (GameObject go in lightsR)
        {
            go.SetActive(false);
        }
    }

    public void SpawnInteraction(bool topGood)
    {
        this.topGood = topGood;
        playerL.InteractionTime();
        playerR.InteractionTime();
    }
}
