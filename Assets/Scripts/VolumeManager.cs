using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class VolumeManager : MonoBehaviour
{
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider soundEffectsSlider;
    [SerializeField] AudioSource musicAudio;
    [SerializeField] AudioSource[] soundEffectsAudio;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 0.5f);
            LoadMusic();
        }
        else
        {
            LoadMusic();
        }
        if (!PlayerPrefs.HasKey("soundEffectsVolume"))
        {
            PlayerPrefs.SetFloat("soundEffectsVolume", 0.5f);
            LoadSoundEffects();
        }
        else
        {
            LoadSoundEffects();
        }
    }
    public void ChangeMusicVolume()
    {
        musicAudio.volume = musicSlider.value;
        SaveMusic();
    }
    public void ChangeSoundEffectsVolume()
    {
        foreach (AudioSource eachSource in soundEffectsAudio)
        {
            eachSource.volume = soundEffectsSlider.value;
        }
        SaveSoundEffects();
    }
    public void LoadMusic()
    {
        musicAudio = GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>();
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        GameObject[] musicObj = GameObject.FindGameObjectsWithTag("Music");
        if (musicObj.Length > 1) Destroy(musicObj[1]);
        DontDestroyOnLoad(musicObj[0]);
    }
    public void LoadSoundEffects()
    {
        soundEffectsSlider.value = PlayerPrefs.GetFloat("soundEffectsVolume");
    }
    void SaveMusic()
    {
        PlayerPrefs.SetFloat("musicVolume", musicSlider.value);
    }
    void SaveSoundEffects()
    {
        PlayerPrefs.SetFloat("soundEffectsVolume", soundEffectsSlider.value);
    }
}
