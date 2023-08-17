using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//목표1 : 마우스의 입력을 받아서 플레이어를 회전시킨다.
//속성1 : 마우스 입력 X, 회전속도
//순서1-1. 사용자의 마우스 X입력을 받는다.
//순서1-2. 마우스의 입력에 따라 회전방향을 설정한다.
//순서1-3. 회전방향을 향해 회전속도에 따라 회전시킨다.

public class PlayerRotate : MonoBehaviour
{
    //속성1 : 마우스 입력 X, 회전속도
    public float rotateSpeed = 200.0f;
    protected float mouseX;

    // Update is called once per frame
    void Update()
    {
        //순서1-1. 사용자의 마우스 X입력을 받는다.
        mouseX = Input.GetAxis("Mouse X");

        //순서1-2. 마우스의 입력에 따라 회전방향을 설정한다.
        Vector3 rotateDirection = new Vector3(0, mouseX, 0);

        //순서1-3. 회전방향을 향해 회전속도에 따라 회전시킨다.
        transform.eulerAngles += rotateSpeed * Time.deltaTime * rotateDirection;
    }
}
