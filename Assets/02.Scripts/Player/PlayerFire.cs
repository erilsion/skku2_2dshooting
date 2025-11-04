using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    // 목표: 스페이스바를 누르면 총알을 만들어서 발사하고 싶다.

    // 필요 속성
    [Header("총알 프리팹")]  // 복사해올 총알 프리팹 게임 오브젝트
    public GameObject BulletPrefab;


    [Header("총구")]
    public Transform FirePosition;
    public Vector3 TwoBullet = new Vector2(1, 0);


    [Header("쿨타임")]
    private float _fireTimer = 0f;
    private float FireCooldown = 0.6f;


    [Header("자동 / 수동 공격")]
    private KeyCode AutoAtteck = KeyCode.Keypad1;
    private KeyCode NotAutoAtteck = KeyCode.Keypad2;


    private void Update()
    {
        _fireTimer += Time.deltaTime;
        if (Input.GetKeyDown())
        {

        }
        // 1. 발사 버튼을 누르고 있으면
        if (Input.GetKey(KeyCode.Space) && _fireTimer > FireCooldown)
        {
            // 2. 프리팹으로부터 총알(게임 오브젝트)을 생성한다.
            // 유니티에서 게임 오브젝트를 생성할 때는 new가 Instantiate 라는 메서드를 이용한다.
            // 클래스 -> 객체(속성+기능) -> 메모리에 로드된 객체를 인스턴스 => 인스턴스화
            GameObject bullet1 = Instantiate(BulletPrefab);
            GameObject bullet2 = Instantiate(BulletPrefab);


            // 3. 총알 생성 후 위치를 플레이어 위치로 수정한다.
            bullet1.transform.position = FirePosition.position + TwoBullet;
            bullet2.transform.position = FirePosition.position - TwoBullet;


            // 4. 쿨타임 초기화
            _fireTimer = 0f;
        }
    }
}
