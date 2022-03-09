using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShipSpawner : MonoBehaviour
{
    public List<EnemyShip> enemyShipPrefabs;
    public Transform spawnPoint;
    public Transform spawnPivot;

    [HideInInspector] public int waveCount;
    [HideInInspector] public int startingNumberOfShips;

    private void Awake()
    {
        waveCount = 1;
        HUD.Instance.DisplayWave(waveCount);
        //Brian's fix to incorrect wave count

        startingNumberOfShips = FindObjectsOfType<EnemyShip>().Length;

    }
    public void SpawnEnemyShips()
    {
        int enemyShipsToSpawn = startingNumberOfShips + waveCount;

        for(int i = 0; i < enemyShipsToSpawn; ++i)
        {
            int rand = Random.Range(0, enemyShipPrefabs.Count);
            float zRotation = Random.Range(0, 360);

            spawnPivot.eulerAngles = new Vector3(0, 0, zRotation);
            Instantiate(enemyShipPrefabs[rand], spawnPoint.position, transform.rotation, null);
        }
    }

    public void CountEnemyShips()
    {
        int numberOfEnemyShips = FindObjectsOfType<EnemyShip>().Length;

        print(numberOfEnemyShips);

        if(numberOfEnemyShips == 1)
        {
            waveCount++;
            HUD.Instance.DisplayWave(waveCount);
            SpawnEnemyShips();
        }
    }
}
