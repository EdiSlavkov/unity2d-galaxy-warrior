using System.Collections.Generic;
using UnityEngine;

public class PathFind : MonoBehaviour
{
    [SerializeField] private bool isBoss;
    private WaveConfig waveConfig;
    private GameController gameController;
    private List<Transform> waypoints;
    private int waypointIndex = 0;
    private bool shouldChangeDirection = false;
    private Vector3 waypointPosition;
    private float waveSpeed;

    private void Awake()
    {
        gameController = FindObjectOfType<GameController>();
    }

    private void Start()
    {
        waveConfig = gameController.GetCurrentWave();
        waypoints = waveConfig.GetWaypoints();
        transform.position = waypoints[0].position;
        waveSpeed = waveConfig.GetSpeed();
    }

    private void Update()
    {
        if (waypointIndex < waypoints.Count && !shouldChangeDirection)
        {
            waypointPosition = waypoints[waypointIndex].position;
            transform.position = Vector3.MoveTowards(transform.position, waypointPosition, waveSpeed * Time.deltaTime);
            if (transform.position == waypointPosition)
            {
                waypointIndex++;
            }
        }
        else if (!isBoss)
        {
            Destroy(gameObject);
        }
        else
        {
            shouldChangeDirection = true;
            waypointIndex--;
            transform.position = Vector3.MoveTowards(transform.position, waypointPosition, waveSpeed * Time.deltaTime);
            if (waypointIndex == 1)
            {
                shouldChangeDirection = false;
            }
        }
    }
}