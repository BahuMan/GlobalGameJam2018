using UnityEngine;

public class ButtonScript : MonoBehaviour {

    private GridBehaviour _grid;

    private void Start()
    {
        _grid = GameObject.FindObjectOfType<GridBehaviour>();
    }

    private void Update()
    {
        InputHandler();
    }

    private void InputHandler()
    {
        CellBehaviour tile;

        if (Input.GetButtonDown("Fire1"))
        {
            tile = RayCastCheck();
            if (tile != null)
            {
                transform.parent = tile.transform;
                tile.StartRotate(90, gameObject);
                tile.RotateClockwise();
                _grid.Propagate();
            }
        }

        if (Input.GetButtonDown("Fire2"))
        {
            tile = RayCastCheck();
            if (tile != null)
            {
                transform.parent = tile.transform;
                tile.RotateCounterClockwise();
                tile.StartRotate(-90, gameObject);
                _grid.Propagate();
            }
        }
    }


    private CellBehaviour RayCastCheck()
    {
        CellBehaviour returnValue;
        Ray ray = new Ray(this.transform.position, transform.up * -1);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 5))
        {
            returnValue = hit.collider.gameObject.GetComponentInParent<CellBehaviour>();
        }

        else returnValue = null;

        return returnValue;
    } 
}
