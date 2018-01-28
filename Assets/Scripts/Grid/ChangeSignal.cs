using System;
using UnityEngine;

public class ChangeSignal: MonoBehaviour
{
    public Color _signalColor;
    private CellBehaviour _cell;

    [SerializeField]
    private Renderer _preview;
    [SerializeField]
    private Light _light;

    [ContextMenu("Preview")]
    public void previewColor()
    {
        if (_preview != null)
        {
            //_preview.material.color = _signalColor;
            _preview.material.SetColor("_EmissionColor", _signalColor);
            _light.color = _signalColor;
        }
    }

    private void Start()
    {
        _cell = GetComponent<CellBehaviour>();
        _cell.OnLightSwitched += Cell_OnLightSwitched;
        previewColor();
    }

    private void Cell_OnLightSwitched(bool light)
    {
        //always override outgoing signal color:
        _cell.SignalColor = _signalColor;
    }
}
