using UnityEngine;

using Random = UnityEngine.Random;  // 다른 랜덤 함수가 자동으로 쳐지는 것을 방지하기 위해 별도로 지정했다!

public class EnemySpawner : MonoBehaviour
{
    [Header("적 프리팹")]
    public GameObject EnemyPrefab;
    public GameObject EnemyChasingPrefab;

    [Header("스포너 위치")]
    public Transform SpawnerPosition;

    [Header("쿨타임")]
    private float _timer = 0f;
    private float _cooltime;

    private void Start()
    {
        float randomNumber = UnityEngine.Random.Range(1f, 2f);
        _cooltime = randomNumber;
    }

    void Update()
    {
        _timer += Time.deltaTime;

        if (_timer <= _cooltime) return;

        float SpawnRate = Random.Range(0f, 1f);
        if (SpawnRate >= 0.7f)
        {
            GameObject _enemy = Instantiate(EnemyPrefab);
            _enemy.transform.position = SpawnerPosition.position;
            
        }
        else
        {
            GameObject _enemyChasing = Instantiate(EnemyChasingPrefab);
            _enemyChasing.transform.position = SpawnerPosition.position;
        }
        SpawnRate = 0f;

        _timer = 0f;

        float randomNumber = UnityEngine.Random.Range(1f, 3f);
        _cooltime = randomNumber;

        float randomX = UnityEngine.Random.Range(-1.8f, 1.8f);
        SpawnerPosition.position = new Vector3(randomX, SpawnerPosition.position.y, SpawnerPosition.position.z);
    }
}
