using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphaPulse : MonoBehaviour {

    private Renderer _renderer;
    private Material _material;
    private CellBehaviour _cellBeh;
    private Color _color;
    private Color _baseColor;

    // Use this for initialization
    void Start ()
    {
        _renderer = this.GetComponent<Renderer>();
        _material = _renderer.material;
        _color = _material.color;
        _baseColor = _material.color;
        _cellBeh = GetComponentInParent<CellBehaviour>();
        _cellBeh.OnLightSwitched += Cell_LightSwitched;
        this.enabled = false;
    }

    private void Cell_LightSwitched(bool light)
    {
        this.enabled = light;
        _material.color = _baseColor;
    }

    // Update is called once per frame
    void Update()
    {
        float emission = Mathf.PingPong(Time.time, 1.0f);
        Color finalColor = new Color(_color.r, _color.g, _color.b, _color.a * Mathf.LinearToGammaSpace(emission));
        _material.color = finalColor;
    }
}
