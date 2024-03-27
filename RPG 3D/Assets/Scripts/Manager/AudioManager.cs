using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioClip music;
    private AudioSource audioSource;
    public List<AudioClip> sounds;


    private void Awake()
    {
        instance = this;

        audioSource = gameObject.AddComponent<AudioSource>();
    }
}
