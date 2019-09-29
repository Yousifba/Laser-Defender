using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfig> waveConfigs;
    [SerializeField] bool wavesLoop = false;

    int waveIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnAllWaves());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnAllWaves()
    {
        do
        {
            foreach (WaveConfig waveConfig in waveConfigs)
            {
                yield return StartCoroutine(SpawnWave(waveConfig));
            }
        }
        while (wavesLoop);
    }

    IEnumerator SpawnWave(WaveConfig waveConfig)
    {
        int waveEnemies = waveConfig.GetNumberOfEnemies();
        for(int enemyCount = 0; enemyCount < waveEnemies; enemyCount++)
        {
            Instantiate(
                waveConfig.GetEnemyPrefab(),
                waveConfig.GetWaypoints()[0].position,
                Quaternion.identity).GetComponent<EnemyPathing>().setWaveConfig(waveConfig);
            yield return new WaitForSeconds(waveConfig.GetSpawnFreq());
        }
    }
}
