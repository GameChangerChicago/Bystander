using UnityEngine;
using System.Collections;

public class InterventionManager : MonoBehaviour
{
    private string _interventionText;

    void Start()
    {
        _interventionText = Resources.Load("SHText/InterventionText").ToString();
    }

    public void InterventionSetup(ButtonType currentType)
    {
        string interventionText = "";
        char buttonTypeChar = '&';
        bool startPointFound = false;

        for (int i = 0; i < _interventionText.Length; i++)
        {
            if (!startPointFound)
            {
                switch (currentType)
                {
                    case ButtonType.CheckIn:
                        if (_interventionText[i] == '!')
                        {
                            startPointFound = true;
                            buttonTypeChar = _interventionText[i];
                        }
                        break;
                    case ButtonType.Empathy:
                        if (_interventionText[i] == '@')
                        {
                            startPointFound = true;
                            buttonTypeChar = _interventionText[i];
                        }
                        break;
                    case ButtonType.Friends:
                        if (_interventionText[i] == '#')
                        {
                            startPointFound = true;
                            buttonTypeChar = _interventionText[i];
                        }
                        break;
                    case ButtonType.IStatement:
                        if (_interventionText[i] == '$')
                        {
                            startPointFound = true;
                            buttonTypeChar = _interventionText[i];
                        }
                        break;
                    case ButtonType.SilentStare:
                        if (_interventionText[i] == '%')
                        {
                            startPointFound = true;
                            buttonTypeChar = _interventionText[i];
                        }
                        break;
                    default:
                        Debug.LogWarning("Hmm, seems like you got here with a 'yes' or 'no' button type. I'm not sure how that happened...");
                        break;
                }
            }
            else
            {
                if (_interventionText[i] != buttonTypeChar)
                    interventionText += _interventionText[i];
                else
                    break;
            }
        }

        //interventionText needs to be sent into a string formatter so that it stays in bounds
        //Also needs to display the test somewhere
        //Beyond even that is the load the correct animation part
    }
}