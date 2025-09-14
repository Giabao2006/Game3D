using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;

    // Volume keys
    private const string MasterVolumeKey = "MasterVolume";
    private const string MusicVolumeKey = "MusicVolume";
    private const string SFXVolumeKey = "SFXVolume";

    private Dictionary<string, Coroutine> ClipCoroutinePairs = new Dictionary<string, Coroutine>();

    public float GetMasterVolume()
    {
        return PlayerPrefs.GetFloat(MasterVolumeKey, 0.75f);
    }
    public float GetMusicVolume()
    {
        return PlayerPrefs.GetFloat(MusicVolumeKey, 0.75f);
    }
    public float GetSFXVolume()
    {
        return PlayerPrefs.GetFloat(SFXVolumeKey, 0.75f);
    }
    public void SetMasterVolume(float volume)
    {
        PlayerPrefs.SetFloat(MasterVolumeKey, volume);
    }
    public void SetMusicVolume(float volume)
    {
        PlayerPrefs.SetFloat(MusicVolumeKey, volume);
    }
    public void SetSFXVolume(float volume)
    {
        PlayerPrefs.SetFloat(SFXVolumeKey, volume);
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.outputAudioMixerGroup = s.mixerGroup;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;

            // Đặt clip mặc định nếu có sẵn
            s.source.clip = s.GetRandomClip();
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        // Chọn clip ngẫu nhiên nếu sound có mảng clips
        AudioClip clipToPlay = s.GetRandomClip();
        if (clipToPlay != null)
        {
            s.source.clip = clipToPlay;
        }

        s.source.Play();
    }

    // Hàm để dừng phát một âm thanh cụ thể
    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Stop();
    }

    public void PlayRepeat(string name, float delay = 0f)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        ClipCoroutinePairs[name] = StartCoroutine(LoopSound(s, delay));
    }

    public void StopRepeat(string name)
    {
        if (ClipCoroutinePairs.ContainsKey(name))
        {
            StopCoroutine(ClipCoroutinePairs[name]);
            ClipCoroutinePairs.Remove(name);
            Stop(name);
        }
    }

    private IEnumerator LoopSound(Sound s, float delay)
    {
        while (true)
        {
            AudioClip clipToPlay = s.GetRandomClip();

            if (clipToPlay != null)
            {
                s.source.clip = clipToPlay;
            }

            s.source.Play();

            yield return new WaitForSeconds(clipToPlay.length + delay);
        }
    }

    // FindObjectOfType<AudioManager>().Play("NameSound");
    // FindObjectOfType<AudioManager>().Stop("NameSound");
    // ---------------------------------------------------
    // private AudioManager audioManager;
    // audioManager = FindObjectOfType<AudioManager>();
    // audioManager.Play("NameSound"); 
    // ---------------------------------------------------
    // audioManager.Stop("NameSound");
}