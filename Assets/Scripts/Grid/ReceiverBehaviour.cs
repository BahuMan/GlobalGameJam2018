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

    private void Update()
    {
        if (Time.time < _nextScoreTime) return;

        Debug.Log("Score ++");
        _nextScoreTime = Time.time + _timeBetweenScore;
        ScoreModel.AddScore(1);
    }
}

