using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : Singleton<GameManager>
{
    [SerializeField] GameObject MainPlayer, SecondRoundPosition, ThirdRoundPosition;
    [SerializeField] MonsterManager monsterManager;

    private GameObject roundPosition;
    private string roundFlyAni;

    public string endText;

    public Text textEarthHP, textTime, textRound, textScore;
    public int earthHP = 200, round = 1, score = 0;
    public float roundTime;

    public int maxRound = 3;

    public AudioClip audioRoundChange;
    public AudioSource audioSource;

    public Animator FlyingMonster;
    public ParticleSystem FlyMonsterPt;

    public GameObject RoundClearCanvas;
    public Text RoundClearText;
    private void Awake()
    {
        roundTime = 60f;
        audioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        // TimeCheck(roundTime);
        FlyingMonster.Play("Flying");
        Invoke(nameof(FlyingMonsterParticle), 5f);
        textRound.text = round.ToString();
        RoundMonsterGen(1);
    }
    private void Update()
    {
        if (roundTime > 0)
        {
            roundTime -= Time.deltaTime;
            textTime.text = Mathf.Ceil(roundTime).ToString();
        }
        else if (roundTime < 0)
        {
            StartCoroutine(nameof(RoundCheck));
        }
        else if (roundTime == 0)
        {
            return;
        }
    }

    /// <summary>
    /// 60초 카운트 후 라운드 확인 로직
    /// </summary>
    /// <returns></returns>
    public IEnumerator RoundCheck()
    {
        if (round < 3)
        {
            roundTime = 0;
            round += 1;
            textRound.text = round.ToString();
            audioSource.clip = audioRoundChange;
           
            switch (round)
            {
                case 2:
                    roundPosition = SecondRoundPosition;
                    roundFlyAni = "SecondRound";
                    break;
                case 3:
                    roundPosition = ThirdRoundPosition;
                    roundFlyAni = "ThirdRound";
                    break;
            }
            Debug.Log("Next Round and Rest 10 Sec");
            RoundClearText.text = ((round - 1) + " ROUND CLEAR");
            RoundClearCanvas.SetActive(true);
            yield return new WaitForSeconds(5);
            RoundClearCanvas.SetActive(false);
            audioSource.Play();
            FlyingMonster.Play(roundFlyAni);
            yield return new WaitForSeconds(5);
            roundTime = 60;
            RoundMonsterGen(round);
            Debug.Log(round + " Round Start!!!");
        }
        else if (round == 3)
        {
            RoundClearText.text = "YOU WIN";
            RoundClearCanvas.SetActive(true);
            yield return new WaitForSeconds(3);
            Debug.Log("GameEnd");
            endText = "You WIN!!";
            SceneManager.LoadScene("EndScene");
        }
        
    }
    void TimeCheck(float time)
    {
        while (time > 0)
        {
            time -= Time.deltaTime;
            Debug.Log(time);
            textTime.text = Mathf.Ceil(time).ToString();
            if (time < 1)
            {
                Debug.Log("roundTImeOUt");
                break;
            }
        }
    }

    /// <summary>
    /// 라운드별 몬스터 생성
    /// </summary>
    /// <param name="round"></param>
    void RoundMonsterGen(int round)
    {
        switch (round)
        {
            case 1:
                StartCoroutine(Generate("Cubo", 1));
                StartCoroutine(Generate("Qupas", 1));
                StartCoroutine(Generate("Qubless", 1));
                break;
            case 2:
                StartCoroutine(Generate("Cubo", 2));
                StartCoroutine(Generate("Qupas", 2));
                StartCoroutine(Generate("Qubless", 2));
                break;
            case 3:
                StartCoroutine(Generate("Cubo", 2));
                StartCoroutine(Generate("Cubo", 2));
                StartCoroutine(Generate("Qupas", 2));
                StartCoroutine(Generate("Qubless", 2));
                StartCoroutine(Generate("Qubless", 1));
                break;
        }
    }


    /// <summary>
    /// 몬스터를 15초 단위 3번 로 생성하게 만드는 코드
    /// </summary>
    /// <param name="monsterName"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    private IEnumerator Generate(string monsterName, int count)
    {
        int CountCheck = 3;
        // while문 구현 후 MonsterOn 조건 확인해서 15초 단위로 생성 후 조건 걸어서 아웃
        while (true)
        {
            if (CountCheck > 0)
            {
                yield return new WaitForSeconds(15);
                monsterManager.Generate(monsterName, count);
                CountCheck -= 1;
            }
            else break;
        }
        yield return new WaitForSeconds(5);
    }

    public void ScoreTextSet()
    {
        textScore.text = score.ToString();
    }

    public void EarthHPTextSet()
    {
        if(earthHP < 0)
        {
            StartCoroutine(EarthHPEnd());
        }
        textEarthHP.text = earthHP.ToString();
    }
    IEnumerator EarthHPEnd()
    {
        RoundClearText.text = "YOU FAIL";
        RoundClearCanvas.SetActive(true);
        yield return new WaitForSeconds(3f);
        endText = "You FAIL";
        SceneManager.LoadScene("EndScene");
    }

    void FlyingMonsterParticle()
    {
        FlyMonsterPt.Play();
    }
}
