using UnityEngine;
using System.Collections;

public class SHGameManager : MonoBehaviour
{
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

    void Start()
    {
        
    }

    void Update()
    {

    }
}
