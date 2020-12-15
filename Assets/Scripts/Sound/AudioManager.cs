using UnityEngine;
using UnityEngine.Audio;
using System;


public class AudioManager : MonoBehaviour
{
   //creates array of Sounds
   public Sound[] sounds;

   public static AudioManager instance;

   void Awake(){

       if (instance==null)
            instance = this;
       else
       {
           Destroy(gameObject);
           return;
       } 
       DontDestroyOnLoad(gameObject);

        //instantiates the list of sounds for the array
       foreach(Sound s in sounds)
       {
           s.source = gameObject.AddComponent<AudioSource>();
           s.source.clip = s.clip;

           s.source.volume = s.volume;
           s.source.pitch = s.pitch;
           s.source.loop = s.loop;

       }
   }
    
    //plays the default theme on start up
   void Start(){
       //Play("Theme");
   }

    //function to cycle through the array for a name match and then plays the sound
   public void Play (string name)
   {
       Sound s = Array.Find(sounds, sound => sound.name == name);
       if(s==null)
       {
            Debug.Log("Sound: " + name + " not found");
            return;
       }
       s.source.Play();
   }

    //function to cycle through the array for a name match and then stops the sound
   public void Stop (string name)
   {
       Sound s = Array.Find(sounds, sound => sound.name == name);
       if(s==null)
       {
            Debug.Log("Sound: " + name + " not found");
            return;
       }
       s.source.Stop();
   }
}
