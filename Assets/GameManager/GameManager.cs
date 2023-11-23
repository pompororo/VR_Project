using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public static AffectFieldSkill currentFieldState = AffectFieldSkill.Default;
    private static float slowTimeDownFactor = 0.1f;
    
    public static bool hasUpdated = false;
    
    // Start is called before the first frame update
    void Awoke()
    {
        if (instance != null && instance != this)
        {
            // If an instance already exists and it's not this one, destroy this GameManager
            Destroy(gameObject);
        }
        else
        {
            // Set the singleton instance to this instance if it's the first one
            instance = this;
            DontDestroyOnLoad(gameObject); // Ensure it persists through scene changes
        }
    }
    
    private void Start()
    {
        SpawnGroupOfEnemy(enemyToSpawnCount);
    }
    void Update()
    {
        if (!hasUpdated)
        {
            switch (currentFieldState)
            {
                case AffectFieldSkill.SlowTime:
                    ActiveSlowTime();
                    break;

                case AffectFieldSkill.Default:
                    DefaultFieldState();
                    break;
            }

            hasUpdated = true;
        }
        
        
        //WAVE SPAWM
        elapsedTime += Time.deltaTime;
        
        if (elapsedTime >= waveDuration || enemyParent.childCount == 0)
        {
            elapsedTime = 0f;
            SpawnGroupOfEnemy(enemyToSpawnCount);
        }
    }

    #region AffectFieldSkill
    
     public static void ActiveSlowTime()
     {
         currentFieldState = AffectFieldSkill.SlowTime;
         Time.timeScale = slowTimeDownFactor;
         Time.fixedDeltaTime = Time.timeScale * 0.2f;
     }

     public static void DefaultFieldState()
     {
         currentFieldState = AffectFieldSkill.Default;
         Time.timeScale = 1;
     }
    #endregion

    #region SpawnWave

    [Header("SpawnPoint&Enemy")]
    [SerializeField]private Transform spawnPoint0;
    [SerializeField]private Transform spawnPoint1;
    [SerializeField]private Transform spawnPoint2;
    [SerializeField] private Transform enemyParent;
    
    public int sizeSpawn;

    [SerializeField] private GameObject enemyPrefab;
    public float percentOfEnemy;
    [SerializeField] private GameObject enemyPrefab2;
    public float percentOfEnemy2;
    
    
    
    [Header("WaveManage")]
    public int enemyToSpawnCount;
    public int MoreEnemyEveryRound;
    
    private int waveRound = 1;
    
    private float elapsedTime = 0f;
    public float waveDuration = 60f;

    public void SpawnGroupOfEnemy(int Count)
    {
        int result;
        if (waveRound == 1)
        {
            result = Count;
        }
        else
        {
            result = Count + MoreEnemyEveryRound;
            MoreEnemyEveryRound += MoreEnemyEveryRound;
        }
        int aEnemyCount = (int)(result * percentOfEnemy);
        int bEnemyCount = result - aEnemyCount;
        
        //RandomSpawn
        for (int i = 0; i < result; i++)
        {
            if (i < aEnemyCount)
            {
                // Spawn A enemy until the count for A enemies is reached
                RandomSpawnPoint(enemyPrefab);
            }
            else
            {
                // Spawn B enemy for the remaining count
                RandomSpawnPoint(enemyPrefab2);
            }
        }
        
        waveRound++;
    }

    private void RandomSpawnPoint(GameObject enemy)
    {
        int spawnPoint = Random.Range(0, 3);

        switch (spawnPoint)
        {
            case 0:
                Instantiate(enemy, GenerateSpawnInSpawnPoint(spawnPoint0, sizeSpawn), Quaternion.identity, enemyParent);
                break;
            case 1:
                Instantiate(enemy, GenerateSpawnInSpawnPoint(spawnPoint1, sizeSpawn), Quaternion.identity, enemyParent);
                break;
            case 2:
                Instantiate(enemy, GenerateSpawnInSpawnPoint(spawnPoint2, sizeSpawn), Quaternion.identity, enemyParent);
                break;
        }
    }
    
    private Vector3 GenerateSpawnInSpawnPoint(Transform spawnPoint,int size)
    {
        Vector3 result = new Vector3(
            Random.Range(-spawnPoint.position.x-size / 2, spawnPoint.position.x+size / 2),
            1,
            Random.Range(-spawnPoint.position.z-size / 2, spawnPoint.position.z+size / 2));

        return result;
    }

    #endregion
}

public enum AffectFieldSkill
{
    Default,
    SlowTime,
    Pause,
    ForceLighting
}
