using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    // 응집도를 높혀라
    // 응집도: '데이터'와 '데이터를 조작하는 로직'이 얼마나 잘 모여있냐
    // 응집도를 높이고, 필요한 것만 외부에 공개하는 것을 '캡슐화'


    // 목표: 적을 죽일 때마다 점수를 올리고, 현재 점수를 UI에 표시하고 싶다.
    // 필요 속성: 현재 점수 UI(Text 컴포넌트), 현재 점수를 기억할 변수

    // 규칙: UI 요소는 항상 변수명 뒤에 UI라고 붙인다.
    [SerializeField]
    private Text _currentScoreTextUI;
    [SerializeField]
    private Text _highScoreTextUI;

    private int _currentScore = 0;
    private int _highScore = 0;
    private const string ScoreKey = "Score";


    void Start()
    {
        Load();
        Refresh();
    }

    // 하나의 메소드는 한 가지 일만 잘 하면 된다.

    public void AddScore(int score)
    {
        if (score <= 0) return;

        _currentScore += score;

        if (_currentScore >= _highScore)
        {
            _highScore = _currentScore;
        }

        Refresh();
        Save();
    }

    public void Refresh()
    {
        _currentScoreTextUI.text = $"현재 점수: {_currentScore}";
        _highScoreTextUI.text = $"최고 점수: {_highScore}";
    }

    private void Save()
    {
        // 유니티에서는 값을 저장할 때 'PlayerPrefs' 모듈을 쓴다.
        // 저장 가능한 자료형은 int, float, string
        // 저장을 할 때는 저장할 이름(key)과 값(value) 이 두 형태로 저장
        // 저장: Set
        // 로드: Get

        PlayerPrefs.SetInt(ScoreKey, _currentScore);
        if (_currentScore >= _highScore)
        {
            PlayerPrefs.SetInt(ScoreKey, _highScore);
        }
    }

    private void Load()
    {
        // int score = 0;
        // if (PlayerPrefs.HasKey("score"))   // 값이 없을 경우 1. 검사
        // {
        //     score = PlayerPrefs.GetInt("score");
        // }

        // string name = PlayerPrefs.GetString("name", "티모");   // 2. default 인자
        _highScore = PlayerPrefs.GetInt(ScoreKey, _highScore);
        _currentScore = 0;
    }
}
