using UnityEngine;

public class ReceiverBehaviour: MonoBehaviour
{
    [SerializeField]
    private float _timeBetweenScore = 1;
    private float _nextScoreTime = 0;
    
    private void Start()
    {
        CellBehaviour cell = GetComponent<CellBehaviour>();
        cell.OnLightSwitched += Cell_LightSwitched;
        this.enabled = false;
    }

    private void Cell_LightSwitched(bool light)
    {
        this.enabled = true;
        Debug.Log("Goal is lit up");
    }

}

