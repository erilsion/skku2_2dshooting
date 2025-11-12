using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    // 목표: 적을 죽일 때마다 점수를 올리고, 현재 점수를 UI에 표시하고 싶다.
    // 필요 속성: 현재 점수 UI(Text 컴포넌트), 현재 점수를 기억할 변수

    // 규칙: UI 요소는 항상 변수명 뒤에 UI라고 붙인다.
    public Text CurrentScoreTextUI;

    [Header("점수")]
    private int _currentScore = 0;

    void Start()
    {
        CurrentScoreTextUI.text = $"현재 점수: {_currentScore}";
    }

    void Update()
    {
        
    }
}
