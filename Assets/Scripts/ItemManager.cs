using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//목적1 : 일정시간마다 현재 위치에 아이템을 생성한다.
//속성1 : 아이템 게임오브젝트, 현재시간, 일정시간
//순서1-1. 현재시간이 흐른다.
//순서1-2. 현재시간이 일정시간을 넘는다.
//순서1-3. 아이템을 생성하고 현재시간을 초기화한다.

public class ItemManager : MonoBehaviour
{
    public static ItemManager Instance;

    //속성1 : 아이템 게임오브젝트, 현재시간, 일정시간, 아이템 트리거
    public GameObject item;
    public float currentTime = 0;
    public float createTime = 60;
    public bool isItem = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    // Update is called once per frame
    void Update()
    {
        //순서1-1. 현재시간이 흐른다.
        currentTime += Time.deltaTime;

        if(!isItem)
        {
            //순서1-2. 현재시간이 일정시간을 넘는다.
            if(currentTime > createTime)
            {
                //순서1-3. 아이템을 생성하고 현재시간을 초기화한다.
                Instantiate(item, transform);

                currentTime = 0;

                isItem = true;
            }
        }
        else
        {
            Mathf.Clamp(currentTime, 0, 50);
        }
    }
}
