using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownController : MonoBehaviour {

    private CharacterController _controller;
    private Animator _characterController;
    public float Speed = 5;
    public bool CanPressButton = false;

    // Use this for initialization
    void Start () {
        _controller = GetComponent<CharacterController>();
        _characterController = GetComponentInChildren<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        HandleInput();
	}

    private void HandleInput()
    {
        Movement();
    }

    private void Movement()
    {
        Vector3 moveVector = Vector3.zero;

        float x = Input.GetAxis("Horizontal") * Speed;
        float z = Input.GetAxis("Vertical") * Speed;
        Vector2 input = new Vector2(x,z);

        moveVector = new Vector3(x, 0, z);

        _controller.Move(moveVector * Time.deltaTime);

        _characterController.SetFloat("Blend", moveVector.magnitude);

        //MOVEMENT OREINTATION
        Quaternion targetRotation = _characterController.transform.rotation;
        if (input.x < 0 && Mathf.Abs(input.y) < float.Epsilon)//left
            targetRotation = Quaternion.Euler(0, -90, 0);
        else if (Mathf.Abs(input.x) < float.Epsilon && input.y > 0) // forward
            targetRotation = Quaternion.Euler(0, 0, 0);
        else if (input.x > 0 && Mathf.Abs(input.y) < float.Epsilon) // right
            targetRotation = Quaternion.Euler(0, 90, 0);
        else if (Mathf.Abs(input.x) < float.Epsilon && input.y < 0) //Backwards
            targetRotation = Quaternion.Euler(0, 180, 0);

        //Diafonal LF: x -1, y 1
        if (input.x < 0 && input.y > 0)
            targetRotation = Quaternion.Euler(0, -45, 0);
        //Diagonal RF: x 1 y 1 
        if (input.x > 0 && input.y > 0)
            targetRotation = Quaternion.Euler(0, 45, 0);
        //Diagonal LB: X -1 y -1
        if (input.x < 0 && input.y < 0)
            targetRotation = Quaternion.Euler(0, -135, 0);
        //Diagonal RB: x 1, y -1
        if (input.x > 0 && input.y < 0)
            targetRotation = Quaternion.Euler(0, 135, 0);

        float rotationSpeed = 5f;
        _characterController.transform.rotation = Quaternion.Slerp(_characterController.transform.rotation, targetRotation, rotationSpeed);
    }
}
