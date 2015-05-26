using UnityEngine;
using System.Collections;

public class SHCameraManager : MonoBehaviour
{
    public Vector2 cameraPos;

    //This property handles which camera is being shown and where it should be when it's not the main camera
    public bool IsCurrentCamera
    {
        get
        {
            return isCurrentCamera;
        }
        set
        {
            //If the property is being set to true...
            if (value)
            {
                //The camera's rectangle is position that it encompaces the scene
                //if (this.camera.aspect < 1.61f)
                //    this.camera.rect = new Rect(0.05f, 0, 0.9f, 1);
                //else
                //{
                    this.camera.rect = new Rect(0, 0, 1, 1);
                //}

                this.camera.clearFlags = CameraClearFlags.Skybox;

                //It then turns off every other camera
                for (int i = 0; i < FindObjectsOfType<SHCameraManager>().Length; i++)
                {
                    if (FindObjectsOfType<SHCameraManager>()[i] != this)
                        FindObjectsOfType<SHCameraManager>()[i].camera.enabled = false;
                }
            }
            else //When set false the camera is moved and shrunken to it's hold position
            {
                this.camera.rect = new Rect(cameraPos.x, cameraPos.y, 0.3f, 0.3f);
            }

            isCurrentCamera = value;
        }
    }
    protected bool isCurrentCamera;

    private BoxCollider2D _myBoxCollider;

    void Start()
    {
        //Standard initialization junk
        _myBoxCollider = this.GetComponent<BoxCollider2D>();
    }

    //This method sets IsCurrentCamera to true for this SHCameraManager and sets IsCurrentCamera to false for all others
    public void FocusOnCamera()
    {
        for (int i = 0; i < FindObjectsOfType<SHCameraManager>().Length; i++)
        {
            if (FindObjectsOfType<SHCameraManager>()[i] == this)
            {
                IsCurrentCamera = true;
            }
            else
                FindObjectsOfType<SHCameraManager>()[i].IsCurrentCamera = false;
        }
    }

    //This method sets IsCurrentCamera to false and turns on the grayscale effect 
    public void ReturnToHub()
    {
        IsCurrentCamera = false;
        this.GetComponent<GrayscaleEffect>().enabled = true;

        for (int i = 0; i < FindObjectsOfType<SHCameraManager>().Length; i++)
        {
            FindObjectsOfType<SHCameraManager>()[i].camera.enabled = enabled;
            FindObjectsOfType<SHCameraManager>()[i].camera.clearFlags = CameraClearFlags.Depth;
        }
    }
}
