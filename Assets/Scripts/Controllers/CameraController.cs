using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    private Camera _camera;
    public Transform PlayerPos;
    

    public Transform ZoomOutLocation;

    public float CameraOffsetY = 2.3f;
    public float CameraOffSetZ = -2.3f;
    private Vector3 CameraOffSet;

    // Use this for initialization
    void Start () {
        _camera = Camera.main;
        CameraOffSet = new Vector3(0, CameraOffsetY, CameraOffSetZ);
	}
	
	// Update is called once per frame
	void Update () {
        FollowPlayer();
        ZoomOut();
    }

    private void FollowPlayer()
    {
        _camera.transform.position = PlayerPos.position + CameraOffSet;
        _camera.transform.LookAt(PlayerPos);
    }

    private void ZoomOut()
    {
        if (Input.GetButton("CameraZoom"))
        {
            _camera.transform.position = ZoomOutLocation.position;
            _camera.transform.eulerAngles = ZoomOutLocation.eulerAngles;
            Debug.Log("Button down");
        }
    }
}
