using UnityEngine;
using System.Collections;

public class AspectRatioHandler : MonoBehaviour
{
    public SpriteRenderer LetterBoxTop,
                          LetterBoxBottom;
    public bool IsMacAspect = false;

    void Start()
    {
        if (((16 * this.camera.rect.width) / (this.camera.aspect * this.camera.rect.height)) > 9.01f)//this.camera.aspect < 1.61f)
        {
            this.camera.orthographicSize = this.camera.orthographicSize + this.camera.orthographicSize * 0.11f;
            LetterBoxTop.enabled = true;
            LetterBoxBottom.enabled = true;
            IsMacAspect = true;
        }
    }
}