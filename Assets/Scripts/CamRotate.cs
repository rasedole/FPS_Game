using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//목표1 : 마우스의 입력을 받아서 카메라를 회전시킨다.
//속성 : 마우스 입력 X, Y, 회전속도
//순서1-1. 사용자의 마우스 X, Y입력을 받는다.
//순서1-2. 마우스의 입력에 따라 회전방향을 설정한다.
//순서1-3. 회전방향을 향해 회전속도에 따라 회전시킨다.

public class CamRotate : MonoBehaviour
{
    public float rotateSpeed = 100.0f;
    private float mX = 0;
    private float mY = 0;

    // Update is called once per frame
    void Update()
    {
        //순서1-1. 사용자의 마우스 X, Y입력을 받는다.
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        mX += mouseX * rotateSpeed * Time.deltaTime;
        mY += mouseY * rotateSpeed * Time.deltaTime;

        mY = Mathf.Clamp(mY, -90f, 90f);

        //순서1-2. 마우스의 입력에 따라 회전방향을 설정한다.
        Vector3 rotateDirection = new Vector3(-mY, mX, 0);

        //순서1-3. 회전방향을 향해 회전속도에 따라 회전시킨다.
        transform.eulerAngles = rotateDirection;
    }
}
