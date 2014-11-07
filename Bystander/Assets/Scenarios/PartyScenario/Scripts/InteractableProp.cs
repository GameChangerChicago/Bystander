using UnityEngine;
using System.Collections;

public class InteractableProp : MonoBehaviour
{
    public bool ImportantProp;
    
    private PartyGameManager _myGameManager;

    void Start()
    {
        _myGameManager = FindObjectOfType<PartyGameManager>();
    }

    void OnMouseDown()
    {
        _myGameManager.PlayerClicked(ImportantProp);
    }
}
