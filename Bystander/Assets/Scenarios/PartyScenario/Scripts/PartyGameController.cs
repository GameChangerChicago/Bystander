using UnityEngine;
using System.Collections;

public class PartyGameController : MonoBehaviour
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
