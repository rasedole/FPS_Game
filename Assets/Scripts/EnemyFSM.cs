using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//����1 : ���׹̸� FSM ���̾�׷��� ���� ���۽�Ű�� �ʹ�.
//�Ӽ�1 : ���׹��� ����, ĳ������Ʈ�ѷ�, �̵��ӵ�
//����1-1. ���׹��� ���¿� ���� ������ �ൿ�� �Ѵ�.
//����1-2. �̵� ������ ���� �÷��̾ ���󰣴�.

//����2 : �÷��̾���� �Ÿ��� ���� ���׹��� ���¸� �����Ѵ�.
//�Ӽ�2 : �÷��̾���� �Ÿ�, �÷��̾��� ��ġ
//����2-1. �÷��̾���� �Ÿ��� �����Ѵ�.
//����2-2. ��� ������ �� �Ÿ��� ��������� �̵� ���·� �����Ѵ�.
//����2-3. �̵� ������ �� �Ÿ��� �־����� ��� ���·� �����Ѵ�.
//����2-4. �̵� ������ �� �Ÿ��� ��������� ���� ���·� �����Ѵ�.
//����2-5. ���� ������ �� �Ÿ��� �־����� �̵� ���·� �����Ѵ�.

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

    //�Ӽ�1 : ���׹��� ����, ĳ������Ʈ�ѷ�, �̵��ӵ�
    private EnemyState enemyState;
    private CharacterController enemyController;
    public float speed = 5f;

    //�Ӽ�2 : �÷��̾���� �Ÿ�, �÷��̾��� ��ġ
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
        //����2-1. �÷��̾���� �Ÿ��� �����Ѵ�.
        distanceToPlayer = (playerTransform.position - transform.position).magnitude;

        //����1-1. ���׹��� ���¿� ���� ������ �ൿ�� �Ѵ�.
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
        //����2-2. ��� ������ �� �Ÿ��� ��������� �̵� ���·� �����Ѵ�.
        if (distanceToPlayer < 10f)
        {
            enemyState = EnemyState.Move;
            print("Idle -> Move");
        }
    }

    private void Move()
    {
        //����1-2. �̵� ������ ���� �÷��̾ ���󰣴�.
        Vector3 dir = (playerTransform.position - transform.position).normalized;
        enemyController.Move(speed * Time.deltaTime * dir);

        //����2-3. �̵� ������ �� �Ÿ��� �־����� ��� ���·� �����Ѵ�.
        if (distanceToPlayer > 15f)
        {
            enemyState = EnemyState.Idle;
            print("Move -> Idle");
        }

        //����2-4. �̵� ������ �� �Ÿ��� ��������� ���� ���·� �����Ѵ�.
        if(distanceToPlayer < 1f)
        {
            enemyState = EnemyState.Attack;
            print("Move -> Attack");
        }
    }

    private void Attack()
    {
        //����2-5. ���� ������ �� �Ÿ��� �־����� �̵� ���·� �����Ѵ�.
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
