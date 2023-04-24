using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource music;
    public AudioSource soundEffect;
    public AudioClip mainMenuTheme;
    public AudioClip[] TankThemes;
    public AudioClip buttonClickedSound;
    public AudioClip advanceLevelSound;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ChangeToMenuAudio(){
        ChangeAudioClip(mainMenuTheme, music);
    }
    public void ChangeToThemeSong(int tankNumber){
        ChangeAudioClip(TankThemes[tankNumber-1], music);
    }

    private void ChangeAudioClip(AudioClip newClip, AudioSource source){
        if (source != null && source.clip != newClip){
            source.clip = newClip;
            source.Play();
        }
    }


    public void PlayButtonClicked(){
        ChangeAudioClip(buttonClickedSound, soundEffect);
    }

    public void PlayAdvanceLevelSound(){
        ChangeAudioClip(advanceLevelSound, soundEffect);
    }
}
