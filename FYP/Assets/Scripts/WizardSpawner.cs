using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardSpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfig> waveConfigs;
    [SerializeField] int startingWave = 0;
    [SerializeField] bool looping = false;


    // Start is called before the first frame update
    IEnumerator Start()
    {
        do
        {
            yield return StartCoroutine(SpawnAllWizards());

        }
        while (looping);
    }

    IEnumerator SpawnAllWizards()
    {
        for (int waveIndex = startingWave; waveIndex < waveConfigs.Count; waveIndex++)
        {
            var currentWave = waveConfigs[waveIndex];
            yield return StartCoroutine(SpawnWizardsInWave(currentWave));
        }

    }

    IEnumerator SpawnWizardsInWave(WaveConfig waveConfig)
    {
        for (int wizardCount = 0; wizardCount < waveConfig.GetNumberOfEnemies(); wizardCount++)
        {
            var newWizard = Instantiate(waveConfig.GetEnemyPrefab(), waveConfig.GetWayPoints()[0].transform.position, Quaternion.identity);
            newWizard.GetComponent<Wizard_Pathing>().SetWaveConfig(waveConfig);
            yield return new WaitForSeconds(waveConfig.GetTimeToSpawn());
        }
    }
}
