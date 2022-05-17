using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


public class MonsterManager : MonoBehaviour
{

    [SerializeField]
    Transform MonsterSpawn;
    public bool MonsterOn = false;

    float A;
    private void Start()
    {
    }

    // 몬스터 생성 위치 랜덤화
    private Vector3 GetRandomPosition(string name)
    {
        Vector3 basePosition = MonsterSpawn.position;
        switch(name)
        {
            case "Cubo":
                A = 0f;
                break;
            case "Qupas":
                A = Random.Range(1f, 5f);
                break;
            case "Qubless":
                A = Random.Range(-1f, -5f);
                break;
        }
        float posX = basePosition.x + A;
        float posY = basePosition.y + A;
        float posZ = basePosition.z;

        Vector3 spawnPos = new Vector3(posX, posY, posZ);

        return spawnPos;
    }

    void ChangeOn()
    {
        MonsterOn = false;
    }

    public void Generate(string monsterName, int count)
    {
        MonsterOn = true;
        for (int i = 0; i < count; i++)
        {
            GameObject monster = ObjectPooler.SpawnFromPool(monsterName, GetRandomPosition(monsterName), MonsterSpawn.rotation);
            monster.GetComponent<NavMeshAgent>().enabled = false;
            monster.transform.rotation = Quaternion.identity;
        }
    }
}
