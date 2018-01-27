﻿using System;
using UnityEngine;

public class ChangeSignal: MonoBehaviour
{
    public Color _signalColor;
    private CellBehaviour _cell;

    [SerializeField]
    private Renderer _preview;

    private void OnValidate()
    {
        if (_preview != null)
        {
            _preview.material.color = _signalColor;
        }
    }

    private void Start()
    {
        _cell = GetComponent<CellBehaviour>();
        _cell.OnLightSwitched += Cell_OnLightSwitched;
    }

    private void Cell_OnLightSwitched(bool light)
    {
        //always override outgoing signal color:
        _cell.SignalColor = _signalColor;
    }
}
