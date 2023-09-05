using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//목적1 : SpawnPoint에 플레이어를 위치시킨다.
//속성1 : SpawnPoint, Player오브젝트

//목적2 : 특정 시간이 지나거나 플레이어가 모두 들어오면 게임을 시작하도록 명령한다.
//속성2 : 현재 시간, 특정 시간, 게임 시작 명령 flag

public class MainGameManager : MonoBehaviour
{
    public static MainGameManager Instance;

    //속성1 : SpawnPoint, Player오브젝트
    public Transform[] spawnPoints;
    public GameObject player;

    //속성2 : 현재 시간, 특정 시간, 게임 시작 명령 flag
    protected float currentTime = 0;
    public float gameStartTime = 10f;
    public bool isGameStarted = false;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    public void SetSpawnPoints()
    {
        Transform spawnPointsParent = GameObject.Find("GroupSpawnPoint").transform;
        spawnPoints = new Transform[spawnPointsParent.childCount];
        for (int i = 0; i < spawnPointsParent.childCount; i++)
        {
            spawnPoints[i] = spawnPointsParent.GetChild(i).transform;
        }
    }

    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(gameStartTime);
        
        isGameStarted = true;

        SetSpawnPoints();
    }

    public void StartTimer()
    {
        StartCoroutine(StartGame());
    }
}
