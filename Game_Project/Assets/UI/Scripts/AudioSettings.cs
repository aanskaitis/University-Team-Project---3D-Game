using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSettings : MonoBehaviour
{
    FMOD.Studio.Bus Master;
    FMOD.Studio.Bus Music;
    FMOD.Studio.Bus SFX;
    float musicVolume = 0.5f;
    float SFXVolume = 0.5f;
    float masterVolume = 1f;

    // Gets all the buses on startup
    void Awake()
    {
        Music = FMODUnity.RuntimeManager.GetBus("bus:/Master/Music");
        SFX = FMODUnity.RuntimeManager.GetBus("bus:/Master/SFX");
        Master = FMODUnity.RuntimeManager.GetBus("bus:/Master");
    }

    // Changes the volume to the new volume every frame
    void Update()
    {
        Music.setVolume(musicVolume);
        SFX.setVolume(SFXVolume);
        Master.setVolume(masterVolume);
    }

    // These functions set the new volume
    public void MasterVolumeLevel(float newMasterVolume)
    {
        masterVolume = newMasterVolume;
    }

    public void MusicVolumeLevel(float newMusicVolume)
    {
        musicVolume = newMusicVolume;
    }

    public void SFXVolumeLevel(float newSFXVolume)
    {
        SFXVolume = newSFXVolume;
    }
}