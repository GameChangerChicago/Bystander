﻿using UnityEngine;
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
                this.camera.rect = new Rect(0, 0, 1, 1);

                //It then turns off every other camera
                for (int i = 0; i < FindObjectsOfType<SHCameraManager>().Length; i++)
                {
                    if (FindObjectsOfType<SHCameraManager>()[i] != this)
                        this.camera.enabled = false;
                }
            }
            else //When set false the camera is moved and shrunken to it's hold position
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
        //Standard initialization junk
        _myBoxCollider = this.GetComponent<BoxCollider2D>();
    }

    //This method sets IsCurrentCamera to true for this SHCameraManager and sets IsCurrentCamera to false for all others
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

    //This method sets IsCurrentCamera to false and turns on the grayscale effect 
    public void ReturnToHub()
    {
        IsCurrentCamera = false;
        this.GetComponent<GrayscaleEffect>().enabled = true;
    }
}