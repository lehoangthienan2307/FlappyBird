using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private List<Sound> sounds;
    private void Awake()
    {
        LoadSounds();
    }
    private void LoadSounds()
    {
        foreach (Sound s in sounds)
        {   
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.loop = s.isLoop;       
        }
    }
    public void Play(SoundId name)
    {
        Sound s = sounds.Find(sound => sound.soundId == name);;
        if (s == null)
        {
            Debug.LogError("Sound " + name + " Not Found!");
            return;
        }
        s.source.PlayOneShot(s.clip);
    }
}
