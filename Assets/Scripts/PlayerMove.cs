﻿using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
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

//목표6 : 적의 공격을 받았을 때 hitImage를 켰다가 꺼준다.
//속성6 : hitImage 게임오브젝트
//순서6-1. 적의 공격을 받는다.
//순서6-2. hitImage를 True로 한다.

//목표7 : 적의 공격을 받아 죽으면 화면을 빨간색으로 만든다.
//속성7 : 현재시간, 특정시간
//순서7-1. 적의 공격을 받아 죽는다.
//순서7-2. hitImage의 알파값을 255로 만든다.

//목적8 : Ready, GameOver 상태일 때 플레이어가 움직일 수 없도록 한다.

//목적9 : Zoom 진행중 줌 카메라를 기준으로 이동한다.
//속성9 : Zoom카메라

//목적10 : 플레이어의 자식 중 모델링 오브젝트에 있는 애니메이터 컴포넌트를 가져와서 블랜딩 트리를 호출하고 싶다.
//속성10 : 모델링 오브젝트의 애니메이터



public class PlayerMove : MonoBehaviour, IPunObservable
{
    //속성 : 이동속도 
    public float speed = 10.0f;

    //속성2 : 중력 변수, 수징 속력 변수, 점프력
    private CharacterController controller;
    private float gravity = -10.0f;
    private float yVelocity = 0;
    public float jumpPower = 3f;

    //속성3 : 점프 확인 변수
    public bool isJumping = false;

    //속성4 : HP
    public int healthPoint = 50;

    //속성5 : maxHP, HP슬라이더
    private int maxHealthPoint;
    public Slider healthPointSlider;

    //속성6 : hitImage 게임오브젝트
    public GameObject hitImage;

    //속성7 : 현재시간, 특정시간
    private float currentTime = 0;
    public float endTime = 3f;

    //속성9 : Zoom카메라
    public GameObject zoomCamera;
    Vector3 direction;

    //속성10 : 모델링 오브젝트의 애니메이터
    Animator animator;

    PhotonView photonView;

    private void Start()
    {
        controller = GetComponent<CharacterController>();

        maxHealthPoint = healthPoint;

        animator = GetComponentInChildren<Animator>();

        photonView = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            //순서5-1. HP를 슬라이더에 적용한다.
            healthPointSlider.value = (float)healthPoint / (float)maxHealthPoint;

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                GameManager.Instance.OpenOptionWindow();
            }

            if (GameManager.Instance.state != GameManager.GameState.Start)
            {
                return;
            }

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
            //if(Camera.main != null)
            {
                direction = Camera.main.transform.TransformDirection(h, 0, v);

                //속성10 : 모델링 오브젝트의 애니메이터
                animator.SetFloat("MoveMotion", direction.magnitude);
            }
            //else
            //{
            //    direction = zoomCamera.transform.TransformDirection(h, 0, v);

            //    //속성10 : 모델링 오브젝트의 애니메이터
            //    animator.SetFloat("MoveMotion", direction.magnitude);
            //}

            //순서1-3. 이동속도에 따라 이동방향으로 플레이어를 이동시킨다.
            //transform.position += speed * Time.deltaTime * direction;



            //순서2-1. 캐릭터의 수직 속도에 중력을 적용한다.
            yVelocity += gravity * Time.deltaTime;
            direction.y = yVelocity;

            //순서2-2. 캐릭터 컨트롤러로 나를 이동시키고 싶다.
            controller.Move(speed * Time.deltaTime * direction);

        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, receivedPos, Time.deltaTime * 20);
            transform.rotation = Quaternion.Lerp(transform.rotation, receivedRot, Time.deltaTime * 20);
        }
    }

    //순서6-1. 적의 공격을 받는다.
    public void GetDamage(int damage)
    {
        if(healthPoint > 0)
        {
            healthPoint -= damage;

            if (healthPoint > 0)
            {
                //순서6-2. hitImage를 True로 한다.
                StartCoroutine(HitEffect());
            }

            //순서7-1. 적의 공격을 받아 죽는다.
            else
            {
                //순서7-2. hitImage의 알파값을 255로 만든다.
                StartCoroutine(DeadEffect());
            }
        }
    }

    IEnumerator HitEffect()
    {
        hitImage.SetActive(true);
        yield return new WaitForSeconds(0.15f);
        hitImage.SetActive(false);
    }

    IEnumerator DeadEffect()
    {
        hitImage.SetActive(true);
        Color hitImageColor = hitImage.GetComponent<Image>().color;

        while (true)
        {
            currentTime += Time.deltaTime;

            yield return null;
            
            hitImageColor.a = Mathf.Lerp(100/255, 1, currentTime / endTime);

            hitImage.GetComponent<Image>().color = hitImageColor;

            if(currentTime > endTime)
            {
                currentTime = 0;
                break;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag is "Item")
        {
            healthPoint += (int)((float)maxHealthPoint * 0.25);
            Mathf.Clamp(healthPoint, 0, maxHealthPoint);
            GetComponent<PlayerFire>().maxBullet += 100;
        }
    }

    Vector3 receivedPos;
    Quaternion receivedRot;

    // 목적 : 네트워크의 동기화를 위해 PhotonStream(플레이어의 position, rotation)을 보내고 받는다.
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        //데이터 전송
        if(stream.IsWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
        //데이터 수신
        else if(stream.IsReading)
        {
            receivedPos = (Vector3)stream.ReceiveNext();
            receivedRot = (Quaternion)stream.ReceiveNext();
        }
    }
}
