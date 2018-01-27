using UnityEngine;

public class GridBehaviour : MonoBehaviour
{

    public int width = 5;
    public int length = 5;
    public CellBehaviour cellPrefab;

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
                CellBehaviour cell = Instantiate<CellBehaviour>(cellPrefab, pos, Quaternion.identity, this.transform);
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
            Propagate(GetCellAt(generators.transform.position + Vector3.forward));
            //EAST
            Propagate(GetCellAt(generators.transform.position + Vector3.right));
            //SOUTH
            Propagate(GetCellAt(generators.transform.position - Vector3.forward));
            //WEST
            Propagate(GetCellAt(generators.transform.position - Vector3.right));

        }
    }

    public void Propagate(CellBehaviour cell) {
        if (cell != null && !cell.GetLight()) {

            cell.GoLight();

            if (cell.IsOutgoing(DirectionEnum.NORTH)) Propagate(GetCellAt(cell.transform.position + Vector3.forward));
            if (cell.IsOutgoing(DirectionEnum.EAST)) Propagate(GetCellAt(cell.transform.position + Vector3.right));
            if (cell.IsOutgoing(DirectionEnum.SOUTH)) Propagate(GetCellAt(cell.transform.position - Vector3.forward));
            if (cell.IsOutgoing(DirectionEnum.WEST)) Propagate(GetCellAt(cell.transform.position - Vector3.right));
        }
    }
}