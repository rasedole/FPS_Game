using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//목적1 : 일정 시간에 따라 적을 발생시킨다.
//속성1 : 적 오브젝트, 현재시간, 특정시간
//순서1-1. 현재시간이 흐른다.
//순서1-2. 현재시간이 특정시간을 넘는다.
//순서1-3. 랜덤값에 따라 적을 생성한다.

public class EnemyManager : MonoBehaviour
{

    //속성1 : 적 오브젝트, 현재시간, 특정시간
    public GameObject enemy;
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

        }
    }
}
