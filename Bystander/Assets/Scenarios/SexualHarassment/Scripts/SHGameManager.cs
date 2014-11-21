using UnityEngine;
using System.Collections;

public class SHGameManager : MonoBehaviour
{
    private SHCameraManager _myCameraManager;

    void Start()
    {
        _myCameraManager = FindObjectOfType<SHCameraManager>();
    }

    void Update()
    {

    }
}
