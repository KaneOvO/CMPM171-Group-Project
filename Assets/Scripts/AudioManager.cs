using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fungus;
using JetBrains.Annotations;

public class AudioManager : MonoBehaviour
{
    public Slider volumeSlider;

    void Start()
    {
        if (FungusManager.Instance.MusicManager.audioSourceMusic.volume != 1)
        {
            volumeSlider.value = FungusManager.Instance.MusicManager.audioSourceMusic.volume;
        }
    }

    public void SetVolume()
    {
        FungusManager.Instance.MusicManager.SetAudioVolume(volumeSlider.value, 1f, null);
    }

    public void SavePlayerConfig()
    {
        Invoke(nameof(Save), 0.5f);

    }

    void Save()
    {
        string playerConfigJson = JsonUtility.ToJson(GameManager.Instance.playerConfig);
        PlayerPrefs.SetString("PlayerConfig", playerConfigJson);
        PlayerPrefs.Save();
    }



}
