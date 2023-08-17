using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//목적1 : 마우스 우클릭을 누르면 폭탄을 특정방향으로 발사한다.
//속성1 : 폭탄 게임오브젝트, 발사 위치, 방향, 힘
//순서1-1. 마우스 우클릭을 누른다.
//순서1-2. 폭탄 오브젝트를 생성한다.
//순서1-3. 폭탄 오브젝트의 발사 위치를 설정한다.
//순서1-4. 폭탄 오브젝트를 카메라방향으로 힘에 비례해서 발사한다.

public class PlayerFire : MonoBehaviour
{
    //속성1 : 폭탄 게임오브젝트, 발사 위치, 방향, 힘
    public GameObject bomb;
    public GameObject firePosition;
    protected Vector3 direction;
    public float power = 10f;

    // Update is called once per frame
    void Update()
    {
        //순서1-1. 마우스 우클릭을 누른다.
        if (Input.GetMouseButtonDown(1))
        {
            //순서1-2. 폭탄 오브젝트를 생성한다.
            GameObject bombGO = Instantiate(bomb);

            //순서1-3. 폭탄 오브젝트의 발사 위치를 설정한다.
            bombGO.transform.position = firePosition.transform.position;

            //순서1-4. 폭탄 오브젝트를 카메라방향으로 힘에 비례해서 발사한다.
            Rigidbody rigidbody = bombGO.GetComponent<Rigidbody>();
            direction = Camera.main.transform.forward;
            rigidbody.AddForce(direction * power, ForceMode.Impulse);
        }
    }
}
