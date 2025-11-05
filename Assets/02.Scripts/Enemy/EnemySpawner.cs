using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("적 프리팹")]
    public GameObject EnemyPrefab;
    public Transform EnemySpawnerPosition;

    [Header("쿨타임")]
    private float _Timer = 0f;
    private float _Cooltime = 1f;

    void Update()
    {
        _Timer += Time.deltaTime;

        if (_Timer <= _Cooltime) return;

        GameObject enemy = Instantiate(EnemyPrefab);

        enemy.transform.position = EnemySpawnerPosition.position;

        _Timer = 0f;

    }
}
