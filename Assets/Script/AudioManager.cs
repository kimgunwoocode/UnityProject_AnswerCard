using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource CardAudio;
    public AudioClip ShuffleSound;
    public AudioClip OpenSound;

    public void startShuffle_audio()
    {
        CardAudio.loop = true;
        CardAudio.pitch = 2.8f;
        CardAudio.clip = ShuffleSound;
        CardAudio.Play();
    }
    public void stopShuffle_audio()
    {
        CardAudio.loop = false;
        CardAudio.pitch = 1;
        CardAudio.Stop();
    }
    public void open_audio()
    {
        CardAudio.PlayOneShot(OpenSound);
    }
}
