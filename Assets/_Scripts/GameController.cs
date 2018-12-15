using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public Player playerL;
    public Player playerR;
    public GameObject crowd;
    int crowdLvL = 0;
    public float crowdScore = 0;
    bool topGood;
    float gameTime = 0;
    public int minPlaytime;
    public int timeModInterval;
    public int maxTimeMod;
    public GameObject[] lights;
    public GameObject[] lightsL;
    public GameObject[] lightsR;
    public MenuManager menuManager;
    public InteractionHandler interactionHandler;
    bool fire = false;
    bool onFire = false;


    private float crowdYPosition;

    // Start is called before the first frame update
    void Start()
    {
        crowdYPosition = crowd.transform.localPosition.y;
        interactionHandler.GameController = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) StartGame();

        HandleCrowd(playerL.score, playerR.score);
        if (crowdLvL != (int)crowdScore)
        {
            crowdLvL = (int)crowdScore;
            playerL.UpdateLvLs(Mathf.Max(Mathf.Min(-crowdLvL, 0),-5));
            playerR.UpdateLvLs(Mathf.Min(Mathf.Max(crowdLvL, 0),5));
        }

        if (Mathf.Abs(crowdScore) >= 5 && !fire && !onFire)
        {
           fire = true;
           foreach (GameObject go in lights)
            {
                go.SetActive(true);
            }
        } else if (Mathf.Abs(crowdScore) < 5 && fire)
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
            int timeMod = Mathf.Min((int)gameTime - minPlaytime / timeModInterval, maxTimeMod);
            playerL.timeMod = timeMod;
            playerR.timeMod = timeMod;
        }
    }

    void StartGame()
    {
        playerL.ms.spawning = true;
        playerR.ms.spawning = true;
        interactionHandler.StartInteractionHandler();
        menuManager.SwitchMenu(2);
    }

    public void EndGame()
    {
        playerL.ms.spawning = false;
        playerR.ms.spawning = false;
    }

    void HandleCrowd(int pL, int pR)
    {
        crowdScore = Mathf.Clamp((float)(pR - pL)/100,-6,6);
        crowd.transform.localPosition = new Vector2(Mathf.Clamp(crowdScore,-4,4), crowdYPosition);
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

    public void CoolDown(Player player)
    {
        if (playerL == player)
        {
            onFire = false;
            foreach (GameObject go in lightsL)
            {
                go.SetActive(false);
            }
            playerR.CoolDown();
        } else
        {
            onFire = false;
            foreach (GameObject go in lightsL)
            {
                go.SetActive(false);
            }
            playerL.CoolDown();
        }
    }

    public void SpawnInteraction(bool topGood)
    {
        this.topGood = topGood;
        playerL.InteractionTime();
        playerR.InteractionTime();
    }
}
