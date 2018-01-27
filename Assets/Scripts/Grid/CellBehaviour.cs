using UnityEngine;

[SelectionBase]
public class CellBehaviour : MonoBehaviour {

    [SerializeField]
    private Renderer _model;

    [SerializeField]
    private Color _litColor;
    [SerializeField]
    private Color _darkColor;

    [SerializeField]
    [General.EnumFlag]
    private DirectionEnum _incoming;
    [SerializeField]
    [General.EnumFlag]
    private DirectionEnum _accepted;

    [SerializeField]
    [General.EnumFlag]
    private DirectionEnum _outgoing;

    private bool _dirty = false;
    private bool _light = false;

    private Quaternion ROTATE_CLOCK = Quaternion.Euler(0, 90, 0);
    private Quaternion ROTATE_COUNTER = Quaternion.Euler(0, 90, 0);

    public delegate bool IsSignalAcceptedHandler(Vector3 normalizedLocalDirection);
    public IsSignalAcceptedHandler IsSignalAccepted;

    public void SetIncoming(DirectionEnum dir)
    {
        _incoming |= dir;
        _dirty = false;
        SetLight((_incoming & _accepted) > 0);

    }

    public bool GetIncoming(DirectionEnum dir)
    {
        return (_incoming & dir) == dir;
    }

    //can be checked by specialized script to decide whether to update model or not
    public bool isDirty ()
    {
        return _dirty;
    }

    public void ClearDirty()
    {
        _dirty = false;
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

    [ContextMenu("Clockwise")]
    public void RotateClockwise()
    {
        _model.transform.rotation *= ROTATE_CLOCK;
    }

    [ContextMenu("Counter")]
    public void RotateCounterClockwise()
    {
        _model.transform.rotation *= ROTATE_COUNTER;
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

    public void SetLight(bool light)
    {
        Debug.Log("SetLight = " + light);
        _light = light;
        _model.material.color = light ? _litColor : _darkColor;
    }

    public bool GetLight()
    {
        return _light;
    }

}
