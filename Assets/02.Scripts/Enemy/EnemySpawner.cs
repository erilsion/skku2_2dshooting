using UnityEngine;

using Random = UnityEngine.Random;  // 다른 랜덤 함수가 자동으로 쳐지는 것을 방지하기 위해 별도로 지정했다!

public class EnemySpawner : MonoBehaviour
{
    [Header("스포너 위치")]
    public Transform SpawnerPosition;
    public float SpawnRangeX = 1.8f;

    [Header("쿨타임")]
    private float _timer = 0f;
    private float _cooltime;
    private float _minTime = 1f;
    private float _maxTime = 3f;

    [Header("확률")]
    private float _maxRate = 1f;
    private float _minRate = 0f;
    private float _enemyRate = 0.7f;


    private void Start()
    {
        float randomNumber = UnityEngine.Random.Range(_minTime, _maxTime);
        _cooltime = randomNumber;
    }

    void Update()
    {
        _timer += Time.deltaTime;

        if (_timer <= _cooltime) return;

        SpawnRate();
        Cooltime();
        RandomSpawnPosition();
    }

    private void SpawnRate()
    {
        float SpawnNumber = Random.Range(_minRate, _maxRate);
        if (SpawnNumber > _enemyRate)
        {
            EnemyFactory.Instance.MakeEnemy(SpawnerPosition.position);
        }

        else
        {
            EnemyFactory.Instance.MakeEnemyTrace(SpawnerPosition.position);
        }
        SpawnNumber = 0f;
    }

    private void RandomSpawnPosition()
    {
        Vector2 randomX = Random.insideUnitCircle * SpawnRangeX;
        SpawnerPosition.position = new Vector3(randomX.x, SpawnerPosition.position.y, SpawnerPosition.position.z);
    }

    private void Cooltime()
    {
        float randomNumber = UnityEngine.Random.Range(_minTime, _maxTime);
        _cooltime = randomNumber;
        _timer = 0f;
    }
}
