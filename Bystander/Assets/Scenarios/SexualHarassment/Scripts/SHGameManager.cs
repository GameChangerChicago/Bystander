using UnityEngine;
using System.Collections;

public class SHGameManager : MonoBehaviour
{
    private int _sectionsCompleted;

    //This property looks at all of the SHCameraManagers and checks to see which one is the current camera then returns said SHCameraManager
    protected SHCameraManager currentCameraManager
    {
        get
        {
            for (int i = 0; i < FindObjectsOfType<SHCameraManager>().Length; i++)
            {
                if (FindObjectsOfType<SHCameraManager>()[i].IsCurrentCamera)
                {
                    _currentCameraManager = FindObjectsOfType<SHCameraManager>()[i];
                    break;
                }
            }

            return _currentCameraManager;
        }
    }
    private SHCameraManager _currentCameraManager;

    //This property, when set to true, calls ReturnToHub in whichever camera is the current camera.
    public bool SectionComplete
    {
        get
        {
            return sectionComplete;
        }
        set
        {
            if (value)
            {
                //Checks to see if all sections are complete; if so then we'll load the epilogue
                if (_sectionsCompleted == 5)
                    Debug.Log("Load Epilogue");
                else //Otherwise we add one to _sectionsComplete and brings us back to the hub world
                {
                    _sectionsCompleted++;
                    currentCameraManager.ReturnToHub();
                }
            }
        }
    }
    protected bool sectionComplete;
}
