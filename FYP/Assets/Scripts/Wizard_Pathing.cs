using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard_Pathing : MonoBehaviour
{
    [SerializeField] WaveConfig waveConfig;
    List<Transform> wayPoints;
    int wayPointIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        wayPoints = waveConfig.GetWayPoints();
        transform.position = wayPoints[wayPointIndex].transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        MovePath();
    }

    private void MovePath()
    {
        if (wayPointIndex <= wayPoints.Count - 1)
        {
            var targetPosition = wayPoints[wayPointIndex].transform.position;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, waveConfig.GetMoveSpeed() * Time.deltaTime);

            if (transform.position == targetPosition)
            {
                wayPointIndex++;
            }

        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetWaveConfig(WaveConfig waveConfig)
    {
        this.waveConfig = waveConfig;
    }
}
