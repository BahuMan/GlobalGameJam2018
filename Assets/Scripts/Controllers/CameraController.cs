using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    private float _lerpAmt = 0;
    private bool _zoomedOut = false;
    private Camera _camera;
    public Transform PlayerPos;
    

    public Transform ZoomOutLocation;

    public float CameraOffsetY = 2.3f;
    public float CameraOffSetZ = -2.3f;
    public float CameraOffSetX = 0;
    private Vector3 CameraOffSet;

    // Use this for initialization
    void Start () {
        _camera = Camera.main;
        CameraOffSet = new Vector3(CameraOffSetX, CameraOffsetY, CameraOffSetZ);
	}
	
	// Update is called once per frame
	void Update () {
        if (!_zoomedOut)
            FollowPlayer();

        ZoomOut();

    }

    private void FollowPlayer()
    {
        _camera.transform.position = Vector3.Lerp(transform.position, PlayerPos.position + CameraOffSet, 0.5f);
        //_camera.transform.LookAt(PlayerPos.position);

        Vector3 targetRotation = PlayerPos.position - (PlayerPos.position + CameraOffSet);
        Quaternion lookDirection = Quaternion.LookRotation(targetRotation);

        _camera.transform.rotation = Quaternion.Lerp(_camera.transform.rotation, lookDirection, 0.125f);

    }

    private void ZoomOut()
    {
        if (Input.GetButton("CameraZoom"))
        {
            _zoomedOut = true;
            if(_lerpAmt < 1)
            {
                _camera.transform.position = Vector3.Lerp(PlayerPos.position + CameraOffSet, ZoomOutLocation.position, _lerpAmt);
                _camera.transform.eulerAngles = Vector3.Lerp(_camera.transform.eulerAngles, ZoomOutLocation.eulerAngles, _lerpAmt);
            }

            _lerpAmt += Time.deltaTime * 2;
        }

        if (Input.GetButtonUp("CameraZoom"))
        {
            _zoomedOut = false;
            _lerpAmt = 0;

        }
    }
}
