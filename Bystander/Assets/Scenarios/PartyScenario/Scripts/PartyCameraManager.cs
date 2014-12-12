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

    void Start()
    {
        
    }

    void Update()
    {
        if (_movingCamera)
        {
            MoveCameraTo();
        }
    }

    private void MoveCameraTo()
    {
        iTween.MoveTo(this.gameObject, new Vector3(_pos.x, _pos.y, this.transform.position.z), _camTravelTime);

        if (this.camera.orthographicSize > _camSize)
        {
            this.camera.orthographicSize -= _camSizeDiff / (_camTravelTime / Time.deltaTime);
            if (this.camera.orthographicSize < _camSize)
                this.camera.orthographicSize = _camSize;
        }
        else if (this.camera.orthographicSize < _camSize)
        {
            this.camera.orthographicSize += _camSizeDiff / (_camTravelTime / Time.deltaTime);
            if (this.camera.orthographicSize > _camSize)
                this.camera.orthographicSize = _camSize;
        }
    }

    public void SetCameraToMove(Vector3 pos, float camTravelTime, float camSize)
    {
        _pos = pos;
        _camTravelTime = camTravelTime;
        _camSize = camSize;
        _camSizeDiff = this.camera.orthographicSize - camSize;
        _movingCamera = true;
        Invoke("StopMoving", camTravelTime);
    }

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

    private void StopMoving()
    {
        _movingCamera = false;
    }
}