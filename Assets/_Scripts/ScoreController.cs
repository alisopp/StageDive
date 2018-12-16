using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    [SerializeField]
    private GameController gameController;
    [SerializeField]
    private UnityEngine.UI.Text winnerText;

    public void HandleScore()
    {
        if(gameController.playerL.score > gameController.playerR.score)
        {
            winnerText.text = "Kevin is the Winner";
        }
        else
        {
            winnerText.text = "Alfredo is the Winner";
        }
    }

}
