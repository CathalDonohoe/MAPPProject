using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    private static readonly string FirstPlay = "FirstPlay";
    private static readonly string BackgroundPref = "BackgroundPref";
    private static readonly string SoundFxPref = "SoundFxPref";
    private int firstPlayInt;
    public Slider backgroundSlider, soundFxSlider;
    private float backgroundFloat, soundFxFloat;
    public AudioSource backgroundAudio; // Single Sound
    public AudioSource[] soundFxAudio; // Array of sounds
    private bool inFocus;

    void Start()
    {
        firstPlayInt = PlayerPrefs.GetInt(FirstPlay);

        if(firstPlayInt ==0)
        {
            backgroundFloat = 0.6f;
            soundFxFloat = 0.6f;
            backgroundSlider.value = backgroundFloat;
            soundFxSlider.value = soundFxFloat;
            PlayerPrefs.SetFloat(BackgroundPref, backgroundFloat);
            PlayerPrefs.SetFloat(SoundFxPref, soundFxFloat);
            PlayerPrefs.SetInt(FirstPlay, -1);
        }
        else
        {
            backgroundFloat = PlayerPrefs.GetFloat(BackgroundPref);
            backgroundSlider.value = backgroundFloat;
            soundFxFloat = PlayerPrefs.GetFloat(SoundFxPref);
            soundFxSlider.value = soundFxFloat;
        }
    }

    public void SaveSoundSettings()
    {
        PlayerPrefs.SetFloat(BackgroundPref, backgroundSlider.value);
        PlayerPrefs.SetFloat(SoundFxPref, soundFxSlider.value);
    }

    private void OnApplicationFocus(bool inFocus)
    {
        if(!inFocus)
        {
            SaveSoundSettings();
        }
    }

    public void UpdateSound()
    {
        backgroundAudio.volume = backgroundSlider.value;

        for(int i =0; i < soundFxAudio.Length; i++)
        {
            soundFxAudio[i].volume = soundFxSlider.value;
        }
    }
}