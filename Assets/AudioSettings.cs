using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSettings : MonoBehaviour
{
    private static readonly string BackgroundPref = "BackgroundPref";
    private static readonly string SoundFxPref = "SoundFxPref";
    
    private float backgroundFloat,soundFxFloat;
    public AudioSource backgroundAudio;
    public AudioSource[] soundFxAudio;



    void Awake()
    {
        ContinueSettings();
    }

    private void ContinueSettings()
    {
        backgroundFloat = PlayerPrefs.GetFloat(BackgroundPref);
        soundFxFloat = PlayerPrefs.GetFloat(SoundFxPref);

        backgroundAudio.volume = backgroundFloat;

        for(int i = 0; i < soundFxAudio.Length; i++)
        {
            soundFxAudio[i].volume = soundFxFloat;
        }
    }
}
