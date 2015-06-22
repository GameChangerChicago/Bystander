using UnityEngine;
using System.Collections;

public class HintManager : MonoBehaviour
{
    public SpriteRenderer InstructionSprite;
    public float HintDelay,
                 InitialDelay;

    private float _hintTimer;
    private bool _timerActive = true,
                 _initialHintShown;

    void Update()
    {
        if (_timerActive)
            _hintTimer += Time.deltaTime;

        if (_hintTimer > InitialDelay && !_initialHintShown)
        {
            _hintTimer = 0;
            _timerActive = false;
            _initialHintShown = true;
            InstructionSprite.enabled = true;
        }

        if (_hintTimer > HintDelay && _initialHintShown)
        {
            _hintTimer = 0;
            _timerActive = false;
            InstructionSprite.enabled = true;
        }

        if (!_timerActive && Input.GetKeyDown(KeyCode.Mouse0))
        {
            _timerActive = true;
            InstructionSprite.enabled = false;
        }

        if (Input.anyKeyDown && _initialHintShown)
        {
            _hintTimer = 0;
        }
    }
}