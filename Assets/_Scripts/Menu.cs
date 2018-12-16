using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Menu : MonoBehaviour
{
    [SerializeField]
    private UnityEvent onEnable;
    [SerializeField]
    private UnityEvent onDisable;

    public void Disabe()
    {
        gameObject.SetActive(false);
        onDisable.Invoke();
    }

    public void Enable()
    {
        gameObject.SetActive(true);
        onEnable.Invoke();
    }
}
