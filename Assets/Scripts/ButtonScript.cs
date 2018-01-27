using System.Collections;
using System.Collections.Generic;
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
                // Tile needs rotate function + 90
                tile.RotateClockwise();
                transform.parent = tile.transform;
                _grid.Propagate();
                Debug.Log("Lel");
            }
        }

        if (Input.GetButtonDown("Fire2"))
        {
            tile = RayCastCheck();
            if (tile != null)
            {
                tile.RotateCounterClockwise();
                transform.parent = tile.transform;
                // rotate -90
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
