using UnityEngine;

// 플레이어 이동
public class PlayerMove : MonoBehaviour
{
    // 목표
    // "키보드 입력"에 따라 "방향"을 구하고 그 방향으로 "이동"시키고 싶다.

    // 구현 순서
    // 1. 키보드 입력
    // 2. 방향 구하는 방법
    // 3. 이동

    // 필요 속성
    public float Speed = 3;   // 초당 3유닛(3칸) 이동
    public float MinX = -2f;
    public float MaxX = 2f;
    public float MinY = -5f;
    public float MaxY = 0f;
    public float speedIncrease = 1f;
    public float speedDecrease = -1f;


    // 게임 오브젝트가 게임을 시작한 후 최대한 많이
    private void Update()
    {
        // 1. 키보드 입력을 감지한다.
        // 유니티에서는 Input이라는 모듈이 입력에 관한 모든 것을 담당한다. (버튼을 눌렀냐, 키보드를 입력했냐)
        float h = Input.GetAxis("Horizontal"); // 수평 입력에 대한 값을 -1 ~ 0 ~ 1로 가져온다.
        float v = Input.GetAxis("Vertical"); // 수직 입력에 대한 값을 -1 ~ 0 ~ 1로 가져온다.

            Debug.Log($"h: {h}, v: {v}");

        // 2. 입력으로부터 방향을 구한다.
        // 벡터: 크기와 방향을 표현하는 물리 개념
        Vector2 direction = new Vector2(h, v);  // Vector2는 x, y만, Vector3는 z까지 포함
        Debug.Log($"direction: {direction.x}, {direction.y}");

        // 3. 그 방향으로 이동한다.
        Vector2 position = transform.position;  // 현재 위치

        // 새로운 위치 = 현재 위치 + (방향 * 속력) * 시간
        // 새로운 위치 = 현재 위치 + 속도 * 시간

        //      새로운 위치 = 현재 위치+   방향    *  속력
        Vector2 newPosition = position + direction * Speed * Time.deltaTime;  // 새로운 위치

        // Time: 시간을 담당하는 모듈
        // Time.deltaTime: 이전 프레임으로부터 현재 프레임까지 시간이 얼마나 흘렀는지 나타내는 값
        // ㄴ 1 / fps 값과 비슷하기에 컴퓨터 사양에 관계 없이 일정한 속도로 움직이게 해준다.

        // 이동속도가 10일때
        // 컴퓨터 1: 50FPS: Update가 초당 50번 실행 -> 10 * 50 = 500      * Time.deltaTime = 두 값이 같아진다.
        // 컴퓨터 2: 100FPS: Update가 초당 100번 실행 -> 10 * 100 = 1000  * Time.deltaTime 이동, 확대축소 등 실시간 관련은 다 써줘야한다.

        // 1, 0, -1, 0.0000001 이 숫자 말고는 다 매직넘버다. 변수로 빼야한다.

        // 포지션 값에 제한을 둔다.
        if (newPosition.x > MaxX)
        {
            newPosition.x = MaxX;
        }
        else if (newPosition.x < MinX)
        {
            newPosition.x = MinX;
        }
        if (newPosition.y > MaxY)
        {
            newPosition.y = MaxY;
        }
        else if (newPosition.y < MinY)
        {
            newPosition.y = MinY;
        }

        transform.position = newPosition;       // 새로운 위치로 갱신

    }
}
