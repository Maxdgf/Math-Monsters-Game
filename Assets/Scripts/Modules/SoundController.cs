using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public void config(AudioSource source, AudioClip clip, float pitch)
    {
        source.clip = clip;
        source.pitch = pitch;
    }

    public void makeSound(AudioSource source)
    {
        if (source != null)
        {
            source.Play();
        }
    }
}
