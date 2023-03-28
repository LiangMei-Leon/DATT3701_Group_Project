using UnityEngine.Audio;
using UnityEngine;
using System;
using Random=UnityEngine.Random;



public class AudioManager : MonoBehaviour
{
   
    public Sound[] sounds;

    public static AudioManager instance;

    public 
   
    // Start is called before the first frame update
    void Awake()
    {
        
        if(instance == null){
            instance = this;
        }else{
            Destroy(gameObject);
            return;
        }
        
        DontDestroyOnLoad(gameObject);
        
        foreach (Sound s in sounds){
            s.source =  gameObject.AddComponent<AudioSource>();
            s.source.clip = s. clip;
            s.source.volume  = s.volume;
            s.source.pitch = s. pitch;
            s.source.loop = s.loop;
            s.source.outputAudioMixerGroup = s.group;
        }
    }

   public void Play(String name)
   {
        Sound s = Array.Find(sounds, sound => sound.name== name);
        
        if(s == null ){
            Debug.LogWarning("Sound:" + name+ " not found");
            return;
        }
        
        s.source.Play();
    
        //FindObjectOfType<AudioManager>().Play("");
   
   
   }

   public bool checkIsPlaying(String name)
   {
        Sound s = Array.Find(sounds, sound => sound.name== name);
        
        if(s == null ){
            Debug.LogWarning("Sound:" + name+ " not found");
            return false;
        }
        
        return s.source.isPlaying;
   
   }

   public void randomVolumeAndPitch(String name)
   {
        Sound s = Array.Find(sounds, sound => sound.name== name);
        
        if(s == null ){
            Debug.LogWarning("Sound:" + name+ " not found");
            return;
        }
        
        s.volume = Random.Range(0.5f, 1f);
        s.pitch = Random.Range(0.2f, 1.8f);

        return;
   }

    public void Stop(String name)
   {
        Sound s = Array.Find(sounds, sound => sound.name== name);
        
        if(s == null ){
            Debug.LogWarning("Sound:" + name+ " not found");
            return;
        }
        
        s.source.Stop();
    
   }

}
