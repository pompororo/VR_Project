using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Update is called once per frame
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

    [SerializeField]private Transform spawnPoint0;
    [SerializeField]private Transform spawnPoint1;
    [SerializeField] private Transform enemyParent;
    [SerializeField] private GameObject enemyPrefab;
    public int sizeSpawn;
    public int enemyToSpawnCount;
    
    private float _elapsedTime = 0f;
    public float waveDuration = 60f;

    private void SpawnGroupOfEnemy(int Count)
    {
        for(int i = 0; i < Count;i++)
        {
            RandomSpawnPoint(enemyPrefab);
        }
    }

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

public enum AffectFieldSkill
{
    Default,
    SlowTime,
    Pause,
    ForceLighting
}
