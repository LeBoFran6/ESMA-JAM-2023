using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Audio_Manager : MonoBehaviour
{

    public Sound[] l_sounds;
    void Awake()
    {
        foreach (Sound s in l_sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            //s.source.outputAudioMixerGroup = Sound.audioMixer;
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(l_sounds, sounds => sounds.objectName == name);
        s.source.Play();
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(l_sounds, sounds => sounds.objectName == name);
        s.source.Stop();
    }
    
    public void LoadLevel()
    {
        SceneManager.LoadScene(1);
    }
    
    public void QuitApplication()
    {
        Application.Quit();
        PlayerPrefs.DeleteAll();
    }
}