using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class CuboMove : MonoBehaviour
{
    SFB_AudioManager Audio;
    MonsterJump MJ;
    Rigidbody rg;
    Animator animator;
    public GameObject firstTarget , secondTarget, thirdTarget;
    public GameObject first, second, third;
    private GameObject monsterTarget;
    private GameObject jumpTarget;
    public float speed = 5f;
    NavMeshAgent nav;
    float distance;
    public int MonsterHP;



    private string monsterName;
    private int monsterScore, monsterPower;

    public AudioClip audioDie;
    AudioSource audioSource;
    private void Awake()
    {
        this.audioSource = GetComponent<AudioSource>();
    }
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        MJ = gameObject.GetComponent<MonsterJump>();
        MJ.enabled = false;
        nav = GetComponent<NavMeshAgent>();
        rg = GetComponent<Rigidbody>();
        TargetCheck();
        StartCoroutine(NavStart(monsterTarget));
        monsterName = gameObject.name;
        Invoke("DeactiveDelay", 25f);
    }
    
    void TargetCheck()
    {
        Debug.Log(GameManager.Instance.round);
       switch(GameManager.Instance.round)
        {
            case 1:
                monsterTarget = firstTarget;
                jumpTarget = first;
                break;
            case 2:
                monsterTarget = secondTarget;
                jumpTarget = second;
                break;
            case 3:
                monsterTarget = thirdTarget;
                jumpTarget = third;
                break;
        }   
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("End"))
        {
            nav.enabled = false;
            Debug.Log("EndTouch");
            MJ.enabled = true;
            MJ.Target = jumpTarget.transform;
            MJ.StartCoroutine(nameof(MJ.SimulateProjectile));
        }
        if (other.gameObject.CompareTag("FirstEnd"))
        {
            monsterNameCheck(monsterName);
            GameManager.Instance.earthHP -= monsterPower;
            GameManager.Instance.EarthHPTextSet();
            DeactiveDelay();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            animator.SetBool("gotHit", true);
            MonsterHP -= 10;
            if (MonsterHP > 9)
            {
                return;
            }
            else
            { 
                audioSource.clip = audioDie;
                audioSource.Play();
                monsterNameCheck(monsterName);
                Debug.Log(monsterName + " Dead - Score " + monsterScore);
                GameManager.Instance.score += monsterScore;
                GameManager.Instance.ScoreTextSet();
                DeactiveDelay();
            }
        }
    }

    void DeactiveDelay()
    {
        gameObject.transform.rotation = Quaternion.identity;
        rg.velocity = Vector3.zero;
        rg.angularVelocity = Vector3.zero;
        rg.Sleep();
        
        gameObject.SetActive(false);
    }
    // 비활성화 시 OnDisable 호출
    private void OnDisable()
    {
        ObjectPooler.ReturnToPool(gameObject);
        CancelInvoke();
    }

    void monsterNameCheck(string name)
    {
        switch (name)
        {
            case "Cubo":
                monsterPower = 10;
                monsterScore = 10;
                break;
            case "Qupas":
                monsterPower = 20;
                monsterScore = 20;
                break;
            case "Qubless":
                monsterPower = 30;
                monsterScore = 30;
                break;
        }
    }

    IEnumerator NavStart(GameObject target)
    {
        yield return new WaitForSeconds(3f);
        nav.enabled = true;
        Debug.Log(target.transform.name);
        animator.SetFloat("walk", 1);
        nav.SetDestination(target.transform.position);
    }

}
