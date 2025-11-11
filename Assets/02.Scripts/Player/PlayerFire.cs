using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    // 필요 속성
    [Header("총알 프리팹")]
    public GameObject BulletPrefab;
    public GameObject MiniBullet1Prefab;
    public GameObject MiniBullet2Prefab;

    [Header("총구")]
    public Transform FirePosition;
    public Vector3 TwoBullet = new Vector2(0.5f, 0);  // FireOffset
    public Vector3 MiniBulletOffset = new Vector2(-1f, 0);  // 추후 수정에는 총구 따로 만드는 게 좋다.

    [Header("쿨타임")]
    private float _fireTimer = 0f;
    private float Cooltime = 0.6f;   // public const float Cooltime = 0.5f;

    [Header("자동 / 수동 공격")]
    private KeyCode AutoAtteck = KeyCode.Keypad1;
    private KeyCode AutoAtteck2 = KeyCode.Alpha1;
    private KeyCode NotAutoAtteck = KeyCode.Keypad2;
    private KeyCode NotAutoAtteck2 = KeyCode.Alpha2;
    private bool isAutoAtteck = true;

    [Header("필살기")]
    private KeyCode SpecialAtteck = KeyCode.Keypad3;
    private KeyCode SpecialAtteck2 = KeyCode.Alpha3;


    private void Update()
    {
        _fireTimer += Time.deltaTime;


        switch (Input.GetKeyDown(AutoAtteck) || Input.GetKeyDown(AutoAtteck2))
        {
            case true:
                isAutoAtteck = true;
                break;
            case false when Input.GetKeyDown(NotAutoAtteck) || Input.GetKeyDown(NotAutoAtteck2):
                isAutoAtteck = false;
                break;
            case false:
                break;
        }

        if (isAutoAtteck == true)
        {
            AutoFireOn();
        }

        if (Input.GetKeyDown(SpecialAtteck) || Input.GetKeyDown(SpecialAtteck2))
        {
            SpecialBoom();
        }

        // 1. 발사 버튼을 누르고 있으면
        if (Input.GetKey(KeyCode.Space) && _fireTimer > Cooltime)
        {
            MakeBullets();
            MakeSubBullets();

            // 4. 쿨타임 초기화
            _fireTimer = 0f;
        }
    }
    public void AutoFireOn()
    {
        if (_fireTimer > Cooltime)
        {
            MakeBullets();
            MakeSubBullets();

            _fireTimer = 0f;
        }
    }

    private void MakeBullets()
    {
        // 2. 프리팹으로부터 총알(게임 오브젝트)을 생성한다.
        // 유니티에서 게임 오브젝트를 생성할 때는 new가 Instantiate 라는 메서드를 이용한다.
        // 클래스 -> 객체(속성+기능) -> 메모리에 로드된 객체를 인스턴스 => 인스턴스화
        GameObject bullet1 = Instantiate(BulletPrefab);
        GameObject bullet2 = Instantiate(BulletPrefab);


        // 3. 총알 생성 후 위치를 플레이어 위치로 수정한다.
        bullet1.transform.position = FirePosition.position + TwoBullet;
        bullet2.transform.position = FirePosition.position - TwoBullet;
    }

    private void MakeSubBullets()
    {
        GameObject Minibullet1 = Instantiate(MiniBullet1Prefab);
        GameObject Minibullet2 = Instantiate(MiniBullet2Prefab);

        Minibullet1.transform.position = transform.position + MiniBulletOffset;
        Minibullet2.transform.position = transform.position - MiniBulletOffset;
    }

    public void AttackSpeedUp(float value)
    {
        Cooltime -= value;
    }

    private void SpecialBoom()
    {

    }
}
