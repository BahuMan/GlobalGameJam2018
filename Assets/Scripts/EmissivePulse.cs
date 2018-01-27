using System;
using UnityEngine;

public class EmissivePulse : MonoBehaviour
{
    private Renderer _renderer;
    private Material _material;
    private Color _colorBlack = new Color(1,1,1);
    private CellBehaviour _cellBeh;
	// Use this for initialization
	void Start ()
    {
        _renderer = this.GetComponent<Renderer>();
        _material = _renderer.material;
        _cellBeh = this.GetComponentInParent<CellBehaviour>();
        _cellBeh.OnLightSwitched += Cell_LightSwitched;
        this.enabled = false;
	}

    private void Cell_LightSwitched(bool light)
    {
        this.enabled = light;
        _colorBlack = _cellBeh.SignalColor;
        _material.SetColor("_EmissionColor", Color.black); //always start/end at black (no emisson)
    }

    // Update is called once per frame
    void Update ()
    {
        float emission = Mathf.PingPong(Time.time, 1.0f);
        Color finalColor = _colorBlack * Mathf.LinearToGammaSpace(emission);
        _material.SetColor("_EmissionColor", finalColor);
	}
}
