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
            MoveCameraTo(_pos, _camTravelTime, _camSize);
        }
    }

    private void MoveCameraTo(Vector3 pos, float camTravelTime, float camSize)
    {
        iTween.MoveTo(this.gameObject, new Vector3(pos.x, pos.y, this.transform.position.z), camTravelTime);

        if (this.camera.orthographicSize > camSize)
            this.camera.orthographicSize -= _camSizeDiff / (camTravelTime / Time.deltaTime);
        else if (this.camera.orthographicSize < camSize)
            this.camera.orthographicSize += _camSizeDiff / (camTravelTime / Time.deltaTime);
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