using UnityEngine;

public class BoomerangEnemySpawner : MonoBehaviour
{
    [Header("적 프리팹")]
    public GameObject[] EnemyPrefab;

    [Header("스포너 위치")]
    public Transform SpawnerPosition;
    public float SpawnRangeX = 3f;

    [Header("쿨타임")]
    private float _timer = 0f;
    private float _cooltime;
    private float _minTime = 1f;
    private float _maxTime = 3f;

    [Header("확률")]
    private float _maxRate = 1f;
    private float _minRate = 0f;

    void Start()
    {
        float RandomNumber = UnityEngine.Random.Range(_minTime, _maxTime);
        _cooltime = RandomNumber;
    }


    void Update()
    {
        _timer += Time.deltaTime;

        if (_timer <= _cooltime) return;

        RandomSpawnPosition();

        GameObject _enemy = Instantiate(EnemyPrefab[(int)EEnemytype.Boomerang]);
        _enemy.transform.position = SpawnerPosition.position;

        Cooltime();
    }

    private void RandomSpawnPosition()
    {
        float _spawnPosition = Random.Range(_minRate, _maxRate);
        if (_spawnPosition < 0.5f)
        {
            SpawnerPosition.position = new Vector3(-SpawnRangeX, SpawnerPosition.position.y, SpawnerPosition.position.z);
        }
        else
        {
            SpawnerPosition.position = new Vector3(SpawnRangeX, SpawnerPosition.position.y, SpawnerPosition.position.z);
        }
    }

    private void Cooltime()
    {
        float RandomNumber = UnityEngine.Random.Range(_minTime, _maxTime);
        _cooltime = RandomNumber;
        _timer = 0f;
    }
}
