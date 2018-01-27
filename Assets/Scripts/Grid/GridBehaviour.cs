using UnityEngine;

public class GridBehaviour : MonoBehaviour
{

    public int width = 5;
    public int length = 5;
    public CellBehaviour[] cellPrefabs;

    [SerializeField]
    private CellBehaviour[] _cells;

    private GeneratorBehaviour[] _generators;
    private ReceiverBehaviour[] _receivers;

    private void Start()
    {
        _generators = GameObject.FindObjectsOfType<GeneratorBehaviour>();
        _receivers = GameObject.FindObjectsOfType<ReceiverBehaviour>();
    }

    [ContextMenu("Create Grid")]
    public void CreateGrid()
    {
        if (_cells != null) foreach (var cell in _cells) Destroy(cell);
        _cells = new CellBehaviour[width * length];

        for (int x = 0; x < width; ++x)
        {
            for (int z = 0; z < length; ++z)
            {
                Vector3 pos = new Vector3(x, 0, z);
                CellBehaviour cell = Instantiate<CellBehaviour>(cellPrefabs[Random.Range(0, cellPrefabs.Length)], pos, Quaternion.identity, this.transform);
                _cells[x * length + z] = cell;
            }
        }
    }

    public CellBehaviour GetCellAt(Vector3 pos)
    {
        return GetCellAt((int)pos.x, (int)pos.z);
    }

    public CellBehaviour GetCellAt(int x, int z)
    {
        if (x < 0 || x > width - 1 || z < 0 || z > length - 1)
        {
            //out of bounds, but perhaps we can find a receiver/goal at this location?
            foreach(var receiver in _receivers)
            {
                int rx = (int) receiver.transform.position.x;
                int rz = (int)receiver.transform.position.z;
                if (x == rx && z == rz)
                {
                    return receiver.GetComponent<CellBehaviour>();
                }
            }
            return null;
        }

        return _cells[x * length + z];
    }

    [ContextMenu("Clear")]
    public void ClearSignal()
    {
        //clear signal:
        for (int x = 0; x < width; ++x)
        {
            for (int z = 0; z < length; ++z)
            {
                GetCellAt(x, z).GoDark();
            }
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
    }

    public void Propagate(DirectionEnum fromWorldDirection, Color beamColor, CellBehaviour cell) {
        if (cell != null && !cell.isLit()) {

            cell.SignalFromDirection(fromWorldDirection, beamColor);
            beamColor = cell.GetSignalColor();

            if (cell.IsOutgoing(DirectionEnum.NORTH)) Propagate(DirectionEnum.SOUTH, beamColor, GetCellAt(cell.transform.position + Vector3.forward));
            if (cell.IsOutgoing(DirectionEnum.EAST))  Propagate(DirectionEnum.WEST,  beamColor, GetCellAt(cell.transform.position + Vector3.right));
            if (cell.IsOutgoing(DirectionEnum.SOUTH)) Propagate(DirectionEnum.NORTH, beamColor, GetCellAt(cell.transform.position - Vector3.forward));
            if (cell.IsOutgoing(DirectionEnum.WEST))  Propagate(DirectionEnum.EAST,  beamColor, GetCellAt(cell.transform.position - Vector3.right));
        }
    }
}