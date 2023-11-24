using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public static AffectFieldSkill currentFieldState = AffectFieldSkill.Default;
    private static float slowTimeDownFactor = 0.1f;
    
    public static bool hasUpdated = false;

    public bool stopSpawning = false;
    public bool clearEnemy = false;
    
    public float maxStamina = 100f;
    public float currentStamina;
    public float staminaRegenerationRate = 5f;
    
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
        currentStamina = maxStamina;
    }
    void Update()
    {
        RegenerateStamina();
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
                case AffectFieldSkill.Pause :
                    PauseFieldState();
                    break;
            }

            hasUpdated = true;
        }
        else if (waveRound == waveToWin)
        {
            Victory();
        }
        
        //WAVE SPAWM
        if (!stopSpawning && waveRound != waveToWin)
        {
            elapsedTime += Time.deltaTime;
        
            if (elapsedTime >= waveDuration || enemyParent.childCount == 0)
            {
                elapsedTime = 0f;
                SpawnGroupOfEnemy(enemyToSpawnCount);
                waveRound++;
            }
        }

        if (clearEnemy)
        {
            foreach (Transform child in enemyParent)
            {
                Destroy(child.gameObject);
            }
            clearEnemy = false;
        }
    }

    private void RegenerateStamina()
    {
        if (currentStamina < maxStamina)
        {
            // Increase stamina over time at a specified regeneration rate.
            currentStamina += staminaRegenerationRate * Time.deltaTime;

            // Ensure stamina doesn't exceed the maximum value.
            currentStamina = Mathf.Clamp(currentStamina, 0f, maxStamina);
            
        }
    }
    
    public void UseStamina(float amount)
    {
        if (currentStamina >= amount)
        {
            // Perform the action that requires stamina.

            // Decrease stamina based on the amount used.
            currentStamina -= amount;
        }
        else
        {
            // Not enough stamina to perform the action. Handle this situation accordingly.
            Debug.Log("Not enough stamina!");
        }
    }

    private void PauseFieldState()
    {
        Time.timeScale = 0;
        Time.fixedDeltaTime = Time.timeScale * 0.2f;
    }

    #region AffectFieldSkill
    
     public static void ActiveSlowTime()
     {
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
    public int waveToWin = 5;
    
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
            Random.Range(spawnPoint.position.x-size, spawnPoint.position.x+size),
            1,
            Random.Range(spawnPoint.position.z-size, spawnPoint.position.z+size));

        return result;
    }

    #endregion

    public TextMeshProUGUI victory;
    private void Victory()
    {
        currentFieldState = AffectFieldSkill.Pause;
        stopSpawning = true;
        clearEnemy = true;
        victory.gameObject.SetActive(true);
    }
}




public enum AffectFieldSkill
{
    Default,
    SlowTime,
    Pause,
    ForceLighting
}
