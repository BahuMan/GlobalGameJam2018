using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonScript : MonoBehaviour
{

    [SerializeField]
    private Renderer _beam;

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

        //set color of the beam:
        _beam.material.color = CannonColor;
        _beam.gameObject.SetActive(false);

        //Listen if the cellbehaviour has light
        _cell = GetComponent<CellBehaviour>();
        _cell.OnLightSwitched += _cell_LightSwitched;
        //Set the color of the cannon
        _cannonRenderer = this.GetComponentInChildren<Renderer>();
        _cannonRenderer.material.SetColor("_EmissionColor", CannonColor);
        //make sure powered is false
        Powered = false;
    }

    private void _cell_LightSwitched(bool light)
    {
        Debug.Log(_cell.SignalColor);
        if (light)
        {
            if (ColorEqual(_cell.SignalColor))
            {
                Powered = true;
                StartCoroutine(Flicker(true));
            }
            else
            {
                Powered = false;
                StartCoroutine(Flicker(false));
            }
        }
        else
        {
            _beam.gameObject.SetActive(false);
            _cannonParticles.Stop();
            Powered = false;
        }
    }

    private IEnumerator Flicker(bool endstate)
    {
        bool state = true;
        float flickertime = .5f;

        for (int i=0; i<UnityEngine.Random.Range(3, 8); ++i)
        {
            _beam.gameObject.SetActive(state);
            if (state) _cannonParticles.Play(); else _cannonParticles.Stop();

            state = !state;
            yield return new WaitForSeconds(flickertime);
            flickertime -= .05f;
        }

        _beam.gameObject.SetActive(endstate);
        if (endstate) _cannonParticles.Play(); else _cannonParticles.Stop();
    }

    private bool ColorEqual(Color signalColor)
    {

        Debug.Log("Cannon = " + CannonColor + ", incoming = " + signalColor);

        bool r = (int)(signalColor.r * 100) == (int)(CannonColor.r * 100);
        bool g = (int)(signalColor.g * 100) == (int)(CannonColor.g * 100);
        bool b = (int)(signalColor.b * 100) == (int)(CannonColor.b * 100);

        if (r && g && b)
        {
            return true;
        }
        else return false;
    }
}
