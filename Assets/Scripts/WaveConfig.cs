using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "WaveCofingSO", fileName = "WaveConfig")]
public class WaveConfig : ScriptableObject
{
    [SerializeField] private List<GameObject> enemies;
    [SerializeField] private Transform pathPrefab;
    [SerializeField] private float moveSpeed;

    public Transform GetStartingWayPoint()
    {
        return pathPrefab.GetChild(0);
    }

    public List<Transform> GetWaypoints()
    {
        List<Transform> waypoints = new List<Transform>();
        foreach (Transform waypoint in pathPrefab)
        {
            waypoints.Add(waypoint);
        }
        return waypoints;
    }

    public float GetSpeed()
    {
        return moveSpeed;
    }

    public int GetEnemiesCount()
    {
        return enemies.Count;
    }

    public List<GameObject> GetEnemies()
    {
        return enemies;
    }
}