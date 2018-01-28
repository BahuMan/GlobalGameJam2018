using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStartScript : MonoBehaviour {

    public GameObject SplashScreen;
    public float SplashTime = 1;

	// Use this for initialization
	void Start () {
        StartCoroutine("StartLevel");
	}

    private IEnumerator StartLevel()
    {
        yield return new WaitForSeconds(SplashTime);

        SplashScreen.SetActive(false);
        GetComponent<TopDownController>().enabled = true;
        GetComponent<ButtonScript>().enabled = true;


    }
}
