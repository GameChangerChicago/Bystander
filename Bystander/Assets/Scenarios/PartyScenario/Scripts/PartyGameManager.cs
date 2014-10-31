using UnityEngine;
using System.Collections;

public class PartyGameManager : MonoBehaviour
{
    private PartyCameraManager _myCameraManager;

    void Start()
    {
        _myCameraManager = FindObjectOfType<PartyCameraManager>();
    }

    void Update()
    {

    }
}
