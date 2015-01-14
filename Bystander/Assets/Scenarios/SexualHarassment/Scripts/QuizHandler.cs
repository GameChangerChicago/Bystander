using UnityEngine;
using System.Collections;

//This script is new ;)
public class QuizHandler : MonoBehaviour
{
    private SHVigilHandler _myVirgil;
    private PointOfInterest _currentPOI;

    void Start()
    {
        _myVirgil = FindObjectOfType<SHVigilHandler>();
    }

    //ShowQuiz handles showing the quiz and setting up it's quiz buttons
    public IEnumerator ShowQuiz(float waitTime, Vector3 instantiationPosition, string virgilString, bool isSexualHarassment, bool simpleQuiz)
    {
        yield return new WaitForSeconds(waitTime);

        //This line moves the quiz into postion
        this.transform.position = instantiationPosition;

        if (simpleQuiz)
        {
            //Set's the correct dialog string in Virgil
            _myVirgil.DialogString = virgilString;

            //This for loop sets the impertinant bools for each QuizButton based on whether the point of interest was sexual harassment
            foreach (QuizButton qb in this.GetComponentsInChildren<QuizButton>())
            {
                if (isSexualHarassment)
                {
                    //If the answer is  yes and it is a case of sexual harrasment then the qb will be marked as a Game winner
                    if (qb.name == "Answer Yes")
                    {
                        qb.CorrectAnswer = true;
                        qb.GameWinner = true;
                    }
                    else
                        qb.CorrectAnswer = false;
                }
                else
                {
                    if (qb.name == "Answer Yes")
                        qb.CorrectAnswer = false;
                    else
                        qb.CorrectAnswer = true;
                }
            }
        }
        else
        {
            //Looks for the quiz that isn't this and moves it off screen
            QuizHandler[] quizes = FindObjectsOfType<QuizHandler>();
            for (int i = 0; i < quizes.Length; i++)
            {
                if (quizes[i] != this)
                    quizes[i].transform.position = new Vector3(0, 0, 500);
            }
        }
    }
}
