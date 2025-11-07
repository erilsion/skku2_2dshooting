using UnityEngine;
public enum EItemtype
{
    SpeedItem
}

public class Item : MonoBehaviour
{
    // 충돌 트리거가 일어났을 때
    // 만약 플레이어 태그라면
    // 플레이어 게임 오브젝트의 플레이어무브 컴포넌트를 읽어온다
    // 스피드를 +N 해준다
    // 아이템 오브젝트 삭제

    [Header("아이템 프리팹")]
    public EItemtype Type;

    [Header("아이템 효과")]
    private int SpeedUpAmount = 2;

    void Start()
    {
        
    }


    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") == false) return;

        PlayerMove _playerMove = other.gameObject.GetComponent<PlayerMove>();
        _playerMove.SpeedUp(SpeedUpAmount);

        Destroy(gameObject);
    }
}
