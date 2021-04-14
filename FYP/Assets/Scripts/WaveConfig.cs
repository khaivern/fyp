using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Wave Config")]
public class WaveConfig : ScriptableObject
{
    [SerializeField] GameObject wizardPrefab;
    [SerializeField] GameObject pathPrefab;
    [SerializeField] float timeToSpawn = 0.5f;
    [SerializeField] float spawnRandomFactor = 0.3f;
    [SerializeField] int numberOfEnemies = 5;
    [SerializeField] float moveSpeed = 2f;

    public GameObject GetEnemyPrefab() { return wizardPrefab;}
    public List<Transform> GetWayPoints() 
    {
        var wayPoints = new List<Transform>();
        foreach(Transform child in pathPrefab.transform)
        {
            wayPoints.Add(child);
        }
        return wayPoints;
    }

    public float GetTimeToSpawn() { return timeToSpawn; }
    public float GetSpawnRandomFactor() { return spawnRandomFactor; }
    public int GetNumberOfEnemies() { return numberOfEnemies; }
    public float GetMoveSpeed() { return moveSpeed; }


}
