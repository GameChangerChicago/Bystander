using UnityEngine;
using System.Collections;

public class PartyCameraManager : MonoBehaviour
{
    public float DefaultSize;
    public bool MovingToNext;

    private CursorHandler _cursorHandler;
    private PartyGameManager _myGameManager;
    private Vector3 _pos,
                    _startPos;
    private float _camTravelTime,
                  _camSize,
                  _camSizeDiff,
                  _camRotation;
    private bool _movingCamera,
                 _movingBack,
				 _recentImportantProp;

    void Start()
    {
        _myGameManager = FindObjectOfType<PartyGameManager>();
        _startPos = this.transform.position;
        _cursorHandler = FindObjectOfType<CursorHandler>();
    }

    void Update()
    {
        //MoveCameraTo() needs to happen in an update to function so its regulated by the bool _movingCamera
        if (_movingCamera)
        {
            MoveCameraTo();
        }
    }

    //This method handles moving the camera from place to place and also handles camera size
    private void MoveCameraTo()
    {
        //Initializing MoveTo values
        Hashtable MoveValues = new Hashtable();
        MoveValues.Add("x", _pos.x);
        MoveValues.Add("y", _pos.y);
        MoveValues.Add("z", this.transform.position.z);
        MoveValues.Add("time", _camTravelTime);
        MoveValues.Add("easetype", iTween.EaseType.easeOutQuad);

        //Initializing RotateTo values
        Hashtable RotateValues = new Hashtable();
        RotateValues.Add("z", _camRotation);
        RotateValues.Add("time", _camTravelTime);
        RotateValues.Add("easetype", iTween.EaseType.easeOutQuad);

        //Using the iTween method MoveTo we set an object to move, a location, and a speed; iTween handles the rest
        iTween.MoveTo(this.gameObject, MoveValues);

        //Using the iTween method RotateTo to handle rotation
        iTween.RotateTo(this.gameObject, RotateValues);

        //If the current camera size is greater than _camSize then we know we need to decrease the size of the camera
        if (this.camera.orthographicSize > _camSize)
        {
            this.camera.orthographicSize -= _camSizeDiff / (_camTravelTime / Time.deltaTime);

            //Eventually the camera size will be slightly smaller than the desired size; once this happens we simply set the size to _camSize
            if (this.camera.orthographicSize < _camSize)
                this.camera.orthographicSize = _camSize;
        }
        else if (this.camera.orthographicSize < _camSize) //If the current camera size is less than _camSize then we know we need to increase the size of the camera
        {
            this.camera.orthographicSize += _camSizeDiff / (_camTravelTime / Time.deltaTime);

            //Eventually the camera size will be slightly larger than the desired size; once this happens we simply set the size to _camSize
            if (this.camera.orthographicSize > _camSize)
                this.camera.orthographicSize = _camSize;
        }
    }

    //This method initializes the moving process; setting impertinant fields and also invokes StopMoving
    public void SetCameraToMove(Vector3 pos, float camTravelTime, float camSize, float camRotation, bool imporant)
    {
        _pos = pos;
        _camTravelTime = camTravelTime;

        if (this.GetComponent<AspectRatioHandler>().IsMacAspect)
        {
            DefaultSize = this.camera.orthographicSize;
            _camSize = camSize + camSize * 0.11f;
        }
        else
            _camSize = camSize;

        _camSizeDiff = Mathf.Abs(this.camera.orthographicSize - _camSize);
        _movingCamera = true;
        _camRotation = camRotation;
        Invoke("StopMoving", camTravelTime);
		_recentImportantProp = imporant;
        _myGameManager.CameraMoving = true;
        _cursorHandler.ChangeCursor(2);
    }

    //This method moves the camera back to it's orrigin pos and size; also it invokes StopMoving
    public IEnumerator ReturnCamera(Vector3 pos, float viewTime, float camRotation)
    {
        yield return new WaitForSeconds(viewTime);

        if (this.camera.orthographicSize > DefaultSize)
            _camSizeDiff = this.camera.orthographicSize - DefaultSize;
        else
            _camSizeDiff = DefaultSize - this.camera.orthographicSize;

        _pos = pos;
        _camSize = DefaultSize;
        _camRotation = camRotation;
        _movingCamera = true;
        _movingBack = true;
        _myGameManager.CameraMoving = true;
        Invoke("StopMoving", _camTravelTime);
    }

    //StopMoving stops the movement of the camera by setting _movingCamera, a bool used to determin whether the camera is moving, to false, which will cause the camera, which up to this point was moving, to stop the afformentioned moving. This is useful for when we want the camera to no longer move. -Mojo Jojo
    private void StopMoving()
    {
        _movingCamera = false;
		if(_recentImportantProp)
		{
			_myGameManager.CameraMoving = false;
			_recentImportantProp = false;
		}

        if (_movingBack || MovingToNext)
        {
            _cursorHandler.ChangeCursor(1);
            _movingBack = false;
            MovingToNext = false;
        }
    }

    public void ResetCam()
    {
        this.transform.position = _startPos;
    }
}