using UnityEngine;
using System.Collections;

public class PartyVirgil : MonoBehaviour
{
    private bool _successful;
    private bool _animationPlayed;

    public void Appear(bool successful)
    {
        this.animation.Play();
        _successful = successful;
        _animationPlayed = true;
    }

    void Update()
    {
        if (!this.animation.isPlaying && _animationPlayed)
        {
            _animationPlayed = false;
            this.GetComponent<BoxCollider>().enabled = true;
            StartCoroutine(this.GetComponent<ConvoHandler>().DialogHandler(0));
        }
    }
}