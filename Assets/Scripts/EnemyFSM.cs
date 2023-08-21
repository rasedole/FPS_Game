using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//목적1 : 에네미를 FSM 다이어그램에 따라 동작시키고 싶다.
//속성1 : 에네미의 상태, 캐릭터컨트롤러, 이동속도
//순서1-1. 에네미의 상태에 따라 정해진 행동을 한다.
//순서1-2. 이동 상태일 때는 플레이어를 따라간다.

//목적2 : 플레이어와의 거리에 따라 에네미의 상태를 변경한다.
//속성2 : 플레이어와의 거리, 플레이어의 위치
//순서2-1. 플레이어와의 거리를 측정한다.
//순서2-2. 대기 상태일 때 거리가 가까워지면 이동 상태로 변경한다.
//순서2-3. 이동 상태일 때 거리가 멀어지면 대기 상태로 변경한다.
//순서2-4. 이동 상태일 때 거리가 가까워지면 공격 상태로 변경한다.
//순서2-5. 공격 상태일 때 거리가 멀어지면 이동 상태로 변경한다.

public class EnemyFSM : MonoBehaviour
{
    enum EnemyState
    {
        Idle,
        Move,
        Attack,
        Return,
        Damaged,
        Die
    }

    //속성1 : 에네미의 상태, 캐릭터컨트롤러, 이동속도
    private EnemyState enemyState;
    private CharacterController enemyController;
    public float speed = 5f;

    //속성2 : 플레이어와의 거리, 플레이어의 위치
    public float distanceToPlayer;
    private Transform playerTransform;


    // Start is called before the first frame update
    void Start()
    {
        enemyState = EnemyState.Idle;
        enemyController = GetComponent<CharacterController>();
        playerTransform = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        //순서2-1. 플레이어와의 거리를 측정한다.
        distanceToPlayer = (playerTransform.position - transform.position).magnitude;

        //순서1-1. 에네미의 상태에 따라 정해진 행동을 한다.
        switch (enemyState)
        {
            case EnemyState.Idle:
                Idle();
                break;
            case EnemyState.Move:
                Move();
                break;
            case EnemyState.Attack:
                Attack();
                break;
            case EnemyState.Return:
                Return();
                break;
            case EnemyState.Damaged:
                Damaged();
                break;
            case EnemyState.Die:
                Die();
                break;
        }
    }
    private void Idle()
    {
        //순서2-2. 대기 상태일 때 거리가 가까워지면 이동 상태로 변경한다.
        if (distanceToPlayer < 10f)
        {
            enemyState = EnemyState.Move;
            print("Idle -> Move");
        }
    }

    private void Move()
    {
        //순서1-2. 이동 상태일 때는 플레이어를 따라간다.
        Vector3 dir = (playerTransform.position - transform.position).normalized;
        enemyController.Move(speed * Time.deltaTime * dir);

        //순서2-3. 이동 상태일 때 거리가 멀어지면 대기 상태로 변경한다.
        if (distanceToPlayer > 15f)
        {
            enemyState = EnemyState.Idle;
            print("Move -> Idle");
        }

        //순서2-4. 이동 상태일 때 거리가 가까워지면 공격 상태로 변경한다.
        if(distanceToPlayer < 1f)
        {
            enemyState = EnemyState.Attack;
            print("Move -> Attack");
        }
    }

    private void Attack()
    {
        //순서2-5. 공격 상태일 때 거리가 멀어지면 이동 상태로 변경한다.
        if(distanceToPlayer > 2f)
        {
            enemyState = EnemyState.Move;
            print("Attack -> Move");
        }
    }

    private void Return()
    {

    }

    private void Damaged()
    {

    }

    private void Die()
    {

    }
}
