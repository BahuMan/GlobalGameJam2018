using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour {

    private GameObject tile;

    private void Update()
    {
        InputHandler();
    }

    private void InputHandler()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            tile = RayCastCheck();
            if (tile != null)
            {
                // Tile needs rotate function + 90
                transform.parent = tile.transform;
                Debug.Log("Lel");
            }
        }

        if (Input.GetButtonDown("Fire2"))
        {
            tile = RayCastCheck();
            if (tile != null)
            {
                transform.parent = tile.transform;
                // rotate -90
            }
        }
    }


    private GameObject RayCastCheck()
    {
        GameObject returnValue;
        Ray ray = new Ray(this.transform.position, transform.up * -1);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 5))
        {
            returnValue = hit.collider.gameObject;
        }

        else returnValue = null;

        return returnValue;
    } 
}
