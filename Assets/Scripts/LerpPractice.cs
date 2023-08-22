using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//목적1 : 시작점부터 끝점까지 일정시간안에 이동한다.
//속성1 : 시작점 위치, 끝점 위치, 일정시간, 현재시간

public class LerpPractice : MonoBehaviour
{
    //속성1 : 시작점 위치, 끝점 위치, 일정시간
    public Transform startPoint;
    public Transform endPoint;
    public float moveTime = 3;
    private float currentTime = 0;
    private bool moveLeftToRight = true;

    // Update is called once per frame
    void Update()
    {
        if (moveLeftToRight)
        {
            currentTime += Time.deltaTime;
        }
        else
        {
            currentTime -= Time.deltaTime;
        }

        if ((transform.position - endPoint.position).magnitude < 0.2f)
        {
            moveLeftToRight = false;
        }
        else if ((transform.position - startPoint.position).magnitude < 0.2f)
        {
            moveLeftToRight = true;
        }

        transform.position = Vector3.Lerp(startPoint.position, endPoint.position, currentTime / moveTime);

    }
}
