using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManuMusicManager : MonoBehaviour
{

    [SerializeField] Slider volumeSlider;

    void Start()
    {
        if(!PlayerPrefs.HasKey("MenuMusicVolume"))
        {
            PlayerPrefs.SetFloat("MenuMusicVolume", 1);
            Load();
        }
        else
        {
            Load();
        }
    }

    void Update()
    {
        
    }
    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
        Save();
    }

    private void Load()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("MenuMusicVolume");
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("MenuMusicVolume", volumeSlider.value);
    }



}
