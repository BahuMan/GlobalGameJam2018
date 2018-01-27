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

    public delegate bool IsSignalAcceptedHandler(Vector3 normalizedLocalDirection);
    public IsSignalAcceptedHandler IsSignalAccepted;

    private void SetIncoming(DirectionEnum dir)
    {
        SetLight((dir & _connected) > 0);
    }

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
        Quaternion worldRot = Quaternion.Euler(0, Direction.ToAngle(worldDir), 0);
        Quaternion localRot = Quaternion.Inverse(this.transform.rotation) * worldRot;
        DirectionEnum localDir = Direction.FromAngle(localRot.eulerAngles.y);
        SetIncoming(localDir);
    }

    public bool IsOutgoing(DirectionEnum worldDir)
    {
        Quaternion worldRot = Quaternion.Euler(0, Direction.ToAngle(worldDir), 0);
        Quaternion localRot = Quaternion.Inverse(this.transform.rotation) * worldRot;
        DirectionEnum localDir = Direction.FromAngle(localRot.eulerAngles.y);

        return _light && (_connected & localDir) == localDir;
    }

    [ContextMenu("Clockwise")]
    public void RotateClockwise()
    {
        this.transform.rotation *= ROTATE_CLOCK;
    }

    [ContextMenu("Counter")]
    public void RotateCounterClockwise()
    {
        this.transform.rotation *= ROTATE_COUNTER;
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
