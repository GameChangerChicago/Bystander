using UnityEngine;
using System.Collections;

public class Step : MonoBehaviour
{
    public Animator MyAnimator;
    public TextMesh MyTextMesh;
    public GameObject TextBox;
    public Rect CamRectangle;
    public Vector2 CamLocation;
    public string MyText;
    public float CamSize,
                 CamTravelTime,
                 TextBounds;
    public int AnimatorIndex;
}