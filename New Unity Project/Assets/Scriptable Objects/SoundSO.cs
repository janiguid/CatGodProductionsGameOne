using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSO : MonoBehaviour
{
    public AudioSource myAudio;

    public void PlayRockBreak()
    {
        myAudio.Play();
    }
}
