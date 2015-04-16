using UnityEngine;
using System.Collections;

public class SHCameraSelector : MonoBehaviour
{
    private SHGameManager _myGameManager;
    private SHCameraManager _myCamera;
    private BoxCollider _myBoxCollider;

    public MicroScenarios MyMicroScenario;
    public SpriteRenderer MySelectSprite;

    void Start()
    {
        //Standard initialization junk
        _myGameManager = FindObjectOfType<SHGameManager>();
        _myCamera = this.transform.parent.GetComponent<SHCameraManager>();
        _myBoxCollider = this.GetComponent<BoxCollider>();
    }

    void Update()
    {
        Collider2D mouseCollision = Physics2D.OverlapCircle(_myCamera.GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition), 0.01f);

        RaycastHit hit;

        if (Physics.Raycast(_myCamera.GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition), Vector3.forward, out hit))
        {
            if (hit.collider.transform == this.transform)
                MySelectSprite.enabled = true;
        }
        else
            MySelectSprite.enabled = false;
    }

    //Disables the box collider and calls FocusOnCamera
    void OnMouseUp()
    {
        _myCamera.FocusOnCamera();
        _myBoxCollider.enabled = false;
        _myGameManager.CurrentMicroScenario = MyMicroScenario;
        Invoke("ActivationDelay", 0.01f);
    }

    private void ActivationDelay()
    {
        _myGameManager.ActivatePOIs();
    }
}
