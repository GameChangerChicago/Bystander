using UnityEngine;
using System.Collections;

public class SHCameraSelector : MonoBehaviour
{
    private SHCameraManager _myCamera;
    private BoxCollider _myBoxCollider;

    void Start()
    {
        _myCamera = this.transform.parent.GetComponent<SHCameraManager>();
        _myBoxCollider = this.GetComponent<BoxCollider>();
    }

    void OnMouseDown()
    {
        if (!_myCamera.IsCurrentCamera)
        {
            _myCamera.FocusOnCamera();
            _myBoxCollider.enabled = false;
        }
    }
}
