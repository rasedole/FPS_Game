using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//목표1 : W, A, S, D 키를 누르면 캐릭터를 해당 방향으로 이동시키고 싶다.
//속성 : 이동속도 
//순서1-1. 사용자의 입력을 받는다.
//순서1-2. 이동방향을 설정한다.
//순서1-3. 이동속도에 따라 이동방향으로 플레이어를 이동시킨다.

//목표2 : Space를 누르면 수직으로 점프를 한다.
//속성2 : 중력 변수, 수징 속력 변수, 점프력
//순서2-1. 캐릭터의 수직 속도에 중력을 적용한다.
//순서2-2. 캐릭터 컨트롤러로 나를 이동시키고 싶다.
//순서2-3. Space를 누르면 점프력에 따라 수직으로 이동한다.

//목표3 : 점프 중인지 확인하고 점프 중에 점프를 못하도록 한다.
//속성3 : 점프 확인 변수
//순서3-1. 점프 중 착지 했을 때 점프 상태를 false로 설정한다.
//순서3-2. 바닥에 있을 경우 수직속도를 0으로 한다.
//순서3-3. 점프를 했을 때 점프상태를 확인해서 점프 중이면 동작하지 않도록 한다.
//순서3-4. 점프를 했을 때 점프 상태를 true로 설정한다.

//목표4 : 플레이어가 피격을 당하면 hp를 damage만큼 깎는다.
//속성4 : HP

//목표5 : 현재 플레이어의 hp(%)를 hp 슬라이더에 적용한다.
//속성5 : UnityEnging.UI, maxHP, HP슬라이더
//순서5-1. HP를 슬라이더에 적용한다.

public class PlayerMove : MonoBehaviour
{
    //속성 : 이동속도 
    public float speed = 10.0f;

    //속성2 : 중력 변수, 수징 속력 변수, 점프력
    private CharacterController controller;
    private float gravity = -10.0f;
    private float yVelocity = 0;
    public float jumpPower = 5.0f;

    //속성3 : 점프 확인 변수
    public bool isJumping = false;

    //속성4 : HP
    public int healthPoint = 10;

    //속성5 : maxHP, HP슬라이더
    public int maxHealthPoint = 10;
    public Slider healthPointSlider;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        //순서1-1. 사용자의 입력을 받는다.
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        //순서3-1. 점프 중 착지 했을 때 점프 상태를 false로 설정한다.
        if (isJumping && controller.collisionFlags == CollisionFlags.Below)
        {
            isJumping = false;
        }
        //순서3-2. 바닥에 있을 경우 수직속도를 0으로 한다.
        else if (controller.collisionFlags == CollisionFlags.Below)
        {
            yVelocity = 0;
        }
        //순서2-3. Space를 누르면 점프력에 따라 수직으로 이동한다.
        //순서3-3. 점프를 했을 때 점프상태를 확인해서 점프 중이면 동작하지 않도록 한다.
        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            yVelocity = jumpPower;
            //순서3-4. 점프를 했을 때 점프 상태를 true로 설정한다.
            isJumping = true;
        }


        //순서1-2. 이동방향을 설정한다.
        Vector3 direction = Camera.main.transform.TransformDirection(h, 0, v);

        //순서1-3. 이동속도에 따라 이동방향으로 플레이어를 이동시킨다.
        //transform.position += speed * Time.deltaTime * direction;



        //순서2-1. 캐릭터의 수직 속도에 중력을 적용한다.
        yVelocity += gravity * Time.deltaTime;
        direction.y = yVelocity;

        //순서2-2. 캐릭터 컨트롤러로 나를 이동시키고 싶다.
        controller.Move(speed * Time.deltaTime * direction);

        //순서5-1. HP를 슬라이더에 적용한다.
        healthPointSlider.value = (float)healthPoint / (float)maxHealthPoint;
    }

    public void GetDamage(int damage)
    {
        healthPoint -= damage;
    }
}
