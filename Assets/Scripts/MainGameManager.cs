using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//����1 : SpawnPoint�� �÷��̾ ��ġ��Ų��.
//�Ӽ�1 : SpawnPoint, Player������Ʈ

//����2 : Ư�� �ð��� �����ų� �÷��̾ ��� ������ ������ �����ϵ��� ����Ѵ�.
//�Ӽ�2 : ���� �ð�, Ư�� �ð�, ���� ���� ��� flag

public class MainGameManager : MonoBehaviour
{
    public static MainGameManager Instance;

    //�Ӽ�1 : SpawnPoint, Player������Ʈ
    public Transform[] spawnPoints;
    public GameObject player;

    //�Ӽ�2 : ���� �ð�, Ư�� �ð�, ���� ���� ��� flag
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
