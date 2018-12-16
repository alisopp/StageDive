using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Musik : MonoBehaviour
{
    public AudioSource musik;
    public AudioSource startGameMusik;
    public AudioSource gameMusik;
    public AudioSource endGameMusik;
    public GameController gc;
    bool started = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gc.running && !started)
        {
            started = true;
            musik.Stop();
            startGameMusik.Play();
            
        }
        if (started)
        {
            if (!startGameMusik.isPlaying)
            {
                gameMusik.Play();
                gameMusik.loop = true;
                return;
            }
            else
            {
                if (!gc.running)
                {
                    gameMusik.loop = false;
                }
                else if (!endGameMusik.isPlaying)
                {
                    if (!gameMusik.isPlaying)
                    {
                        endGameMusik.Play();
                        started = false;
                    }
                }
            }
        }
    }
}
