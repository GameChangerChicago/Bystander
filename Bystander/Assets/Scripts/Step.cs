using UnityEngine;
using System.Collections;

public class Step : MonoBehaviour
{
    public Animation MyAnimation;
    public AnimationClip MyAnimationClip;
    public Rect CamRectangle;
    public Vector2 CamLocation;
    public float CamSize,
                 CamTravelTime;
    public bool CameraChange;
}