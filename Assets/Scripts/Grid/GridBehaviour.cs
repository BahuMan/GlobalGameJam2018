using UnityEngine;

public class GridBehaviour : MonoBehaviour
{
    public delegate void SignalPropagatedHandler();
    public event SignalPropagatedHandler OnSignalPropagated;

    public int width = 5;
    public int length = 5;
    public CellBehaviour[] cellPrefabs;

    private CellBehaviour[] _allCells;
    private GeneratorBehaviour[] _generators;
    private ReceiverBehaviour[] _receivers;

    private void Start()
    {
        _generators = GameObject.FindObjectsOfType<GeneratorBehaviour>();
        _receivers = GameObject.FindObjectsOfType<ReceiverBehaviour>();
        _allCells = GameObject.FindObjectsOfType<CellBehaviour>();
        Propagate();
    }

    [ContextMenu("Create Grid")]
    public void CreateGrid()
    {
        for (int x = 0; x < width; ++x)
        {
            for (int z = 0; z < length; ++z)
            {
                Vector3 pos = new Vector3(x, 0, z);
                CellBehaviour cell = Instantiate<CellBehaviour>(cellPrefabs[Random.Range(0, cellPrefabs.Length)], pos, Quaternion.identity, this.transform);

                ChangeSignal c = cell.GetComponent<ChangeSignal>();
                if (c != null) c._signalColor = Random.ColorHSV(0f, 1f, .5f, 1f, .5f, 1f);
            }
        }
    }

    public CellBehaviour GetCellAt(Vector3 pos)
    {

        Collider[] colliders = Physics.OverlapSphere(pos, .1f);
        foreach(var col in colliders)
        {
            CellBehaviour cell = col.GetComponentInParent<CellBehaviour>();
            if (cell != null) return cell;
        }
        if (colliders.Length > 0) Debug.LogError("found " + colliders.Length + " colliders, but none contained a cell");
        return null;
    }

    [ContextMenu("Clear")]
    public void ClearSignal()
    {
        foreach (var cell in _allCells)
        {
            cell.GoDark();
        }
    }

    [ContextMenu("Propagate")]
    public void Propagate()
    {

        ClearSignal();

        foreach (GeneratorBehaviour generator in _generators)
        {
            //NORTH
            Propagate(DirectionEnum.SOUTH, generator.signalColor, GetCellAt(generator.transform.position + Vector3.forward));
            //EAST
            Propagate(DirectionEnum.WEST,  generator.signalColor, GetCellAt(generator.transform.position + Vector3.right));
            //SOUTH
            Propagate(DirectionEnum.NORTH, generator.signalColor, GetCellAt(generator.transform.position - Vector3.forward));
            //WEST
            Propagate(DirectionEnum.EAST,  generator.signalColor, GetCellAt(generator.transform.position - Vector3.right));

        }

        if (OnSignalPropagated != null) OnSignalPropagated();
    }

    public void Propagate(DirectionEnum fromWorldDirection, Color beamColor, CellBehaviour cell) {
        if (cell != null && !cell.isLit()) {

            cell.SignalFromDirection(fromWorldDirection, beamColor);
            beamColor = cell.SignalColor;

            if (cell.IsOutgoing(DirectionEnum.NORTH)) Propagate(DirectionEnum.SOUTH, beamColor, GetCellAt(cell.transform.position + Vector3.forward));
            if (cell.IsOutgoing(DirectionEnum.EAST))  Propagate(DirectionEnum.WEST,  beamColor, GetCellAt(cell.transform.position + Vector3.right));
            if (cell.IsOutgoing(DirectionEnum.SOUTH)) Propagate(DirectionEnum.NORTH, beamColor, GetCellAt(cell.transform.position - Vector3.forward));
            if (cell.IsOutgoing(DirectionEnum.WEST))  Propagate(DirectionEnum.EAST,  beamColor, GetCellAt(cell.transform.position - Vector3.right));
        }
    }
}