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
    [Header("능력치")]
    public float Speed = 3f;   // 초당 3유닛(3칸) 이동


    [Header("이동 제한 범위")]
    public float MinX = -2.5f;
    public float MaxX = 2.5f;
    public float MinY = -5f;
    public float MaxY = 0f;


    [Header("시작위치")]
    private Vector3 _originPosition;  // 은닉화-prvate로 해준다.

    private void Start()
    {
        // 처음 시작 위치 저장
        _originPosition = transform.position;
    }


    [Header("스피드 증감")]
    public float speedIncrease = 1f;
    public float speedDecrease = -1f;
    public KeyCode speedUpKey = KeyCode.Q;
    public KeyCode speedDownKey = KeyCode.E;
    public KeyCode GoBack = KeyCode.R;
    public float maxSpeed = 10f;
    public float minSpeed = 1f;


    // 게임 오브젝트가 게임을 시작한 후 최대한 많이
    private void Update()
    {
        // GetKey: 키가 눌려있는지 계속 감지
        // GetKeyDown: 키가 눌린 순간 1번만 감지
        // GetKeyUp: 키가 떼어진 순간 1번만 감지

        // 쉬프트 누르는 동안 속도 증가
        if (Input.GetKey(KeyCode.LeftShift))
        {
            Speed = 6f;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            Speed = 3f;
        }

        if (Input.GetKey(GoBack))
        {
            TranslateToOrigin();
            return;
        }

        // Speed = Mathf.Clamp(Speed, minSpeed, maxSpeed); - 속도를 최소값과 최대값 사이로 반환해준다.
        // float finalSpeed = Speed;
        // if (Input.GetKey(KeyCode.LeftShift))
        // {
        //    finalSpeed = 2 * Speed;
        // }


        // 스피드 업 / 다운
        if (Input.GetKeyDown(speedUpKey))
        {
            Speed += speedIncrease;
            if (Speed > maxSpeed)
            {
                Speed = maxSpeed;
            }
        }
        if (Input.GetKeyDown(speedDownKey))
        {
            Speed += speedDecrease;
            if (Speed < minSpeed)
            {
                Speed = minSpeed;
            }
        }

        

        // 1. 키보드 입력을 감지한다.
        // 유니티에서는 Input이라는 모듈이 입력에 관한 모든 것을 담당한다. (버튼을 눌렀냐, 키보드를 입력했냐)
        float h = Input.GetAxis("Horizontal"); // 수평 입력에 대한 값을 -1 ~ 0 ~ 1로 가져온다. (가속도 존재)
        float v = Input.GetAxis("Vertical"); // 수직 입력에 대한 값을 -1 ~ 0 ~ 1로 가져온다.



        // Input.GetAxisRaw: 딜레이 없이 바로 반응 (가속도 X)
        // float h = Input.GetAxisRaw("Horizontal"); 로 하면 천천히 가속되는게 아니라 바로 1이나 -1로 감지된다.


        // 2. 입력으로부터 방향을 구한다.
        // 벡터: 크기와 방향을 표현하는 물리 개념
        Vector2 direction = new Vector2(h, v);  // Vector2는 x, y만, Vector3는 z까지 포함


        // 방향을 크기 1로 만드는 정규화를 한다.
        direction.Normalize();  // direction = direction.normalized; 와 동일


        // 3. 그 방향으로 이동한다.
        Vector2 position = transform.position;  // 현재 위치

        // 이동하는 쉬운 방법
        // transform.Translate(direction * Speed * Time.deltaTime);


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
            newPosition.x = MinX;
        }
        else if (newPosition.x < MinX)
        {
            newPosition.x = MaxX;
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

    private void TranslateToOrigin()
    {
        // 1. 입력을 받는다.
        // 2. 방향을 구한다.
        Vector2 direction = _originPosition - transform.position;
        // 3. 이동을 한다.
        transform.Translate(translation: direction * Speed * Time.deltaTime);
    }
}

