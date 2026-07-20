using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum SoundType
{
    GUI,
    GUY1,
    GUY2,
    GUY3,
    GUY4,
    GUY5,
    GUY6,
    GUY7,
    GUY8,
    GUY9,
    GUY10,
    GUY11,
    GUY12,
    GUY13,
    GUY14,
    GUY15,
    GUY16,
    GUY17,
    GUY18,
    GUY19
}

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    [SerializeField] private SoundList[] soundList;
    public static SoundManager instance;
    private AudioSource audioSource;

    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(SoundType soundIndex, int sound, float volume = 1) //fix volumes
    {
        AudioClip[] clips = instance.soundList[(int)soundIndex].Sounds;
        AudioClip desiredClip = clips[sound];
        instance.audioSource.PlayOneShot(desiredClip, volume * Master.instance.soundLevel / 100);
    }
}

[Serializable]
public struct SoundList
{
    public AudioClip[] Sounds { get => sounds; }
    [HideInInspector] public string name;
    [SerializeField] private AudioClip[] sounds;
}