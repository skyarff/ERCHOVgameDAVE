using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sounds : MonoBehaviour
{
    public AudioClip[] sounds;
    private AudioSource audioSource => GetComponent<AudioSource>();

    public void PlaySound(AudioClip clip, float volume = 1f, bool destroyed = false, float p1 = 0.85f, float p2 = 1.2f, float p3 = 1)
    {
        audioSource.pitch = p3; // Random.Range(p1, p2);

        if (destroyed)
        {
            AudioSource.PlayClipAtPoint(clip, transform.position);
        }
        else
        {
            audioSource.PlayOneShot(clip, volume);
        }
    }

    public bool AudioIsActive()
    {
        return audioSource.isPlaying;
    }

    public void AudioStop()
    {
        audioSource.Stop();
    }
}
