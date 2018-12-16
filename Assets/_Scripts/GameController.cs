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
    public bool topGood;
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
    public bool running = false;
    private bool gamesEnd = false;

    private float crowdYPosition;

    // Start is called before the first frame update
    void Start()
    {
        crowdYPosition = crowd.transform.localPosition.y;
        interactionHandler.GameController = this;
        menuManager.SwitchMenu(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !running) StartGame();
        if (Input.GetKeyDown(KeyCode.C) && !running) menuManager.SwitchMenu(3);
        if (Input.GetKeyDown(KeyCode.Space) && gamesEnd) ResetGame();
        if (!running) return;

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
        }
        else if (Mathf.Abs(crowdScore) < 5)
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

    private void ResetGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    void StartGame()
    {
        playerL.end = false;
        playerR.end = false;
        playerL.ms.spawning = true;
        playerR.ms.spawning = true;
        interactionHandler.StartInteractionHandler();
        menuManager.SwitchMenu(2);
        running = true;
    }

    public void EndGame()
    {
        playerL.end = true;
        playerL.ms.spawning = false;
        playerR.end = true;
        playerR.ms.spawning = false;
        gamesEnd = true;
        interactionHandler.StopInteractionHandler();
        menuManager.SwitchMenu(1);
    }

    void HandleCrowd(int pL, int pR)
    {

        crowdScore = Mathf.Clamp((((float)(pR - pL)) / 100), -6, 6);
        crowd.transform.position = new Vector2(Mathf.Clamp(crowdScore*2, -8, 8), crowd.transform.position.y);

        crowdScore = Mathf.Clamp((float)(pR - pL)/100,-6,6);
        crowd.transform.localPosition = new Vector2(Mathf.Clamp(crowdScore,-4,4), crowdYPosition);
        //crowdScore = Mathf.Clamp((((float)(pR - pL)) / 100), -6, 6);
        //crowd.transform.position = new Vector2(Mathf.Clamp(crowdScore*2, -10, 10), crowd.transform.position.y);

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