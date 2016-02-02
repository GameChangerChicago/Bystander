using UnityEngine;
using System.Collections;

public class SHCameraSelector : MonoBehaviour
{
	public AudioClip HoverSound,
					 ClickSound,
					 AmbientTrack;

    protected bool MousedOver
    {
        get
        {
            return _mousedOver;
        }
        set
        {
            if (_mousedOver != value)
            {
                if (value)
				{
					_audioManager.ChangeAmbientTrack(AmbientTrack);
					//_audioManager.PlaySFX(HoverSound, true);
                    _cursorHandler.ChangeCursor(0);
				}
                else
				{
					if(!_clickInitialized)
					{
						_audioManager.StopAmbience();
	                    _cursorHandler.ChangeCursor(1);
					}
				}

                _mousedOver = value;
            }
        }
    }
    private bool _mousedOver;

	private AudioManager _audioManager;
    private CursorHandler _cursorHandler;
    private SHGameManager _myGameManager;
    private SHCameraManager _myCamera;
    private BoxCollider[] _allBoxColliders = new BoxCollider[5];
    private BoxCollider _myBoxCollider;
    private bool _clickInitialized;

    public MicroScenarios MyMicroScenario;
    public SpriteRenderer MySelectSprite;
    public bool Selected;

    void Start()
    {
        //Standard initialization junk
		_audioManager = FindObjectOfType<AudioManager>();
        _cursorHandler = FindObjectOfType<CursorHandler>();
        _myGameManager = FindObjectOfType<SHGameManager>();
        _myCamera = this.transform.parent.GetComponent<SHCameraManager>();
        _myBoxCollider = this.GetComponent<BoxCollider>();

        for (int i = 0; i < 5; i++)
        {
            _allBoxColliders[i] = _myGameManager.AllSelectors[i].GetComponent<BoxCollider>();
        }

        this.enabled = false;
    }

    void Update()
    {
        //Collider2D mouseCollision = Physics2D.OverlapCircle(_myCamera.GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition), 0.01f);

        RaycastHit hit;

        if (Physics.Raycast(_myCamera.GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition), Vector3.forward, out hit))
        {
            if (hit.collider.transform == this.transform)
            {
                MySelectSprite.enabled = true;
                MousedOver = true;
            }
        }
        else
        {
            MySelectSprite.enabled = false;
            MousedOver = false;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && !_clickInitialized)
            _clickInitialized = true;
    }

    //Disables the box collider and calls FocusOnCamera
    void OnMouseUp()
    {
        if (_clickInitialized)
        {
			_audioManager.PlaySFX(ClickSound, false);
            Selected = true;
            _myCamera.FocusOnCamera();
            _myGameManager.CurrentMicroScenario = MyMicroScenario;
            Invoke("ActivationDelay", 0.01f);
			MousedOver = false;
            _clickInitialized = false;

            for (int i = 0; i < 5; i++)
            {
                _allBoxColliders[i].enabled = false;
            }
            //Disables them all but needs to make all the incompleted ones come back
            //Talking about box colliders, not sure I made that clear...
        }
    }

    private void ActivationDelay()
    {
        _myGameManager.ActivatePOIs();
    }
}