using System;
using System.Collections;
using UnityEngine;

[SelectionBase]
public class CellBehaviour : MonoBehaviour {

    public delegate void LightHandler(bool light);
    public event LightHandler OnLightSwitched;

    [SerializeField]
    [General.EnumFlag]
    private DirectionEnum _connected;

    private bool _light = false;
    private Color _signalColor = Color.magenta;
    public Color SignalColor { get { return _signalColor; } set { _signalColor = value; } }

    private Quaternion ROTATE_CLOCK = Quaternion.Euler(0, 90, 0);
    private Quaternion ROTATE_COUNTER = Quaternion.Euler(0, -90, 0);

    private Vector3 _currentRotation;
    private Vector3 _targetRotation;
    private float _lerpAmt;
    private GameObject _player;

    public float LerpSpeed = 1;

    public void SignalFromDirection(DirectionEnum worldDir, Color beamColor)
    {
        if ((worldDir & _connected) > 0)
        {
            _signalColor = beamColor;
            SetLight(true) ;
        }
    }

    public bool IsOutgoing(DirectionEnum worldDir)
    {
        return _light && (_connected & worldDir) == worldDir;
    }

    public void RotateClockwise()
    {
        //this.transform.rotation *= ROTATE_CLOCK;
        this._connected = Direction.RotateClockWise(this._connected);
    }

    public void RotateCounterClockwise()
    {
        //this.transform.rotation *= ROTATE_COUNTER;
        this._connected = Direction.RotateCounterClockWise(this._connected);
    }

    [ContextMenu("Clockwise")]
    public void EditorClockwise()
    {
        this.transform.rotation *= ROTATE_CLOCK;
        this._connected = Direction.RotateClockWise(this._connected);
    }

    [ContextMenu("Counter")]
    public void EditorCounterClockwise()
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
        if (OnLightSwitched != null) OnLightSwitched(light);
    }

    public bool isLit()
    {
        return _light;
    }

    private IEnumerator RotateTile()
    {
        _lerpAmt = 0;
        while (_lerpAmt < 1)
        {
            transform.eulerAngles = Vector3.Lerp(_currentRotation, _targetRotation, _lerpAmt);
            _lerpAmt += Time.deltaTime * LerpSpeed;
            yield return null;
        }

        _player.GetComponent<ButtonScript>().enabled = true;
        _player.GetComponent<TopDownController>().enabled = true;
        _player = null;

        transform.eulerAngles = _targetRotation;

    }

    public void StartRotate(float amt, GameObject player)
    {
        _currentRotation = transform.eulerAngles;
        _targetRotation = new Vector3(0, transform.eulerAngles.y + amt, 0);
        _player = player;
        _player.GetComponent<ButtonScript>().enabled = false;
        _player.GetComponent<TopDownController>().enabled = false;
        StartCoroutine("RotateTile");
    }

}
