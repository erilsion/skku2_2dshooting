using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    // 응집도를 높혀라
    // 응집도: '데이터'와 '데이터를 조작하는 로직'이 얼마나 잘 모여있냐
    // 응집도를 높이고, 필요한 것만 외부에 공개하는 것을 '캡슐화'


    // 목표: 적을 죽일 때마다 점수를 올리고, 현재 점수를 UI에 표시하고 싶다.
    // 필요 속성: 현재 점수 UI(Text 컴포넌트), 현재 점수를 기억할 변수

    // 규칙: UI 요소는 항상 변수명 뒤에 UI라고 붙인다.
    [SerializeField] private Text _currentScoreTextUI;

    [Header("점수")]
    private int _currentScore = 0;

    void Start()
    {
        Refresh();
    }

    void Update()
    {
        Refresh();
    }
    public void AddScore(int score)
    {
        if (score <= 0) return;

        _currentScore += score;
    }

    public void Refresh()
    {
        _currentScoreTextUI.text = $"현재 점수: {_currentScore}";
    }
}
