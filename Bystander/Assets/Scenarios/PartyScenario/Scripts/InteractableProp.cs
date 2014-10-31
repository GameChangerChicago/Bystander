using UnityEngine;
using System.Collections;

public class InteractableProp : MonoBehaviour
{
    private PartyGameManager _myGameManager;

    void Start()
    {
        _myGameManager = FindObjectOfType<PartyGameManager>();
    }

    void Update()
    {

    }
}
