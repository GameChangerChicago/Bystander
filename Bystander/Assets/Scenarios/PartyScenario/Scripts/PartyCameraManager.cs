using UnityEngine;
using System.Collections;

public class PartyCameraManager : MonoBehaviour
{
    public float DefaultSize;

    private Vector3 _pos;
    private float _camTravelTime,
                  _camSize,
                  _camSizeDiff;
    private bool _movingCamera;

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
        //Using the iTween method MoveTo we set an object to move, a location, and a speed; iTween handles the rest
        iTween.MoveTo(this.gameObject, new Vector3(_pos.x, _pos.y, this.transform.position.z), _camTravelTime);

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
    public void SetCameraToMove(Vector3 pos, float camTravelTime, float camSize)
    {
        _pos = pos;
        _camTravelTime = camTravelTime;
        _camSize = camSize;
        _camSizeDiff = this.camera.orthographicSize - camSize;
        _movingCamera = true;
        Invoke("StopMoving", camTravelTime);
    }

    //This method moves the camera back to it's orrigin pos and size; also it invokes StopMoving
    public IEnumerator ReturnCamera(Vector3 pos, float viewTime)
    {
        yield return new WaitForSeconds(viewTime);

        if (this.camera.orthographicSize > DefaultSize)
            _camSizeDiff = this.camera.orthographicSize - DefaultSize;
        else
            _camSizeDiff = DefaultSize - this.camera.orthographicSize;

        _pos = pos;
        _camSize = DefaultSize;
        _movingCamera = true;
        Invoke("StopMoving", _camTravelTime);
    }

    //StopMoving stops the movement of the camera by setting _movingCamera, a bool used to determin whether the camera is moving, to false which will cause the camera, which up to this point was moving, to stop the afformentioned moving. This is useful for when we want the camera to no longer move.
    private void StopMoving()
    {
        _movingCamera = false;
    }
}