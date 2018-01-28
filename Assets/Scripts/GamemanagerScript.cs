using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamemanagerScript : MonoBehaviour
{
    private CannonScript[] _cannons;
    private GridBehaviour _gridBeh;
    private int _winCounter;
	// Use this for initialization
	void Start ()
    {
        _cannons = FindObjectsOfType<CannonScript>();
        _gridBeh = FindObjectOfType<GridBehaviour>();
        _gridBeh.OnSignalPropagated += Grid_Propagated;
	}

    private void Grid_Propagated()
    {
        _winCounter = 0;
        for(int i = 0; i < _cannons.Length; i++)
        {
            if (_cannons[i].Powered)
            {
                _winCounter++;
            } 
        }

        if (_winCounter == _cannons.Length)
        {
            ExecuteWinScreen();
        }
    }

    private void ExecuteWinScreen()
    {
        throw new NotImplementedException();
    }

    //check if all the cannons are powered with propegate event


}
