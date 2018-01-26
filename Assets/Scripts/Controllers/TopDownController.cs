﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPressedEventArgs : EventArgs
{
    
}

public class TopDownController : MonoBehaviour {

    private CharacterController _controller;
    public float Speed = 5;
    public bool CanPressButton = false;

    public event EventHandler<ButtonPressedEventArgs> ButtonPressed;

    // Use this for initialization
    void Start () {
        _controller = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
        HandleInput();
	}

    private void HandleInput()
    {
        Movement();
        PressButton();
    }

    private void Movement()
    {
        Vector3 moveVector = Vector3.zero;

        float x = Input.GetAxis("Horizontal") * Speed;
        float z = Input.GetAxis("Vertical") * Speed;

        moveVector = new Vector3(x, 0, z);

        _controller.Move(moveVector * Time.deltaTime);
    }

    private void PressButton()
    {
        // Press button, fire event
        if (Input.GetButtonDown("Fire1"))
        {
            FireButtonPressed();
        }
    }

    private void FireButtonPressed()
    {
        if (ButtonPressed != null)
        {
            ButtonPressed(this, new ButtonPressedEventArgs());
        }
    }
}
