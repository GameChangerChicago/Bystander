using UnityEngine;
using System.Collections;

public class PartyVirgil : MonoBehaviour
{
    public AudioSource BGMSource;
    public AudioClip VirgilResetSFX;
    public SpriteRenderer CameraMask;

    private PartyGameManager _myGameManager;
    private AudioSource _myAudioSource;
    private float _maskAlphaValue;
    private int _virgilSFXCount;
    private bool _maskFadingIn,
                 _maskFadingOut;

    //This method is called to start the animation of Virgil appearing
    public void VirgilReset()
    {
        _myGameManager = FindObjectOfType<PartyGameManager>();
        _myAudioSource = this.GetComponent<AudioSource>();
        _myAudioSource.clip = VirgilResetSFX;
        BGMSource.volume = BGMSource.volume / 3;

        if (_virgilSFXCount < 2)
        {
            _virgilSFXCount++;
            BGMSource.volume = BGMSource.volume / 3;
            _myAudioSource.Play();
        }
        else
            _virgilSFXCount = 0;

        _myGameManager.DisableAllProps();
        _maskFadingOut = true;
    }

    void Update()
    {
        //After the animation has been recently started and the animation finishes Virgil's box collider will be enabled and DialogHandler will be called 
        if (_maskFadingOut)
        {
            _maskAlphaValue += 1 / (3 / Time.deltaTime);

            if (_maskAlphaValue >= 1)
            {
                _maskAlphaValue = 1;
                _maskFadingOut = false;
                _maskFadingIn = true;
                _myGameManager.ResetProps();
            }

            CameraMask.color = new Color(0, 0, 0, _maskAlphaValue);
        }

        if (_maskFadingIn)
        {
            _maskAlphaValue -= 1 / (3 / Time.deltaTime);

            if (_maskAlphaValue <= 0)
            {
                _maskAlphaValue = 0;
                _maskFadingIn = false;
                StartCoroutine(_myGameManager.EnableAllProps(0));
                if (BGMSource.volume < 0.3f)
                    BGMSource.volume = BGMSource.volume * 3;
            }

            CameraMask.color = new Vector4(0, 0, 0, _maskAlphaValue);
        }
    }
}