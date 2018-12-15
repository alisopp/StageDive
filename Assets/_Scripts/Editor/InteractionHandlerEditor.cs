using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(InteractionHandler))]
public class InteractionHandlerEditor : Editor
{

    private InteractionHandler interactionHandler;

    public void OnEnable()
    {
        interactionHandler = target as InteractionHandler;
    }


    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        string buttonText = "is running";
        if(!interactionHandler.IsRunning)
        {
            buttonText = "is not running";
        }

        if(GUILayout.Button("Toogle " + buttonText))
        {
            if(!interactionHandler.IsRunning)
            {
                interactionHandler.StartInteractionHandler();
            }
            else
            {
                interactionHandler.StopInteractionHandler();
            }
        }
    }
}
