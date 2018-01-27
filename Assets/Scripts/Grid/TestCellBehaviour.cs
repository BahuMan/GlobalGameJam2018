using UnityEngine;

public class TestCellBehaviour: MonoBehaviour
{
    public Vector3 acceptableDirection;

    [SerializeField]
    private CellBehaviour _cell;

    private void Start()
    {
        _cell = GetComponent<CellBehaviour>();
        _cell.IsSignalAccepted = CellBehaviour_IsSignalAccepted;
    }

    private bool CellBehaviour_IsSignalAccepted(Vector3 normalizedLocalDirection)
    {
        Debug.Log("Checking signal from " + normalizedLocalDirection);
        if (Vector3.Dot(normalizedLocalDirection, acceptableDirection) > .9f)
        {
            return true;
        }

        return false;
    }

}
