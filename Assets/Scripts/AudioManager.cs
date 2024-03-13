using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fungus;

public class AudioManager : MonoBehaviour
{
    public Slider volumeSlider;

    void Start()
    {
        if(FungusManager.Instance.MusicManager.audioSourceMusic.volume != 1)
        {
            volumeSlider.value = FungusManager.Instance.MusicManager.audioSourceMusic.volume;
        }
    }

    public void SetVolume()
    {
        FungusManager.Instance.MusicManager.SetAudioVolume(volumeSlider.value, 1f, null);
    }

}
