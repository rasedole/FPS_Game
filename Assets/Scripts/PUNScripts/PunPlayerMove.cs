using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//����1 : �÷��̾ Ű �Է¿� ���� �̵���Ų��.
//�Ӽ�1 : ���ǵ�

public class PunPlayerMove : MonoBehaviour
{
    //�Ӽ�1 : ���ǵ�
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
