using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public AudioClip BGMTrack,
                     AmbientTrack,
                     SFXTrack;
    private AudioSource _BGMSource,
                        _ambientSource,
                        _SFXSource;
	private float _volumeIncreaseValue;
    
    void Start()
    {
	    AudioSource[] allAudioSources = GetComponentsInChildren<AudioSource>();
	    for (int i = 0; i < allAudioSources.Length; i++)
		{
	        switch (allAudioSources[i].name)
	        {
				case "BGMSource":
                    _BGMSource = allAudioSources[i];
					if(BGMTrack)
					{
                        _BGMSource.clip = BGMTrack;
						_BGMSource.volume = 0.25f;
                        _BGMSource.Play();
					}
	                break;
	            case "AmbientSource":
		            _ambientSource = allAudioSources[i];
					if(AmbientTrack)
					{
		                _ambientSource.clip = AmbientTrack;
		                _ambientSource.Play();
					}
	                break;
				case "SFXSource":
					_SFXSource = allAudioSources[i];
					break;
	            default:
	                break;
	        }
	    }
    }

    public void PlaySFX(AudioClip newClip, float volume, bool loop)
    {
		_SFXSource.clip = newClip;
		_SFXSource.volume = volume;
		_SFXSource.loop = loop;
        _SFXSource.Play();
    }

	public void ChangeAmbientTrack(AudioClip newClip)
	{
		_ambientSource.clip = newClip;
		_ambientSource.Play();
	}

	public void IncreaseAmbientVolume(float amount)
	{
		_volumeIncreaseValue = amount;
		_ambientSource.volume += amount;
	}

	public void RevertAmbientVolume()
	{
		_ambientSource.volume -= _volumeIncreaseValue;
	}

	public void StopAmbience()
	{
		_ambientSource.Stop();
	}

	public void StopSFX()
	{
		_SFXSource.Stop();
	}
}