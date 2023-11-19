using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public static AffectFieldSkill currentFieldState;
    public static float slowTimeDownFactor = 0.5f;
    
    // Start is called before the first frame update
    void Start()
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
        #region ลบตอนใส่จริงมันต้องอยู่ที่ player

        if ((OVRInput.GetDown(OVRInput.Button.One) || Input.GetKey(KeyCode.Tab)) && GameManager.currentFieldState != AffectFieldSkill.SlowTime)
        {
            GameManager.ActiveSlowTime();
        }
        else
        {
            GameManager.DefaultFieldState();
        }

        #endregion
        
        switch (currentFieldState)
        {
            case AffectFieldSkill.SlowTime:
                break;
        }
    }

    #region AffectFieldSkill
    
     public static void ActiveSlowTime()
     {
         currentFieldState = AffectFieldSkill.SlowTime;
         Time.timeScale = slowTimeDownFactor;
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
