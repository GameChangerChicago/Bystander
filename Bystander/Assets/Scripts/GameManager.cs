using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    static bool singleScenarioMode;
    public bool SingleScenarioMode
    {
        get
        {
            return singleScenarioMode;
        }
        set
        {
            singleScenarioMode = value;
        }
    }
    
    void Start()
    {

    }

    void Update()
    {

    }
}