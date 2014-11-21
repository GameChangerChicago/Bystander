using UnityEngine;
using System.Collections;

public class SHVigilHandler : MonoBehaviour
{
    private TextMesh _myText;

    void Start()
    {
        _myText = this.GetComponentInChildren<TextMesh>();
    }

    void Update()
    {

    }
}
