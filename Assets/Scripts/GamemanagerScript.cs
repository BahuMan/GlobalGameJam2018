using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamemanagerScript : MonoBehaviour
{
    private CannonScript[] _cannons;
    private GridBehaviour _gridBeh;
    private int _winCounter;

    public float BeamTime = 4;
    [SerializeField]
    private RectTransform _transmittingText;

    [SerializeField]
    private RectTransform _choiceMenu;

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
            StartCoroutine(ExecuteWinScreen());
        }
    }

    private IEnumerator ExecuteWinScreen()
    {
        if (_transmittingText != null)
        {
            _transmittingText.gameObject.SetActive(true);
        }
        yield return new WaitForSeconds(BeamTime);

        if (_choiceMenu != null)
        {
            _choiceMenu.gameObject.SetActive(true);
        }
        yield return new WaitForSeconds(5f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    //check if all the cannons are powered with propegate event


}
