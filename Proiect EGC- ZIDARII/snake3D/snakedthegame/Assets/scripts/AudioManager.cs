using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioClip pickup_sound, gameover_sound;

    void Awake()
    {
        MakeInstance();
    }

    
    void MakeInstance()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void pickup_sounds()
    {
        AudioSource.PlayClipAtPoint(pickup_sound, transform.position);
    }

    public void dead_sounds()
    {
        AudioSource.PlayClipAtPoint(gameover_sound, transform.position);
    }
}
