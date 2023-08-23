using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//목표1 : 마우스의 입력을 받아서 카메라를 회전시킨다.
//속성1 : 마우스 입력 X, Y, 회전속도
//순서1-1. 사용자의 마우스 X, Y입력을 받는다.
//순서1-2. 마우스의 입력에 따라 회전방향을 설정한다.
//순서1-3. 회전방향을 향해 회전속도에 따라 회전시킨다.

//목표2 : 키보드 Q, E 입력을 받으면 좌, 우로 45도 기울인다.
//속성2 : Z회전, 현재시간 목표시간
//순서2-1. 키보드에서 Q입력을 받으면
//순서2-2. 카메라를 좌로 45도까지 기울인다.
//순서2-3. 키보드에서 E입력을 받으면
//순서2-4. 카메라를 우로 45도까지 기울인다.
//순서2-5. 버튼을 해제하면 원래 상태로 돌아온다.

//목적3 : Ready, GameOver 상태일 때 플레이어가 움직일 수 없도록 한다.

public class CamRotate : MonoBehaviour
{
    //속성1 : 마우스 입력 X, Y, 회전속도
    public float rotateSpeed = 800.0f;
    public float mX = 0;
    public float mY = 0;

    //속성2 : Z회전, 현재시간 목표시간
    private float rotateZ = 0;
    private float currentTime = 0;
    public float moveTime = 0.5f;

    public GameObject player;

    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.state != GameManager.GameState.Start)
        {
            return;
        }

        //순서1-1. 사용자의 마우스 X, Y입력을 받는다.
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        mX += mouseX * rotateSpeed * Time.deltaTime;
        mY += mouseY * rotateSpeed * Time.deltaTime;

        mY = Mathf.Clamp(mY, -90f, 90f);

        //순서1-2. 마우스의 입력에 따라 회전방향을 설정한다.
        Vector3 rotateDirection = new Vector3(-mY, mX, rotateZ);

        //순서1-3. 회전방향을 향해 회전속도에 따라 회전시킨다.
        transform.eulerAngles = rotateDirection;

        ////순서2-1. 키보드에서 Q입력을 받으면
        //if (Input.GetKey(KeyCode.Q))
        //{
        //    if(currentTime < moveTime)
        //    {
        //        //순서2-2. 카메라를 좌로 45도까지 기울인다.
        //        currentTime += Time.deltaTime;
        //        transform.eulerAngles = Vector3.Lerp(rotateDirection, new Vector3(-mY, mX, 45), currentTime / moveTime);
        //    }
        //    rotateZ = transform.eulerAngles.z;
        //}
        ////순서2-3. 키보드에서 E입력을 받으면
        //else if (Input.GetKey(KeyCode.E))
        //{
        //    if(currentTime < moveTime)
        //    {
        //        //순서2-4. 카메라를 우로 45도까지 기울인다.
        //        currentTime += Time.deltaTime;
        //        transform.eulerAngles = Vector3.Lerp(rotateDirection, new Vector3(-mY, mX, -45), currentTime / moveTime);
        //    }
        //    rotateZ = transform.eulerAngles.z;
        //}
        //else
        //{
        //    currentTime = 0;
        //    //if (currentTime < moveTime)
        //    //{
        //    //    //순서2-4. 카메라를 우로 45도까지 기울인다.
        //    //    currentTime += Time.deltaTime;
        //    //    transform.eulerAngles = Vector3.Lerp(rotateDirection, new Vector3(-mY, mX, 0), currentTime / moveTime);
        //    //}
        //    //rotateZ = transform.eulerAngles.z;
        //    rotateZ = 0;
        //}
    }
}
