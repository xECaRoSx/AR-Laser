using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private AudioSource voiceSource;
    [SerializeField] private AudioSource bgmSource;
    [SerializeField] private AudioSource sfxSource;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void PlayVoiceovers(List<AudioClip> clips)
    {
        StartCoroutine(PlaySequential(clips));
    }

    private IEnumerator PlaySequential(List<AudioClip> clips)
    {
        foreach (var clip in clips)
        {
            if (clip != null)
            {
                voiceSource.clip = clip;
                voiceSource.Play();
                yield return new WaitWhile(() => voiceSource.isPlaying);
            }
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip != null) sfxSource.PlayOneShot(clip);
    }

    public void SetBGM(AudioClip bgm, bool loop = true)
    {
        bgmSource.clip = bgm;
        bgmSource.loop = loop;
        bgmSource.Play();
    }

    public void StopAll()
    {
        voiceSource.Stop();
        bgmSource.Stop();
        sfxSource.Stop();
    }
}
