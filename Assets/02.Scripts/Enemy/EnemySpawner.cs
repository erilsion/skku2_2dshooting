using UnityEngine;

using Random = UnityEngine.Random;  // 다른 랜덤 함수가 자동으로 쳐지는 것을 방지하기 위해 별도로 지정했다!

public class EnemySpawner : MonoBehaviour
{
    [Header("적 프리팹")]
    public GameObject EnemyPrefab;
    public Transform SpawnerPosition;

    [Header("쿨타임")]
    private float _Timer = 0f;
    private float _Cooltime;

    private void Start()
    {
        // 쿨타임을 1초와 2초 사이로 랜덤하게 지정한다.
        float randomNumber = UnityEngine.Random.Range(1f, 2f);
        _Cooltime = randomNumber;
    }

    void Update()
    {
        _Timer += Time.deltaTime;

        if (_Timer <= _Cooltime) return;

        GameObject enemy = Instantiate(EnemyPrefab);
        enemy.transform.position = SpawnerPosition.position;

        // Instantiate(EnemyPrefab, EnemySpawnerPosition.position); <= 한 줄로 요약

        // Transform이라는 컴포지션은 무조건 존재하기 때문에 GetComponent<Transform>()을 쓸 필요가 없다.

        _Timer = 0f;

        float randomNumber = UnityEngine.Random.Range(1f, 3f);
        _Cooltime = randomNumber;

        float randomX = UnityEngine.Random.Range(-1.8f, 1.8f);
        SpawnerPosition.position = new Vector3(randomX, SpawnerPosition.position.y, SpawnerPosition.position.z);
    }
}
