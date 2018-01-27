using System;
using UnityEngine;

[SelectionBase]
public class CellBehaviour : MonoBehaviour {

    public delegate void LightHandler(bool light);
    public event LightHandler LightSwitched;

    [SerializeField]
    [General.EnumFlag]
    private DirectionEnum _connected;

    private bool _light = false;

    private Quaternion ROTATE_CLOCK = Quaternion.Euler(0, 90, 0);
    private Quaternion ROTATE_COUNTER = Quaternion.Euler(0, 90, 0);

    public void SignalNorth()
    {
        SignalFromDirection(DirectionEnum.NORTH);
    }

    public void SignalSouth()
    {
        SignalFromDirection(DirectionEnum.SOUTH);
    }
    public void SignalEast()
    {
        SignalFromDirection(DirectionEnum.EAST);
    }
    public void SignalWest()
    {
        SignalFromDirection(DirectionEnum.WEST);
    }
    public void SignalFromDirection(DirectionEnum worldDir)
    {
        SetLight((worldDir & _connected) > 0);
    }

    public bool IsOutgoing(DirectionEnum worldDir)
    {
        return _light && (_connected & worldDir) == worldDir;
    }

    [ContextMenu("Clockwise")]
    public void RotateClockwise()
    {
        this.transform.rotation *= ROTATE_CLOCK;
        this._connected = Direction.RotateClockWise(this._connected);
    }

    [ContextMenu("Counter")]
    public void RotateCounterClockwise()
    {
        this.transform.rotation *= ROTATE_COUNTER;
        this._connected = Direction.RotateCounterClockWise(this._connected);
    }

    [ContextMenu("Dark")]
    public void GoDark()
    {
        SetLight(false);
    }

    [ContextMenu("Light")]
    public void GoLight()
    {
        SetLight(true);
    }

    [ContextMenu("Print Connection")]
    public void PrintConnection()
    {
        Debug.Log("NORTH: " + Convert.ToString((int)DirectionEnum.NORTH, 2) + " = " + Direction.ToString(DirectionEnum.NORTH));
        Debug.Log("EAST: " + Convert.ToString((int)DirectionEnum.EAST, 2) + " = " + Direction.ToString(DirectionEnum.EAST));
        Debug.Log("Connections: " + this._connected  + ", bin = " + Convert.ToString((int)this._connected, 2) + " = " + Direction.ToString(this._connected));
    }

    public void SetLight(bool light)
    {
        _light = light;
        if (LightSwitched != null) LightSwitched(light);
    }

    public bool GetLight()
    {
        return _light;
    }

}
