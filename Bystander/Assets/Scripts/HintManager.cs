using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;

public class HintManager : MonoBehaviour
{
    public SpriteRenderer InstructionSprite;
    public ConversationTrigger MyTrigger;
    public float HintDelay,
                 InitialDelay;

    DatabaseManager _databaseManager;
    private float _hintTimer;
    private bool _timerActive = true,
                 _initialHintShown,
                 _fadingIn,
                 _fadingOut;

    void Update()
    {
        if (_timerActive)
            _hintTimer += Time.deltaTime;

        if (_hintTimer > InitialDelay && !_initialHintShown)
        {
            _hintTimer = 0;
            _timerActive = false;
            _initialHintShown = true;
            _fadingIn = true;
        }

        if (_hintTimer > HintDelay && _initialHintShown)
        {
            _hintTimer = 0;
            _timerActive = false;
            _fadingIn = true;
        }

        if (!_timerActive && Input.GetKeyDown(KeyCode.Mouse0))
        {
            _timerActive = true;
            _fadingOut = true;
        }

        if (Input.anyKeyDown && _initialHintShown)
        {
            _hintTimer = 0;
        }

        Fading();
    }

    private void Fading()
    {
        if (_fadingIn && InstructionSprite.color.a < 1)
        {
            InstructionSprite.color = new Color(1, 1, 1, InstructionSprite.color.a + (2f * Time.deltaTime));

            if (InstructionSprite.color.a > 1)
            {
                InstructionSprite.color = new Color(1, 1, 1, 1);
                _fadingIn = false;
            }
        }

        if (_fadingOut && InstructionSprite.color.a > 0)
        {
            InstructionSprite.color = new Color(1, 1, 1, InstructionSprite.color.a - (3f * Time.deltaTime));

            if (InstructionSprite.color.a < 0)
            {
                InstructionSprite.color = new Color(1, 1, 1, 0);
                _fadingOut = false;

                if (MyTrigger != null)
                    MyTrigger.TryStartConversation(MyTrigger.actor);
            }
        }
    }
}