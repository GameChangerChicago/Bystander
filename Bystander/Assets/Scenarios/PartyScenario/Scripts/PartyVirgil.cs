using UnityEngine;
using System.Collections;

public class PartyVirgil : MonoBehaviour
{
    private bool _successful;

    public void Appear(bool successful)
    {
        this.animation.Play();
        _successful = successful;
    }
}