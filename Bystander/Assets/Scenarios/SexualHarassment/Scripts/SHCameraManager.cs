using UnityEngine;
using System.Collections;

public class SHCameraManager : MonoBehaviour
{
    public Vector2 cameraPos;
    public bool IsCurrentCamera
    {
        get
        {
            return isCurrentCamera;
        }
        set
        {
            if (value)
            {
                this.camera.rect = new Rect(0, 0, 1, 1);

                for (int i = 0; i < FindObjectsOfType<SHCameraManager>().Length; i++)
                {
                    if (FindObjectsOfType<SHCameraManager>()[i] != this)
                        this.camera.enabled = false;
                }
            }
            else
            {
                this.camera.rect = new Rect(cameraPos.x, cameraPos.y, 0.2f, 0.2f);
            }

            isCurrentCamera = value;
        }
    }
    protected bool isCurrentCamera;

    private BoxCollider2D _myBoxCollider;

    void Start()
    {
        _myBoxCollider = this.GetComponent<BoxCollider2D>();
    }

    void Update()
    {

    }

    public void FocusOnCamera()
    {
        for (int i = 0; i < FindObjectsOfType<SHCameraManager>().Length; i++)
        {
            if (FindObjectsOfType<SHCameraManager>()[i] == this)
                IsCurrentCamera = true;
            else
                FindObjectsOfType<SHCameraManager>()[i].IsCurrentCamera = false;
        }
    }

    public void ReturnToHub()
    {
        IsCurrentCamera = false;
        this.GetComponentInChildren<SpriteRenderer>().enabled = true;
    }
}
