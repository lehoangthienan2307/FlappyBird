using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoundId {
    JUMP,
    SCORE,
    LOSE
}

[System.Serializable]
public class Sound
{
    public SoundId soundId;

    public AudioClip clip;

    [Range(0f, 1f)]
    public float audioClipVolume=1f;

    public bool isLoop;

    [HideInInspector]
    public AudioSource source;
}