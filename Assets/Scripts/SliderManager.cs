using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SliderManager : MonoBehaviour
{
    // Les sources musique et fx 
    [SerializeField, Tooltip("l'audio mixer de musique a mettre")] private AudioMixer m_audioMixer;

    // les Sliders
    [SerializeField, Tooltip("le slider de la musique")] private Slider m_musicSlider;
    [SerializeField, Tooltip("le slider des sfx")] private Slider m_sfxSlider;

    // la valeur maximale par defaut assignee
    private float m_musicVolume;
    private float m_sfxVolume;

    private void Start()
    {
  
        // Au lancement du jeu je vais recuperer les valeurs de mes sliders
        m_musicVolume = PlayerPrefs.GetFloat("music");
        m_sfxVolume = PlayerPrefs.GetFloat("sfx");

        // la valeur du slider est egale a celle du volume recupere
        m_musicSlider.value = m_musicVolume;
        m_sfxSlider.value = m_sfxVolume;

        // J'inite la valeur de base du volume de la musique
        m_musicSlider.value = 1f;
        m_sfxSlider.value = 1f;
    }

    private void Update()
    {
        // Mes valeurs de volumes sont toujours egales a celles recuperees
        m_audioMixer.SetFloat("musicVolume", Mathf.Log10(m_musicVolume) * 20);
        m_audioMixer.SetFloat("sfxVolume", Mathf.Log10(m_sfxVolume) * 20);


        // Je remplace les valeurs recuperables
        PlayerPrefs.SetFloat("music", m_musicVolume);
        PlayerPrefs.SetFloat("sfx", m_sfxVolume);
    }

    // Les fonctions a attribuer aux sliders pour mettre a jour les differentes valeurs 

    public void MusicVolumeUpdater(float volume)
    {
        m_musicVolume = volume;
    }
    public void SFXVolumeUpdater(float volume)
    {
        m_sfxVolume = volume;
    }

    // Les fonctions pour reset les valeurs

    public void VolumeReset()
    {
        PlayerPrefs.DeleteKey("music");
        PlayerPrefs.DeleteKey("sfx");

        m_audioMixer.SetFloat("musicVolume", 1);
        m_audioMixer.SetFloat("sfxVolume", 1);

        m_musicSlider.value = 1;
        m_sfxSlider.value = 1;
    }
}