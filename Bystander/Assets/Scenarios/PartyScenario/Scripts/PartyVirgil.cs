using UnityEngine;
using System.Collections;

public class PartyVirgil : MonoBehaviour
{
    public AudioClip VirgilResetSFX;
    public SpriteRenderer CameraMask;

    private PartyGameManager _myGameManager;
    private AudioSource _myAudioSource;
    private float _maskAlphaValue;
    private bool _maskFadingIn,
                 _maskFadingOut;

    //This method is called to start the animation of Virgil appearing
    public void VirgilReset()
    {
        _myGameManager = FindObjectOfType<PartyGameManager>();
        _myAudioSource = this.GetComponent<AudioSource>();
        _myAudioSource.clip = VirgilResetSFX;
        _myAudioSource.Play();
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
                _myGameManager.EnableAllProps();
            }

            CameraMask.color = new Vector4(0, 0, 0, _maskAlphaValue);
        }
    }
}