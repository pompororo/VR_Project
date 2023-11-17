using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region SpawnWave

    [SerializeField]private Transform spawnPoint0;
    [SerializeField]private Transform spawnPoint1;
    [SerializeField] private Transform enemyParent;
    [SerializeField] private GameObject enemyPrefab;
    public int sizeSpawn;

    

    private void RandomSpawnPoint(GameObject enemy)
    {
        int spawnPoint = Random.Range(0, 1);
        switch (spawnPoint)
        {
            case 0:
                Instantiate(enemy, GenerateSpawnInSpawnPoint(spawnPoint0, sizeSpawn), Quaternion.identity, enemyParent);
                break;
            case 1:
                Instantiate(enemy, GenerateSpawnInSpawnPoint(spawnPoint1, sizeSpawn), Quaternion.identity, enemyParent);
                break;
        }
    }
    
    private Vector3 GenerateSpawnInSpawnPoint(Transform spawnPoint,int size)
    {
        Vector3 result = new Vector3(
            Random.Range(-spawnPoint.position.x / 2, spawnPoint.position.x / 2),
            1,
            Random.Range(-spawnPoint.position.y / 2, spawnPoint.position.y / 2));

        return result;
    }

    #endregion
}
