using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//목적1 : 일정 시간에 따라 적을 발생시킨다.
//속성1 : 적 오브젝트1, 적 오브젝트2, 현재시간, 특정시간
//순서1-1. 현재시간이 흐른다.
//순서1-2. 현재시간이 특정시간을 넘는다.
//순서1-3. 랜덤값에 따라 적을 생성한다.

public class EnemyManager : MonoBehaviour
{

    //속성1 : 적 오브젝트1, 적 오브젝트2, 현재시간, 특정시간
    public GameObject enemy1;
    public GameObject enemy2;
    public float currentTime = 0;
    public float popTime = 10f;

    // Start is called before the first frame update
    void Start()
    {
        popTime = Random.Range(5f, 15f);
    }

    // Update is called once per frame
    void Update()
    {
        //순서1-1. 현재시간이 흐른다.
        currentTime += Time.deltaTime;

        //순서1-2. 현재시간이 특정시간을 넘는다.
        if(currentTime > popTime )
        {
            //순서1-3. 랜덤값에 따라 적을 생성한다.
            if(Random.Range(0f,1f) < 0.95f)
            {
                GameObject enemyGO = Instantiate(enemy1);
                enemyGO.transform.position = this.transform.position;
            }
            else
            {
                GameObject enemyGO = Instantiate(enemy2);
                enemyGO.transform.position = this.transform.position;
                EnemyFSM fsm = enemyGO.GetComponent<EnemyFSM>();
                fsm.healthPoint = 50;
                fsm.speed = 12f;
                fsm.attackPower = 4;
                fsm.moveDistance = 3000;
            }

            currentTime = 0;
        }
    }
}
