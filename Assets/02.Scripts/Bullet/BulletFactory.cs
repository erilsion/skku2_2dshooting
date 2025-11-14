using UnityEngine;

public class BulletFactory : MonoBehaviour
{
    private static BulletFactory _instance = null;
    public static BulletFactory Instance => _instance;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        _instance = this;

        PoolInit();
        Mini1PoolInit();
        Mini2PoolInit();
    }


    [Header("총알 프리팹")]
    public GameObject BulletPrefab;
    public GameObject MiniBullet1Prefab;
    public GameObject MiniBullet2Prefab;
    public GameObject SpecialBombPrefab;


    [Header("풀링")]
    public int PoolSize = 64;
    private GameObject[] _bulletObjectPool;  // 게임 총알을 담아둘 풀: 탄창
    private GameObject[] _miniBullet1ObjectPool;
    private GameObject[] _miniBullet2ObjectPool;

    // 풀(탄창) 초기화
    private void PoolInit()
    {
        // Awake vs Start vs Lazy  Awake는 게임 시작하기 전, Awake가 끝나면 게임 프레임 스타트하며 Start, Lazy는 누군가 호출하면
        // 1. 탄창에 총알을 담을 수 있는 크기 배열로 만들어준다.
        _bulletObjectPool = new GameObject[PoolSize];

        // 2. 탄창 사이즈 크기만큼 반복해서
        for (int i = 0; i < PoolSize; i++)
        {
            // 3. 총알을 생성해서 담는다.
            GameObject bulletObject = Instantiate(BulletPrefab, transform);
            // 4. 생성한 총알을 탄창에 담는다.
            _bulletObjectPool[i] = bulletObject;

            // 5. 비활성화한다.
            bulletObject.SetActive(false);
        }
    }

    private void Mini1PoolInit()
    {
        _miniBullet1ObjectPool = new GameObject[PoolSize];

        for (int i = 0; i < PoolSize; i++)
        {
            GameObject bulletObject = Instantiate(MiniBullet1Prefab, transform);
            _miniBullet1ObjectPool[i] = bulletObject;
            bulletObject.SetActive(false);
        }
    }

    private void Mini2PoolInit()
    {
        _miniBullet2ObjectPool = new GameObject[PoolSize];

        for (int i = 0; i < PoolSize; i++)
        {
            GameObject bulletObject = Instantiate(MiniBullet2Prefab, transform);
            _miniBullet2ObjectPool[i] = bulletObject;
            bulletObject.SetActive(false);
        }
    }


    public GameObject MakeBullet(Vector3 position)
    {
        // 필요하다면 여기서 생성 이펙트도 넣고
        // 인자값으로 대미지도 받아서 넘겨줄 수 있다

        // 1. 탄창 안에 있는 총알들 중에서
        for (int i = 0; i < PoolSize; i++)
        {
            GameObject bulletObject = _bulletObjectPool[i];
            // 2. 비활성화된 총알 하나를 찾아
            if (bulletObject.activeInHierarchy == false)
            {
                // 3. 위치를 수정하고 활성화시킨다.
                bulletObject.transform.position = position;
                bulletObject.SetActive (true);

                return bulletObject;
            }
        }

        Debug.LogError("탄창에 총알 개수가 부족합니다. [ㅇㅇㅇ를 찾아주세요.]");
        return null;
    }

    public GameObject MakeMiniBullet1(Vector3 position)
    {
        return Instantiate(MiniBullet1Prefab, position, Quaternion.identity, transform);
    }
    public GameObject MakeMiniBullet2(Vector3 position)
    {
        return Instantiate(MiniBullet2Prefab, position, Quaternion.identity, transform);
    }
    public GameObject MakeSpecialBomb(Vector3 position)
    {
        return Instantiate(SpecialBombPrefab, position, Quaternion.identity, transform);
    }
}
