using UnityEngine;

public class GridBehaviour : MonoBehaviour
{

    public int width = 5;
    public int length = 5;
    public CellBehaviour[] cellPrefabs;

    [SerializeField]
    private CellBehaviour[] _cells;

    [SerializeField]
    private GeneratorBehaviour[] _generators;

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
        if (x < 0 || x > width - 1) return null;
        if (z < 0 || z > length- 1) return null;

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


        foreach (GeneratorBehaviour generators in _generators)
        {
            //NORTH
            Propagate(DirectionEnum.NORTH, GetCellAt(generators.transform.position + Vector3.forward));
            //EAST
            Propagate(DirectionEnum.EAST, GetCellAt(generators.transform.position + Vector3.right));
            //SOUTH
            Propagate(DirectionEnum.SOUTH, GetCellAt(generators.transform.position - Vector3.forward));
            //WEST
            Propagate(DirectionEnum.WEST, GetCellAt(generators.transform.position - Vector3.right));

        }
    }

    public void Propagate(DirectionEnum fromWorldDirection, CellBehaviour cell) {
        if (cell != null && !cell.GetLight()) {

            cell.SignalFromDirection(fromWorldDirection);

            if (cell.IsOutgoing(DirectionEnum.NORTH)) Propagate(DirectionEnum.NORTH, GetCellAt(cell.transform.position + Vector3.forward));
            if (cell.IsOutgoing(DirectionEnum.EAST))  Propagate(DirectionEnum.EAST,  GetCellAt(cell.transform.position + Vector3.right));
            if (cell.IsOutgoing(DirectionEnum.SOUTH)) Propagate(DirectionEnum.SOUTH, GetCellAt(cell.transform.position - Vector3.forward));
            if (cell.IsOutgoing(DirectionEnum.WEST))  Propagate(DirectionEnum.WEST,  GetCellAt(cell.transform.position - Vector3.right));
        }
    }
}