using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonScript : MonoBehaviour
{
    private ParticleSystem _cannonParticles;
    public Color CannonColor;
    private CellBehaviour _cell;
    private Renderer _cannonRenderer;
    public bool Powered
    {
        get;
        private set;
    }

	// Use this for initialization
	void Start ()
    {
        //set the color of the particle
        _cannonParticles = this.GetComponentInChildren<ParticleSystem>();
        var main = _cannonParticles.main;
        main.startColor = CannonColor;
        //Listen if the cellbehaviour has light
        _cell = GetComponent<CellBehaviour>();
        _cell.OnLightSwitched += _cell_LightSwitched;
        //Set the color of the cannon
        _cannonRenderer = this.GetComponentInChildren<Renderer>();
        _cannonRenderer.material.SetColor("_EmissionColor", CannonColor);
        //make sure powered is false
        Powered = false;
        Debug.Log(CannonColor);
    }

    private void _cell_LightSwitched(bool light)
    {
        Debug.Log(_cell.SignalColor);
        if (light && ColorEqual(_cell.SignalColor))
        {           
            _cannonParticles.Play();
            Powered = true;
        }
        else
        {
            _cannonParticles.Stop();
            Powered = false;
        }
    }

    private bool ColorEqual(Color signalColor)
    {
        if ((int)signalColor.r*100 == (int)CannonColor.r*100 && (int)signalColor.g*100 == (int)CannonColor.g*100 && (int)signalColor.b*100 == (int)CannonColor.b*100)
        {
            return true;
        }
        else return false;
    }
}
