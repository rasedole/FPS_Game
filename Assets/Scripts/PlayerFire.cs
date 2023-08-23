using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//목적1 : 마우스 우클릭을 누르면 폭탄을 특정방향으로 발사한다.
//속성1 : 폭탄 게임오브젝트, 발사 위치, 방향, 힘
//순서1-1. 마우스 우클릭을 누른다.
//순서1-2. 폭탄 오브젝트를 생성한다.
//순서1-3. 폭탄 오브젝트의 발사 위치를 설정한다.
//순서1-4. 폭탄 오브젝트를 카메라방향으로 힘에 비례해서 발사한다.

//목적2 : 마우스 좌클릭을 누르면 보고있는 방향으로 총을 발사한다.
//속성2 : 레이, 피격효과 게임오브젝트, 이펙트의 파티클 시스템
//순서2-1. 마우스 좌클릭을 누른다.
//순서2-2. 레이를 생성하고 발사 위치와 방향을 설정한다.
//순서2-3. 레이가 부딫힌 대상의 정보를 저장할 수 있는 변수를 만든다.
//순서2-4. 레이와 부딪힌 물체가 있으면 그 위치에 피격 효과를 만든다.

//목적3 : 레이가 부딫힌 대상이 에네미라면 에네미에게 데미지를 준다.
//속성3 : 공격력
//순서3-1. 레이가 에네미와 부딫힌다.
//순서3-2. 에네미에게 데미지를 준다.

//목적4 : Ready, GameOver 상태일 때 플레이어가 움직일 수 없도록 한다.

//목적5 : 휠클릭으로 조준 기능을 추가한다.
//속성5 : 조준UI, 플레이어 UI, 조준 카메라, 줌 bool 변수, 메인 카메라
//순서5-1. 휠클릭을 누른다.
//순서5-2. 줌 변수를 true로 한다.
//순서5-3. 조준UI를 활성화한다.
//순서5-4. 그 외의 UI를 비활성화 한다.
//순서5-5. 조준 카메라를 활성화하고 메인 카메라를 비활성화한다.
//순서5-6. 다시 휠클릭을 누른다.
//순서5-7. 줌 변수를 false로 한다.
//순서5-8. 조준 UI를 비활성화한다.
//순서5-9. 그 외의 UI를 활성화한다.
//순서5-10. 조준 카메라를 비활성화하고 메인 카메라를 활성화한다.


public class PlayerFire : MonoBehaviour
{
    //속성1 : 폭탄 게임오브젝트, 발사 위치, 방향, 힘
    public GameObject bomb;
    public GameObject firePosition;
    protected Vector3 direction;
    public float power = 10f;

    //속성2 : 레이, 피격효과 게임오브젝트, 이펙트의 파티클 시스템
    protected Ray ray;
    public GameObject hitEffect;
    protected ParticleSystem particle;

    //속성3 : 공격력
    public int weaponPower = 2;

    private void Start()
    {
        particle = hitEffect.GetComponent<ParticleSystem>();
    }

    //속성5 : 조준UI, 플레이어 UI, 조준 카메라, 줌 bool 변수, 메인 카메라
    public GameObject zoomUI;
    public GameObject playerUI1;
    public GameObject playerUI2;
    public GameObject zoomCamera;
    public bool zoom = false;
    public GameObject mainCamera;

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.state != GameManager.GameState.Start)
        {
            return;
        }

        //순서1-1. 마우스 우클릭을 누른다.
        if (Input.GetMouseButtonDown(1))
        {
            //순서1-2. 폭탄 오브젝트를 생성한다.
            GameObject bombGO = Instantiate(bomb);

            //순서1-3. 폭탄 오브젝트의 발사 위치를 설정한다.
            bombGO.transform.position = firePosition.transform.position;

            //순서1-4. 폭탄 오브젝트를 카메라방향으로 힘에 비례해서 발사한다.
            Rigidbody rigidbody = bombGO.GetComponent<Rigidbody>();
            if(Camera.main != null)
            {
                direction = Camera.main.transform.forward;
            }
            else
            {
                direction = zoomCamera.transform.forward;
            }
            rigidbody.AddForce(direction * power, ForceMode.Impulse);
        }

        //순서2-1. 마우스 좌클릭을 누른다.
        if (Input.GetMouseButtonDown(0))
        {
            //순서2-2. 레이를 생성하고 발사 위치와 방향을 설정한다.
            if (Camera.main != null)
            {
                ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            }
            else
            {
                ray = new Ray(zoomCamera.transform.position, zoomCamera.transform.forward);
            }

            //순서2-3. 레이가 부딫힌 대상의 정보를 저장할 수 있는 변수를 만든다.
            RaycastHit hitInfo = new RaycastHit();

            //순서2-4. 레이와 부딪힌 물체가 있으면 그 위치에 피격 효과를 만든다.
            if (Physics.Raycast(ray, out hitInfo))
            {
                hitEffect.transform.position = hitInfo.point;
                hitEffect.transform.forward = hitInfo.normal;
                particle.Play();

                //순서3-1. 레이가 에네미와 부딫힌다.
                if (hitInfo.transform.gameObject.layer == LayerMask.NameToLayer("Enemy"))
                {
                    //순서3-2. 에네미에게 데미지를 준다.
                    hitInfo.transform.gameObject.GetComponent<EnemyFSM>().GetDamaged(weaponPower);
                }
            }
        }

        //순서5-1. 휠클릭을 누른다.
        if (Input.GetMouseButtonDown(2))
        {
            if (!zoom)
            {
                //순서5-2. 줌 변수를 true로 한다.
                zoom = true;

                //순서5-4. 그 외의 UI를 비활성화 한다.
                playerUI1.SetActive(false);
                playerUI2.SetActive(false);
                //순서5-3. 조준UI를 활성화한다.
                zoomUI.SetActive(true);
                //순서5-5. 조준 카메라를 활성화하고 메인 카메라를 비활성화한다.
                mainCamera.SetActive(false);
                zoomCamera.SetActive(true);

            }
            //순서5-6. 다시 휠클릭을 누른다.
            else
            {
                //순서5-7. 줌 변수를 false로 한다.
                zoom = false;
                //순서5-9. 그 외의 UI를 활성화한다.
                playerUI1.SetActive(true);
                playerUI2.SetActive(true);
                //순서5-8. 조준 UI를 비활성화한다.
                zoomUI.SetActive(false);
                //순서5-10. 조준 카메라를 비활성화하고 메인 카메라를 활성화한다.
                zoomCamera.SetActive(false);
                mainCamera.SetActive(true);
            }
        }
    }
}
