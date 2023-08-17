using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//목표1 : 이펙트가 특정 시간이 지난후 제거된다.
//속성1 : 현재시간, 특정시간
//순서1-1. 현재시간이 흐른다.
//순서1-2. 현재시간이 특정시간을 넘는다.
//순서1-3. 이펙트를 파괴한다.

public class EffectDestroy : MonoBehaviour
{
    //속성1 : 현재시간, 특정시간
    public float destroyTime = 2.0f;
    protected float currentTime = 0f;

    // Update is called once per frame
    void Update()
    {
        //순서1-1. 현재시간이 흐른다.
        currentTime += Time.deltaTime;

        //순서1-2. 현재시간이 특정시간을 넘는다.
        if(currentTime > destroyTime)
        {
            //순서1-3. 이펙트를 파괴한다.
            Destroy(gameObject);
        }
    }
}
