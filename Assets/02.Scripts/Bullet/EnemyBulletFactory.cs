using UnityEngine;

public class EnemyBulletFactory : MonoBehaviour
{
    private static EnemyBulletFactory _instance = null;
    public static EnemyBulletFactory Instance => _instance;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        _instance = this;

        BulletPoolInit();
        BigBulletPoolInit();
    }


    [Header("총알 프리팹")]
    public GameObject EnemyBulletPrefab;
    public GameObject EnemyBigBulletPrefab;

    [Header("풀링")]
    public int PoolSize = 32;
    private GameObject[] _bulletObjectPool;


    private void BulletPoolInit()
    {
        _bulletObjectPool = new GameObject[PoolSize];

        for (int i = 0; i < PoolSize; i++)
        {
            GameObject bulletObject = Instantiate(EnemyBulletPrefab, transform);
            _bulletObjectPool[i] = bulletObject;
            bulletObject.SetActive(false);
        }
    }

    private void BigBulletPoolInit()
    {
        _bulletObjectPool = new GameObject[PoolSize];

        for (int i = 0; i < PoolSize; i++)
        {
            GameObject bigBulletObject = Instantiate(EnemyBigBulletPrefab, transform);
            _bulletObjectPool[i] = bigBulletObject;
            bigBulletObject.SetActive(false);
        }
    }

    public GameObject MakeBullet(Vector3 position)
    {
        for (int i = 0; i < PoolSize; i++)
        {
            GameObject bulletObject = _bulletObjectPool[i];

            if (bulletObject.activeInHierarchy == false)
            {
                bulletObject.transform.position = position;
                bulletObject.SetActive(true);

                return bulletObject;
            }
        }

        Debug.LogError("탄창에 총알 개수가 부족합니다.");
        return null;
    }

    public GameObject MakeEnemyBullet(Vector3 position)
    {
        return Instantiate(EnemyBulletPrefab, position, transform.rotation, transform);
    }
}
