using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    WaveConfig waveConfig;
    List<Transform> waypoints;
    int waypointIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        waypoints = waveConfig.GetWaypoints();
        transform.position = waypoints[waypointIndex].position;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void setWaveConfig(WaveConfig waveConfig)
    {
        this.waveConfig = waveConfig;
    }

    private void Move()
    {
        if (waypointIndex < waypoints.Count)
        {
            transform.position = Vector2.MoveTowards
                (transform.position, waypoints[waypointIndex].position, waveConfig.GetMoveSpeed() * Time.deltaTime);

            if (transform.position.Equals(waypoints[waypointIndex].position))
            {
                waypointIndex++;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
