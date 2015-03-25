using UnityEngine;
using System.Collections;

public class SHCameraSelector : MonoBehaviour
{
    private SHGameManager _myGameManager;
    private SHCameraManager _myCamera;
    private BoxCollider _myBoxCollider;

    public MicroScenarios MyMicroScenario;

    void Start()
    {
        //Standard initialization junk
        _myGameManager = FindObjectOfType<SHGameManager>();
        _myCamera = this.transform.parent.GetComponent<SHCameraManager>();
        _myBoxCollider = this.GetComponent<BoxCollider>();
    }

    //Disables the box collider and calls FocusOnCamera
    void OnMouseDown()
    {
        _myCamera.FocusOnCamera();
        _myBoxCollider.enabled = false;
        _myGameManager.CurrentMicroScenario = MyMicroScenario;
    }
}
