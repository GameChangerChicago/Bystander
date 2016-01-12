using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public AudioClip BGMTrack,
                     AmbientTrack,
                     SFXTrack;
    public bool Enabled;
    private AudioSource _BGMSource,
                        _ambientSource,
                        _SFXSource;
    
    void Start()
    {
        if (Enabled)
        {
            AudioSource[] allAudioSources = GetComponentsInChildren<AudioSource>();
            for (int i = 0; i < allAudioSources.Length; i++)
            {
                switch (allAudioSources[i].name)
                {
                    case "BGMSource":
                        _BGMSource = allAudioSources[i];
                        _BGMSource.clip = BGMTrack;
                        _BGMSource.Play();
                        break;
                    case "AmbientSource":
                        _ambientSource = allAudioSources[i];
                        _ambientSource.clip = AmbientTrack;
                        _ambientSource.Play();
                        break;
                    case "SFXSource":
                        _SFXSource = allAudioSources[i];
                        _SFXSource.clip = SFXTrack;
                        break;
                    default:
                        break;
                }
            }
        }
    }

    public void PlaySFX()
    {
        _SFXSource.Play();
    }
}