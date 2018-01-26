using UnityEngine;

public class GridBehaviour : MonoBehaviour {

    public int width = 5;
    public int length = 5;
    public CellBehaviour cellPrefab;

    [SerializeField]
    private CellBehaviour[] _cells;

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
                _cells[x * width + z] = cell;
            }
        }
    }

    public CellBehaviour GetCellAt(Vector3 pos)
    {
        return GetCellAt((int) pos.x, (int) pos.z);
    }

    public CellBehaviour GetCellAt(int x, int z)
    {
        return _cells[x * width + z];
    }
}
