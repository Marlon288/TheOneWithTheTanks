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
    public AudioClip gameOverSound;
    public AudioClip winTheGameSound;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ChangeToMenuAudio(){
        ChangeSong(mainMenuTheme);
    }
    public void ChangeToThemeSong(int tankNumber){
        ChangeSong(TankThemes[tankNumber-1]);
    }

    private void ChangeSong(AudioClip newClip){
        if (music != null && music.clip != newClip){
            music.clip = newClip;
            music.Play();
        }
    }

    private void ChangeSoundEffectClip(AudioClip newClip){
        if (soundEffect != null){
            soundEffect.clip = newClip;
            soundEffect.Play();
        }
    }


    public void PlayButtonClicked(){
        ChangeSoundEffectClip(buttonClickedSound);
    }

    public void PlayAdvanceLevelSound(){
        ChangeSoundEffectClip(advanceLevelSound);
    }

    public void PlayGameOverSound(){
       StartCoroutine(PlaySoundDelayed(gameOverSound, 2.0f)); 
    }

    public void PlayWinningSound(){
       StartCoroutine(PlaySoundDelayed(winTheGameSound, 5.0f));
    }

    private IEnumerator PlaySoundDelayed(AudioClip sound, float delay)
    {
        music.Stop();
        yield return new WaitForSeconds(1.0f); 

        ChangeSoundEffectClip(sound);
        yield return new WaitForSeconds(delay); 

        music.Play();
    }

    
}
