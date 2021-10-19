using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class audiosetting : MonoBehaviour
{
    public Slider bgmVolumn;
    public Slider effectVolumn;

    private float backVol = 1f;
    private float effectVol = 1f;

    //background sound
    public AudioSource bgmAudio;
    public AudioSource bossMusic;

    //player's sound
    public AudioSource AttackAudio;
    public AudioSource dashAudio;
    public AudioSource dmgAudio;

    //monster's sound
    public AudioSource hitAudio;
    public AudioSource deadAudio;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        backVol = PlayerPrefs.GetFloat("backVol", 1f);
        bgmVolumn.value = backVol;
        bgmAudio.volume = bgmVolumn.value;
        bossMusic.volume = bgmVolumn.value;

        effectVol = PlayerPrefs.GetFloat("effectVol", 1f);
        effectVolumn.value = effectVol;

        AttackAudio.volume = effectVolumn.value;
        hitAudio.volume = effectVolumn.value;
        deadAudio.volume = effectVolumn.value;
        dashAudio.volume = effectVolumn.value;
        dmgAudio.volume = effectVolumn.value;

    }

    // Update is called once per frame
    void Update()
    {
        SoundSlider();
    }

    public void SoundSlider()
    {
        bgmAudio.volume = bgmVolumn.value;
        bossMusic.volume = bgmVolumn.value;

        backVol = bgmVolumn.value;
        PlayerPrefs.SetFloat("backVol", backVol);

        AttackAudio.volume = effectVolumn.value;
        hitAudio.volume = effectVolumn.value;
        deadAudio.volume = effectVolumn.value;
        dmgAudio.volume = effectVolumn.value;
        dashAudio.volume = effectVolumn.value;

        effectVol = effectVolumn.value;
        
        PlayerPrefs.SetFloat("effectVol", effectVol);

    }
}
