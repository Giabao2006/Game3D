using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    // [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    private AudioManager audioManager;

    private void Start()
    {
        audioManager = FindFirstObjectByType<AudioManager>();
        musicSlider.value = audioManager.GetMusicVolume();
        sfxSlider.value = audioManager.GetSFXVolume();

        SetMusicVolume(musicSlider.value);
        SetSFXVolume(sfxSlider.value);

        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    public void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("MasterVolume", volume);
    }

    public void SetMusicVolume(float volume)
    {
        audioManager.SetMusicVolume(volume);
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
    }

    public void SetSFXVolume(float volume)
    {
        audioManager.SetSFXVolume(volume);
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20);
    }
    
    private void OnDestroy()
    {
        // masterSlider.onValueChanged.RemoveListener(SetMasterVolume);
        musicSlider.onValueChanged.RemoveListener(SetMusicVolume);
        sfxSlider.onValueChanged.RemoveListener(SetSFXVolume);
    }
}