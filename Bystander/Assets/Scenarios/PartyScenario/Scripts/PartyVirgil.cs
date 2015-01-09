using UnityEngine;
using System.Collections;

public class PartyVirgil : MonoBehaviour
{
    private bool _animationPlayed;

    //This method is called to start the animation of Virgil appearing
    public void Appear()
    {
        this.animation.Play();
        _animationPlayed = true;
    }

    void Update()
    {
        //After the animation has been recently started and the animation finishes Virgil's box collider will be enabled and DialogHandler will be called 
        if (!this.animation.isPlaying && _animationPlayed)
        {
            _animationPlayed = false;
            this.GetComponent<BoxCollider>().enabled = true;
            StartCoroutine(this.GetComponent<ConvoHandler>().DialogHandler(0));
        }
    }
}