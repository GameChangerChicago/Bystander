using UnityEngine;
using System.Collections;

public class SHCameraSelector : MonoBehaviour
{
    private SHCameraManager _myCamera;
    private BoxCollider _myBoxCollider;

    void Start()
    {
        //Standard initialization junk
        _myCamera = this.transform.parent.GetComponent<SHCameraManager>();
        _myBoxCollider = this.GetComponent<BoxCollider>();
    }

    //Disables the box collider and calls FocusOnCamera
    void OnMouseDown()
    {
        _myCamera.FocusOnCamera();
        _myBoxCollider.enabled = false;
    }
}
