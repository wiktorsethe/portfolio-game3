using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] AudioSource musicAudio;
    [SerializeField] AudioSource[] soundEffectsAudio;
    float musicVolume;
    float soundEffectsVolume;
    private void Start()
    {
        musicVolume = PlayerPrefs.GetFloat("musicVolume");
        soundEffectsVolume = PlayerPrefs.GetFloat("soundEffectsVolume");

        musicAudio = GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>();
        if (musicAudio)
        {
            musicAudio.volume = musicVolume;
        }
        foreach (AudioSource eachSource in soundEffectsAudio)
        {
            eachSource.volume = soundEffectsVolume;
        }

        GameObject[] musicObj = GameObject.FindGameObjectsWithTag("Music");
        if (musicObj.Length > 1) Destroy(musicObj[1]);
        DontDestroyOnLoad(musicObj[0]);
    }
}
