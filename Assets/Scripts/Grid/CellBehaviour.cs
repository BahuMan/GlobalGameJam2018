using UnityEngine;

[SelectionBase]
public class CellBehaviour : MonoBehaviour {

    [SerializeField]
    private Renderer _renderer;

    [SerializeField]
    Color _litColor;
    [SerializeField]
    Color _darkColor;

    [General.EnumFlag]
    public DirectionEnum test;

    private bool _lit = false;
    private bool _dirty = false;

    [ContextMenu("Light")]
    public void Light()
    {
        _lit = true;
        _dirty = true;
    }

    [ContextMenu("Dark")]
    public void Dark()
    {
        _lit = false;
        _dirty = true;
    }

    private void Update()
    {
        if (!_dirty) return;

        _renderer.material.color = _lit ? _litColor : _darkColor;
    }

    public bool GetDirty()
    {
        return _dirty;
    }

    public bool GetLit()
    {
        return _lit;
    }
}
