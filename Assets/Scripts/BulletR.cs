using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BulletR : MonoBehaviour
{
    private void Start()
    {
        Invoke(nameof(DeactiveDelay), 5);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Monster"))
        {
            Debug.Log("hitMonster BullerR");
            DeactiveDelay();
        }
    }

    void DeactiveDelay() => gameObject.SetActive(false);

    // ��Ȱ��ȭ �� OnDisable ȣ��
    private void OnDisable()
    {
        ObjectPooler.ReturnToPool(gameObject);
        CancelInvoke();
    }

}
