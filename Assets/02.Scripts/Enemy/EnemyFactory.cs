using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    private static EnemyFactory _instance = null;
    public static EnemyFactory Instance => _instance;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        _instance = this;

        PoolInit();
        TracePoolInit();
    }

    [Header("적 프리팹")]
    public GameObject[] EnemyPrefab;

    [Header("풀링")]
    public int PoolSize = 30;
    private GameObject[] _enemyObjectPool;
    private GameObject[] _enemyTraceObjectPool;

    private void PoolInit()
    {
        _enemyObjectPool = new GameObject[PoolSize];

        for (int i = 0; i < PoolSize; i++)
        {
            GameObject enemyObject = Instantiate(EnemyPrefab[(int)EEnemytype.Directional], transform);
            _enemyObjectPool[i] = enemyObject;
            enemyObject.SetActive(false);
        }
    }

    private void TracePoolInit()
    {
        _enemyTraceObjectPool = new GameObject[PoolSize];

        for (int i = 0; i < PoolSize; i++)
        {
            GameObject enemyTraceObject = Instantiate(EnemyPrefab[(int)EEnemytype.Trace], transform);
            _enemyTraceObjectPool[i] = enemyTraceObject;
            enemyTraceObject.SetActive(false);
        }
    }


    public GameObject MakeEnemy(Vector3 position)
    {
        return Instantiate(EnemyPrefab[(int)EEnemytype.Directional], position, Quaternion.identity, transform);
    }

    public GameObject MakeEnemyTrace(Vector3 position)
    {
        return Instantiate(EnemyPrefab[(int)EEnemytype.Trace], position, Quaternion.identity, transform);
    }

    public GameObject MakeEnemyBoss(Vector3 position)
    {
        return Instantiate(EnemyPrefab[(int)EEnemytype.Boss], position, Quaternion.identity, transform);
    }
}
