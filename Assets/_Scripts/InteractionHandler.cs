using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionHandler : MonoBehaviour
{
    private static InteractionHandler instance;

    [SerializeField]
    private GameController gameController;
    [SerializeField]
    private int decisionFieldAliveTime;
    [SerializeField]
    private bool debug;
    [SerializeField]
    private DecisionField[] upperFields;
    [SerializeField]
    private DecisionField[] lowerFields;
    [SerializeField]
    private float lowerBoundIntervalBetweeenInteractions;
    [SerializeField]
    private float upperBoundIntervalBetweenInteractions;

    private bool isRunning = false;

    private bool wasLastUpperDecisionGood;

    private float waitTime;
    private float currentTime;

    private int currentState = 0;

    public bool IsRunning
    {
        get
        {
            return isRunning;
        }

        set
        {
            isRunning = value;

        }
    }



    public int DecisionFieldAliveTime
    {
        get
        {
            return decisionFieldAliveTime;
        }

        set
        {
            decisionFieldAliveTime = value;
        }
    }

    public GameController GameController
    {
        get
        {
            return gameController;
        }

        set
        {
            gameController = value;
        }
    }

    public static InteractionHandler GetInstance()
    {
        return instance;
    }

    public void Awake()
    {
        instance = this;
    }

    public void StartInteractionHandler()
    {
        currentState = 0;
        
        isRunning = true;
        currentTime = 0;
        
    }

    public void StopInteractionHandler()
    {
        isRunning = false ;
    }

    public void Update()
    {
        if (isRunning)
        {
            switch (currentState)
            {
                case 0:
                    WaitingForCreatingNewDecision();
                    break;
                case 1:
                    WaitingBeforeSendingDecisionToController();
                    break;
            }
            
        }
    }

    private void SwitchState(int newState)
    {
        currentState = newState;
        currentTime = 0;
        switch (newState)
        {
            case 0:
                waitTime = Random.Range(lowerBoundIntervalBetweeenInteractions, upperBoundIntervalBetweenInteractions);
                break;
            case 1:
                waitTime = decisionFieldAliveTime;
                break;
        }

    }

    private void WaitingForCreatingNewDecision()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= waitTime)
        {
            wasLastUpperDecisionGood = Random.Range(0, 100) > 49;
            
            SwitchState(1);
            SendMessageToDecisionFields(wasLastUpperDecisionGood);
        }
    }

    private void WaitingBeforeSendingDecisionToController()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= waitTime)
        {
            SwitchState(0);
            SendMessageTo(wasLastUpperDecisionGood);
        }
    }

    private void SendMessageToDecisionFields(bool isUpperAGoodInteraction)
    {
        for(int i = 0; i < upperFields.Length; i++)
        {
            upperFields[i].OnReceiveDecision(Dialogues.GetDialogue(isUpperAGoodInteraction),isUpperAGoodInteraction);
            lowerFields[i].OnReceiveDecision(Dialogues.GetDialogue(!isUpperAGoodInteraction), !isUpperAGoodInteraction);
        }
    }

    private void SendMessageTo(bool isUpperAGoodInteraction)
    {
        // TODO send Message to gameController if next Interaction is good or bad
        if(debug)
        {
            Debug.Log("Message is: " + isUpperAGoodInteraction.ToString());
        }
        gameController.SpawnInteraction(isUpperAGoodInteraction);
    }
}
