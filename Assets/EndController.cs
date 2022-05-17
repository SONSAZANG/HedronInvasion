using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndController : MonoBehaviour
{
    [SerializeField] Text score, end;
    private void Awake()
    {
    }
    void Start()
    {
        score.text = GameManager.Instance.score.ToString();
        end.text = GameManager.Instance.endText;
        Destroy(GameManager.Instance.gameObject);
    }

}
