using UnityEngine;
using System.Collections;

public class Step : MonoBehaviour
{
    public Animation MyAnimation;
    public AnimationClip MyAnimationClip;
    public TextMesh MyTextMesh;
    public GameObject TextBox;
    public Rect CamRectangle;
    public Vector2 CamLocation;
    public string MyText;
    public float CamSize,
                 CamTravelTime,
                 TextBounds;
}