using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create Wave Config")]
public class WaveConfig : ScriptableObject
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject pathPrefab;
    [SerializeField] float spawnFreq = 0.5f;
    [SerializeField] float moveSpeed = 0.2f;
    [SerializeField] float spawnRandomFactor = 0.3f;
    [SerializeField] int numberOfEnemies = 5;

    public GameObject GetEnemyPrefab() => enemyPrefab;

    public GameObject GetPathPrefab() => pathPrefab;

    public List<Transform> GetWaypoints()
    {
        var waveWaypoints = new List<Transform>();
        foreach (Transform child in pathPrefab.transform)
        {
            waveWaypoints.Add(child);
        }
        return waveWaypoints;
    }

    public float GetSpawnFreq() => spawnFreq;

    public float GetMoveSpeed() => moveSpeed;

    public float GetSpawnRandomFactor() => spawnRandomFactor;

    public int GetNumberOfEnemies() => numberOfEnemies;
}
