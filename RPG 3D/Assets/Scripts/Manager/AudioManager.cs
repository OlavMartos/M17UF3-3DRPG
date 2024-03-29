using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioClip music;
    public AudioSource audioSource;
    public List<AudioClip> sounds;


    private void Awake()
    {
        if (instance != null && instance != this) Destroy(gameObject);
        else instance = this;

        audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void Death() { audioSource.PlayOneShot(sounds[0]); }

    public void OpenDoor() { audioSource.PlayOneShot(sounds[1]); }

    public void Win() { audioSource.PlayOneShot(sounds[2]); }
}
