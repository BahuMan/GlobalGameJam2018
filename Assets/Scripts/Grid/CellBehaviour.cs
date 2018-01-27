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
        _light = light;
        _model.material.color = light ? _litColor : _darkColor;
    }

    public bool GetLight()
    {
        return _light;
    }

}
