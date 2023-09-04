using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//목적1 : 플레이어를 키 입력에 따라 이동시킨다.
//속성1 : 스피드

public class PunPlayerMove : MonoBehaviour
{
    //속성1 : 스피드
    public float speed = 3f;
    PhotonView view;

    private void Start()
    {
        view = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if (view.IsMine)
        {
            if (Input.GetKey(KeyCode.A))
            {
                transform.Translate(-speed * Time.deltaTime, 0, 0);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                transform.Translate(0, -speed * Time.deltaTime, 0);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                transform.Translate(speed * Time.deltaTime, 0, 0);
            }
            else if (Input.GetKey(KeyCode.W))
            {
                transform.Translate(0, speed * Time.deltaTime, 0);
            }
        }
    }
}
