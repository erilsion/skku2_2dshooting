using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    [Header("총구")]
    public Transform FirePosition;
    public Transform SpecialAttackPosition;
    public float TwoBullet = 0.5f;  // FireOffset
    public float MiniBulletOffset = -1f;  // 추후 수정에는 총구 따로 만드는 게 좋다.


    [Header("쿨타임")]
    private float _fireTimer = 0f;
    private float Cooltime = 0.6f;   // public const float Cooltime = 0.5f;

    [Header("자동 / 수동 공격")]
    private KeyCode AutoAtteck = KeyCode.Keypad1;
    private KeyCode AutoAtteck2 = KeyCode.Alpha1;
    private KeyCode NotAutoAtteck = KeyCode.Keypad2;
    private KeyCode NotAutoAtteck2 = KeyCode.Alpha2;
    private bool isAutoAtteck = true;

    [Header("필살기 사용")]
    private KeyCode SpecialAtteck = KeyCode.Keypad3;
    private KeyCode SpecialAtteck2 = KeyCode.Alpha3;

    [Header("사운드")]
    public AudioSource FireSound;
    public AudioSource SpecialAttackSound;


    private void Update()
    {
        _fireTimer += Time.deltaTime;

        AutoSwitch();

        Fire();

        if (Input.GetKeyDown(SpecialAtteck) || Input.GetKeyDown(SpecialAtteck2))
        {
            SpecialAttackOn();
        }
    }

    public void AutoSwitch()
    {
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
    }

    public void Fire()
    {

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

    // 총알 (프리팹) - 생성 로직이 바뀔 때마다 아래 모든 코드가 수정되어야한다.
    // ㄴ 총알 생성이라는 행위 자체를 담당하는 클래스를 만들면 편하지 않을까? => 팩토리
    // ㄴ 총알 생성기 (타입, 대미지, 위치, 생성이펙트); => 불렛팩토리

    private void MakeBullets()
    {
        BulletFactory.Instance.MakeBullet(FirePosition.position + new Vector3(+TwoBullet, 0, 0));
        BulletFactory.Instance.MakeBullet(FirePosition.position + new Vector3(-TwoBullet, 0, 0));
        FireSound.Play();
    }

    private void MakeSubBullets()
    {
        BulletFactory.Instance.MakeMiniBullet1(FirePosition.position + new Vector3(+MiniBulletOffset, 0, 0));
        BulletFactory.Instance.MakeMiniBullet2(FirePosition.position + new Vector3(-MiniBulletOffset, 0, 0));
    }

    public void AttackSpeedUp(float value)
    {
        Cooltime -= value;
    }

    public void SpecialAttackOn()
    {
        SpecialAttackSound.Play();
        BulletFactory.Instance.MakeSpecialBomb(SpecialAttackPosition.position);
    }
}
