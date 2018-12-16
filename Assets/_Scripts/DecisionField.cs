using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DecisionField : MonoBehaviour
{
    [SerializeField]
    private Text text;


    public void OnReceiveDecision(string text, bool isGoodDecision)
    {
        this.text.text = text;
        // TODO Play Animation
        gameObject.SetActive(true);
        StartCoroutine(StayOnBoard());
    }


    public IEnumerator StayOnBoard()
    {
        yield return new WaitForSeconds(InteractionHandler.GetInstance().DecisionFieldAliveTime);
        // Todo Play Animation or just deactivate field;
        gameObject.SetActive(false);
    }
}
