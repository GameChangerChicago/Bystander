﻿using UnityEngine;
using System.Collections;

public class Step : MonoBehaviour
{
    public Animator MyAnimator;
    public AudioClip StepClip;
    public TextMesh MyTextMesh;
    public GameObject TextBox;
    public Rect CamRectangle;
    public Vector2 CamLocation;
    public string MyText,
                  SceneToLoad;
    public float CamSize,
                 CamTravelTime,
                 PartyCameraTravelTime,
                 CamRotation,
                 TextBounds,
                 AudioDelay,
                 ClickDelay,
                 InitialDelay;
    public int AnimatorIndex;
    public bool PlayAnimImmediately,
                PlayAudioImmediately,
                AutoStep,
                InitialAutoStep;
}