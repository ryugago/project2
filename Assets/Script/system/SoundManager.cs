using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public AudioMixer mixer;
    public AudioSource bgSound;
    public AudioClip[] bglist;
    public static SoundManager instance;

    private Dictionary<string, AudioSource> sfxSources = new Dictionary<string, AudioSource>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
            SceneManager.sceneLoaded += OnsceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        //BgSoundPlay(bglist[0]);
    }

    private void OnsceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        for (int i = 0; i < bglist.Length; i++)
        {
            if (arg0.name == bglist[i].name)
                BgSoundPlay(bglist[i]);
        }
    }

    public void MasterSoundVolume(float val)// ������ ���� ����
    {
        mixer.SetFloat("MasterVolume", MathF.Log10(val) * 20);
    }

    public void BGSoundVolume(float val)// ��� ���� ����
    {
        mixer.SetFloat("BGSoundVolume", MathF.Log10(val) * 20);
    }

    public void SFXVolume(float val)// SFX���� ����
    {
        mixer.SetFloat("SFXVolume", MathF.Log10(val) * 20);
    }

    public void SFXPlay(string sfxName, AudioClip clip, bool loop = false)
    {
        // ������ ������ ���尡 ��� ���̸� �ߺ����� �ʵ��� ó��
        AudioSource existingSource = GameObject.Find(sfxName + "Sound")?.GetComponent<AudioSource>();
        if (existingSource != null && existingSource.isPlaying) return;

        GameObject go = new GameObject(sfxName + "Sound");
        AudioSource audioSource = go.AddComponent<AudioSource>();
        audioSource.outputAudioMixerGroup = mixer.FindMatchingGroups("SFX")[0];
        audioSource.clip = clip;
        audioSource.loop = loop;
        audioSource.Play();

        if (!loop)
        {
            Destroy(go, clip.length);
        }
    }

    public void SFXPlayVolume(string sfxName, AudioClip clip, float volume ,bool loop = false)// SFX ���� ����
    {
        // ������ ������ ���尡 ��� ���̸� �ߺ����� �ʵ��� ó��
        AudioSource existingSource = GameObject.Find(sfxName + "Sound")?.GetComponent<AudioSource>();
        if (existingSource != null && existingSource.isPlaying) return;

        GameObject go = new GameObject(sfxName + "Sound");
        AudioSource audioSource = go.AddComponent<AudioSource>();
        audioSource.outputAudioMixerGroup = mixer.FindMatchingGroups("SFX")[0];
        audioSource.clip = clip;
        audioSource.loop = loop;
        audioSource.volume = volume;
        audioSource.Play();

        if (!loop)
        {
            Destroy(go, clip.length);
        }
    }

    public void SFXStop(string sfxName)// SFX���� ����
    {
        GameObject go = GameObject.Find(sfxName + "Sound");
        if (go != null)
        {
            AudioSource audioSource = go.GetComponent<AudioSource>();
            if (audioSource != null)
            {
                audioSource.Stop();
            }
            Destroy(go);
        }
    }

    public void BgSoundPlay(AudioClip clip)// ��� ���� ����
    {
        if (bgSound.isPlaying)
        {
            bgSound.Stop();
        }

        bgSound.outputAudioMixerGroup = mixer.FindMatchingGroups("BGSound")[0];
        bgSound.clip = clip;
        bgSound.loop = true;
        //bgSound.volume = 0.1f;
        bgSound.Play();
    }
}
