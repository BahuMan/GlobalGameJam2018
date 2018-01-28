using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamemanagerScript : MonoBehaviour
{
    private CannonScript[] _cannons;
	// Use this for initialization
	void Start ()
    {
        _cannons = FindObjectsOfType<CannonScript>();
	}
	
    //check if all the cannons are powered with propegate event

}
