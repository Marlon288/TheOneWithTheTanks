using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class OptionsManager : MonoBehaviour
{
    public Slider musicVolumeSlider;
    public Slider soundEffectsVolumeSlider;
    public AudioMixer audioMixer;

    private void Start()
    {
        // Initialize sliders with the current volume values
        float musicVolume;
        float soundEffectsVolume;
        audioMixer.GetFloat("MusicVolume", out musicVolume);
        audioMixer.GetFloat("SFXVolume", out soundEffectsVolume);
        musicVolumeSlider.value = Mathf.Pow(10, musicVolume / 20);
        soundEffectsVolumeSlider.value = Mathf.Pow(10, soundEffectsVolume / 20);
    }

    public void UpdateMusicVolume()
    {   
        float volume = Mathf.Clamp(Mathf.Log10(musicVolumeSlider.value) * 20, -80f, 0f);
        audioMixer.SetFloat("MusicVolume", volume);
        
    }

    public void UpdateSoundEffectsVolume()
    {
        float volume = Mathf.Clamp(Mathf.Log10(soundEffectsVolumeSlider.value) * 20, -80f, 0f);
        audioMixer.SetFloat("SFXVolume", volume);
    }
}
