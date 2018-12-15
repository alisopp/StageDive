using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    public GameObject triggered;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        triggered = collision.gameObject;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        triggered = null;
    }
}
