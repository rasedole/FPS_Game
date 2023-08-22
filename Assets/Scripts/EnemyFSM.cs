using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//목표1 : 에네미를 FSM 다이어그램에 따라 동작시키고 싶다.
//속성1 : 에네미의 상태, 캐릭터컨트롤러, 이동속도, 플레이어 게임오브젝트, 공격력, 초기 위치, HP
//순서1-1. 에네미의 상태에 따라 정해진 행동을 한다.
//순서1-2. 이동 상태일 때는 플레이어를 따라간다.
//순서1-3. 공격 상태일 때는 플레이어를 공격한다.
//순서1-4. 복귀 상태일 때는 초기 위치로 이동한다.
//순서1-5. 피격 상태일 때는 HP를 감소시킨다.
//순서1-6. 사망 상태일 때는 삭제한다.

//목적2 : 플레이어와의 거리에 따라 에네미의 상태를 변경한다.
//속성2 : 플레이어와의 거리, 플레이어의 위치, 이동거리, 공격거리, 공격속도, 현재시간, 복귀거리
//순서2-1. 플레이어와의 거리를 측정한다.
//순서2-2. 대기 상태일 때 거리가 가까워지면 이동 상태로 변경한다.
//순서2-3. 이동 상태일 때 거리가 멀어지면 대기 상태로 변경한다.
//순서2-4. 이동 상태일 때 거리가 가까워지면 공격 상태로 변경한다.
//순서2-5. 공격 상태일 때 거리가 멀어지면 이동 상태로 변경한다.
//순서2-6. 이동 상태일 때 초기 위치에서 멀어지면 복귀 상태로 변경한다.
//순서2-7. 복구 상태일 때 초기 위치로 이동하면 대기 상태로 변경한다.
//순서2-8. 공격을 받으면 현재 상태와 상관없이 피격 상태로 변경한다.
//순서2-9. 피격 상태일 때 데미지 처리를 하고 체력이 0보다 크다면 이동 상태로 변경한다.
//순서2-10. 피격 상태일 때 데미지 처리를 하고 체력이 0보다 작거나 같다면 사망 상태로 변경한다.

//목적3 : 에네미의 HP를 HP슬라이더에 연결한다.
//속성3 : UnityEngine.UI, maxHP, hp슬라이더
//순서3-1. HP를 슬라이더에 적용한다.

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

    //속성1 : 에네미의 상태, 캐릭터컨트롤러, 이동속도, 플레이어 게임오브젝트, 공격력, 초기 위치, HP
    private EnemyState enemyState;
    private CharacterController enemyController;
    public float speed = 7.5f;
    private GameObject player;
    public int attackPower = 2;
    private Vector3 originPosition;
    public int healthPoint = 3;

    //속성2 : 플레이어와의 거리, 플레이어의 위치, 이동거리, 공격거리, 공격속도, 현재시간, 복귀거리
    private float distanceToPlayer;
    private Transform playerTransform;
    public float moveDistance = 10f;
    public float attackDistance = 2f;
    public float attackDelay = 0.5f;
    private float currentTime = 0;
    public float returnDistance = 30f;

    //속성3 : maxHP, hp슬라이더
    private int maxHealthPoint;
    public Slider healthPointSlider;

    // Start is called before the first frame update
    void Start()
    {
        enemyState = EnemyState.Idle;
        enemyController = GetComponent<CharacterController>();
        playerTransform = GameObject.Find("Player").transform;
        player = GameObject.Find("Player");
        originPosition = transform.position;

        maxHealthPoint = healthPoint;
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
                //Damaged();
                break;
            case EnemyState.Die:
                //Die();
                break;
        }

        //순서3-1. HP를 슬라이더에 적용한다.
        healthPointSlider.value = (float)healthPoint / (float)maxHealthPoint;
    }
    private void Idle()
    {
        //순서2-2. 대기 상태일 때 거리가 가까워지면 이동 상태로 변경한다.
        if (distanceToPlayer < moveDistance)
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

        //순서2-6. 이동 상태일 때 초기 위치에서 멀어지면 복귀 상태로 변경한다.
        float distanceToOrigin = (originPosition - transform.position).magnitude;
        if (distanceToOrigin > returnDistance)
        {
            enemyState = EnemyState.Return;
            print("Move -> Return");
        }
        else
        {
            //순서2-3. 이동 상태일 때 거리가 멀어지면 대기 상태로 변경한다.
            if (distanceToPlayer > (moveDistance + 2))
            {
                enemyState = EnemyState.Idle;
                print("Move -> Idle");
            }

            //순서2-4. 이동 상태일 때 거리가 가까워지면 공격 상태로 변경한다.
            if (distanceToPlayer < attackDistance)
            {
                enemyState = EnemyState.Attack;
                print("Move -> Attack");
                currentTime += 1;
            }
        }
    }

    private void Attack()
    {
        if(player.GetComponent<PlayerMove>().healthPoint > 0)
        {
            //순서1-3. 공격 상태일 때는 플레이어를 공격한다.
            currentTime += Time.deltaTime * attackDelay;
            if (currentTime > 1)
            {
                player.GetComponent<PlayerMove>().GetDamage(attackPower);
                print("공격!");
                currentTime = 0;
            }
        }

        //순서2-5. 공격 상태일 때 거리가 멀어지면 이동 상태로 변경한다.
        if (distanceToPlayer > (attackDistance + 1))
        {
            enemyState = EnemyState.Move;
            print("Attack -> Move");
            currentTime = 0;
        }
    }

    private void Return()
    {
        //순서1-4. 복귀 상태일 때는 초기 위치로 이동한다.
        Vector3 dir = (originPosition - transform.position).normalized;
        enemyController.Move(speed * 3 * Time.deltaTime * dir);

        //순서2-7. 복구 상태일 때 초기 위치로 이동하면 대기 상태로 변경한다.
        if ((originPosition - transform.position).magnitude < 0.4f)
        {
            enemyState = EnemyState.Idle;
            print("Return -> Idle");
        }
    }

    private void Damaged()
    {
        //순서2-9. 피격 상태일 때 데미지 처리를 하고 체력이 0보다 크다면 이동 상태로 변경한다.
        if (healthPoint > 0)
        {
            StartCoroutine(DamageProcess());
        }

        //순서2-10. 피격 상태일 때 데미지 처리를 하고 체력이 0보다 작거나 같다면 사망 상태로 변경한다.
        else
        {
            enemyState = EnemyState.Die;
            Die();
        }
    }

    IEnumerator DamageProcess()
    {
        //피격모션(=0.5초)가 끝날 때까지  기다린다.
        yield return new WaitForSeconds(0.5f);

        enemyState = EnemyState.Move;
        print("Damaged -> Move");
    }

    //순서2-8. 공격을 받으면 현재 상태와 상관없이 피격 상태로 변경한다.
    public void GetDamaged(int damage)
    {
        //에네미가 데미지를 처리하는 중이거나 죽은 경우 처리하지 않는다.
        if (enemyState == EnemyState.Damaged || enemyState == EnemyState.Die)
        {
            return;
        }

        enemyState = EnemyState.Damaged;
        print(string.Format("Get {0} Damage", damage));

        //순서1-5. 피격 상태일 때는 HP를 감소시킨다.
        healthPoint -= damage;

        Damaged();


    }

    private void Die()
    {
        StopAllCoroutines();

        //순서1-6. 사망 상태일 때는 삭제한다.
        StartCoroutine(DieProcess());
    }

    IEnumerator DieProcess()
    {
        yield return new WaitForSeconds(0.5f);

        print("Enemy Die");

        Destroy(gameObject);
    }
}
