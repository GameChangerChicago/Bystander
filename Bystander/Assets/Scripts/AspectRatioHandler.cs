using UnityEngine;
using System.Collections;

public class AspectRatioHandler : MonoBehaviour
{
    public SpriteRenderer LetterBoxTop,
                          LetterBoxBottom;
    public bool IsMacAspect = false,
                StaticLetterBoxing;
    private float _adjustedOrthographicSize;

    void Start()
    {
        if (((16 * this.camera.rect.width) / (this.camera.aspect * this.camera.rect.height)) > 9.01f)
        {
            _adjustedOrthographicSize = this.camera.orthographicSize;
            this.camera.orthographicSize = this.camera.orthographicSize + this.camera.orthographicSize * 0.11f;
            LetterBoxTop.enabled = true;
            LetterBoxBottom.enabled = true;
            IsMacAspect = true;
        }
    }

    void Update()
    {
        if (IsMacAspect && !StaticLetterBoxing)
        {
            if (_adjustedOrthographicSize != this.camera.orthographicSize - this.camera.orthographicSize * 0.11f)
                _adjustedOrthographicSize = this.camera.orthographicSize - this.camera.orthographicSize * 0.11f;

            LetterBoxTop.gameObject.transform.localPosition = new Vector3(LetterBoxTop.gameObject.transform.localPosition.x, _adjustedOrthographicSize + (LetterBoxTop.gameObject.transform.lossyScale.y / 200), LetterBoxTop.gameObject.transform.localPosition.z);
            LetterBoxBottom.gameObject.transform.localPosition = new Vector3(LetterBoxBottom.gameObject.transform.localPosition.x, -(_adjustedOrthographicSize + (LetterBoxBottom.gameObject.transform.lossyScale.y / 200)), LetterBoxBottom.gameObject.transform.localPosition.z);
        }
    }
}