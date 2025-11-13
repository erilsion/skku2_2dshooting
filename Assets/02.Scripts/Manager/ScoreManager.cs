using DG.Tweening;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

[System.Serializable]
public class  UserData
{
    public int currentScore;
    public int highScore;
}

public class ScoreManager : MonoBehaviour
{
    // ScoreManager는 단 하나여야 한다.
    // 전역적인 접근점을 제공해야 한다.
    // 게임 개발에서는 Manager(관리자) 클래스를 보통 싱글톤 패턴으로 사용하는 것이 관행이다.

    private static ScoreManager _instance = null;  // 은닉화
    public static ScoreManager Instance => _instance;  // Get 프로퍼티

    private void Awake()
    {
        // 만약 ScoreManager가 두개다. => 랜덤하게 호출된다.
        // 그렇기에 후발 주자는 삭제해버려야 한다.
        if (_instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        _instance = this;
    }

    [SerializeField]
    private Text _currentScoreTextUI;
    [SerializeField]
    private Text _highScoreTextUI;

    private int _currentScore = 0;
    private int _highScore = 0;
    private const string ScoreKey = "Score";
    private Vector2 _scoreEffect = new Vector2(1.3f, 1.3f);
    private Vector2 _scoreEffectOrigin = new Vector2(1f, 1f);
    private float _scoreEffectTime = 0.2f;


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

        _currentScoreTextUI.transform.DOScale(_scoreEffect, _scoreEffectTime)
        .OnComplete(() => { _currentScoreTextUI.transform.DOScale(_scoreEffectOrigin, _scoreEffectTime); });

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
        UserData data = new UserData();
        data.currentScore = _currentScore;
        data.highScore = _highScore;

        string json = JsonUtility.ToJson(data);

        PlayerPrefs.SetString(ScoreKey, json);
        PlayerPrefs.Save();

        Debug.Log("점수 저장 완료: " + json);
    }

    private void Load()
    {
        if (PlayerPrefs.HasKey(ScoreKey))
        {
            string json = PlayerPrefs.GetString(ScoreKey);

            UserData data = JsonUtility.FromJson<UserData>(json);

            _highScore = data.highScore;
            _currentScore = 0;

            Debug.Log("점수 불러오기 완료: " + json);
        }
        else
        {
            _highScore = 0;
            _currentScore = 0;

            Debug.Log("저장된 점수가 없어 새로 시작합니다.");
        }
    }
}
