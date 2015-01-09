using UnityEngine;
using System.Collections;

public class SHGameManager : MonoBehaviour
{
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
                currentCameraManager.ReturnToHub();
            }
        }
    }
    protected bool sectionComplete;
}
