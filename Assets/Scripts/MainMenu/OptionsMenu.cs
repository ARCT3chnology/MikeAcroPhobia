using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] Toggle soundToggle;
    [SerializeField] AudioMixer SoundMixer;

    private void Start()
    {
        SoundMixer.SetFloat("Volume", 0);   
    }

    public void onToggle_SoundToggle()
    {
        if(soundToggle.isOn)
            SoundMixer.SetFloat("Volume", 0);   
        else
            SoundMixer.SetFloat("Volume", -80);   

    }
}
