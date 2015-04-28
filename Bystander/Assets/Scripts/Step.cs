using UnityEngine;
using System.Collections;

public class Step : MonoBehaviour
{
    public Animator MyAnimator;
    public TextMesh MyTextMesh;
    public GameObject TextBox;
    public Rect CamRectangle;
    public Vector2 CamLocation;
    public string MyText,
                  SceneToLoad;
    public float CamSize,
                 CamTravelTime,
                 CamRotation,
                 TextBounds,
                 ClickDelay;
    public int AnimatorIndex;
    public bool PlayImmediately,
                AutoStep;
}