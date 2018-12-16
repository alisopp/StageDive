using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteRotator : MonoBehaviour
{
    public Sprite[] sprites;
    float timer = 0;

    // Update is called once per frame
    void Update()
    {
        if (timer < 0)
        {
            this.GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.Length)];
            timer = 0.5f;
        }
        timer -= Time.deltaTime;
    }
}
