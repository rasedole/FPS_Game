using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

//목적1 : 마우스 기능키를 누르면 폭탄을 특정방향으로 발사한다.
//속성1 : 폭탄 게임오브젝트, 발사 위치, 방향, 힘
//순서1-1. 마우스 기능키를 누른다.
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

//목적5 : 우클릭으로 조준 기능을 추가한다.
//속성5 : 조준UI, 플레이어 UI, 조준 카메라, 줌 bool 변수, 메인 카메라
//순서5-1. 우클릭을 누른다.
//순서5-2. 줌 변수를 true로 한다.
//순서5-3. 조준UI를 활성화한다.
//순서5-4. 그 외의 UI를 비활성화 한다.
//순서5-5. 조준 카메라를 활성화하고 메인 카메라를 비활성화한다.
//순서5-6. 다시 우클릭을 누른다.
//순서5-7. 줌 변수를 false로 한다.
//순서5-8. 조준 UI를 비활성화한다.
//순서5-9. 그 외의 UI를 활성화한다.
//순서5-10. 조준 카메라를 비활성화하고 메인 카메라를 활성화한다.

//목적6 : 총을 쏠 때 마다 총알이 줄어들고 현재 총알이 0발이거나 R키를 누르면 재장전을 한다.
//속성6 : 현재총알, 모든총알, 총알텍스트UI, 장전확인 bool변수
//순서6-1. 총을 쏘면 총알이 줄어든다.
//순서6-2. 현재총알이 0발일 경우 총을 쏘는 대신에 재장전을 한다.
//순서6-3. 재장전이 끝나면 모든총알에서 30발만큼 줄어들고 현재총알이 30발이 된다.

//목적7 : 이동 블랜드 트리의 파라메터 값이 0일 때, Attack Trigger를 시전하겠다.
//속성7 : 자식 오브젝트의 애니메이터

//목적8 : 재장전 중 재장전 UI를 표시한다.
//속성8 : 재장전UI

//목적9 : 키보드의 특정 키 입력으로 무기모드를 전환하고 싶다.
//속성9 : 무기모드 열거형 변수
//순서9-1. 노멀모드일 때 우클릭을 누르면 수류탄을 던진다.
//순서9-2. 스나이퍼모드일 때 우클릭을 누르면 줌을 한다.
//순서9-3. 1번을 누르면 노멀모드, 2번을 누르면 스나이퍼모드로 설정한다.

//목적10 : 총을 쏠 때 마다 일정시간이 지나면 사라지는 이펙트를 랜덤하게 발생시킨다.
//속성10 : 이펙트 게임오브젝트
//순서10-1. 총을 발사한다.
//순서10-2. 랜덤하게 이펙트를 발생시킨다.

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
    public int weaponPower = 3;

    //속성5 : 조준UI, 플레이어 UI, 조준 카메라, 줌 bool 변수, 메인 카메라
    //public GameObject zoomUI;
    public GameObject playerUI1;
    public GameObject playerUI2;
    //public GameObject zoomCamera;
    public bool zoom = false;
    public GameObject mainCamera;

    //속성6 : 현재총알, 모든총알, 총알텍스트UI, 장전확인 bool변수
    public int currentBullet = 30;
    public int maxBullet = 300;
    public TMP_Text bulletText;
    public bool isReloading = false;

    //속성7 : 자식 오브젝트의 애니메이터
    private Animator animator;

    //속성8 : 재장전UI
    public TMP_Text reloadUI;

    //속성9 : 무기모드 열거형 변수
    public enum WeaponMode
    {
        Normal,
        Sniper
    }
    WeaponMode weaponMode = WeaponMode.Normal;

    //속성10 : 이펙트 게임오브젝트
    public GameObject[] fireFlashEffect;


    public TMP_Text weaponModeText;

    private void Start()
    {
        particle = hitEffect.GetComponent<ParticleSystem>();
        bulletText.text = currentBullet.ToString() + "/" + maxBullet.ToString();
        animator = GetComponentInChildren<Animator>();
        weaponModeText.text = "Normal";
        //zoomCamera.GetComponent<CamRotate>().rotateSpeed = mainCamera.GetComponent<CamRotate>().rotateSpeed / 4;
    }


    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.state != GameManager.GameState.Start)
        {
            return;
        }

        //순서1-1. 마우스 기능키를 누른다.
        if (Input.GetMouseButtonDown(1))
        {
            switch (weaponMode)
            {
                //순서9-1. 노멀모드일 때 우클릭을 누르면 수류탄을 던진다.
                case WeaponMode.Normal:
                    //순서1-2. 폭탄 오브젝트를 생성한다.
                    GameObject bombGO = Instantiate(bomb);

                    //순서1-3. 폭탄 오브젝트의 발사 위치를 설정한다.
                    bombGO.transform.position = firePosition.transform.position;

                    //순서1-4. 폭탄 오브젝트를 카메라방향으로 힘에 비례해서 발사한다.
                    Rigidbody rigidbody = bombGO.GetComponent<Rigidbody>();
                    //if (Camera.main != null)
                    {
                        direction = Camera.main.transform.forward;
                    }
                    //else
                    //{
                    //    direction = zoomCamera.transform.forward;
                    //}
                    rigidbody.AddForce(direction * power, ForceMode.Impulse);
                    break;

                //순서9-2. 스나이퍼모드일 때 우클릭을 누르면 줌을 한다.
                case WeaponMode.Sniper:
                    //순서5-1. 우클릭을 누른다.
                    if (Input.GetMouseButtonDown(1))
                    {
                        if (!zoom)
                        {
                            //순서5-2. 줌 변수를 true로 한다.
                            zoom = true;

                            //순서5-4. 그 외의 UI를 비활성화 한다.
                            //playerUI1.SetActive(false);
                            playerUI2.SetActive(false);

                            Camera.main.fieldOfView = 15;

                            gameObject.GetComponent<PlayerRotate>().rotateSpeed = mainCamera.GetComponent<CamRotate>().rotateSpeed /= 4;

                            ////순서5-3. 조준UI를 활성화한다.
                            //zoomUI.SetActive(true);

                            ////순서5-5. 조준 카메라를 활성화하고 메인 카메라를 비활성화한다.
                            //zoomCamera.SetActive(true);
                            //zoomCamera.transform.forward = mainCamera.transform.forward;
                            //zoomCamera.GetComponent<CamRotate>().mX = mainCamera.GetComponent<CamRotate>().mX;
                            //zoomCamera.GetComponent<CamRotate>().mY = mainCamera.GetComponent<CamRotate>().mY;
                            //mainCamera.SetActive(false);

                            //gameObject.GetComponent<PlayerRotate>().rotateSpeed = zoomCamera.GetComponent<CamRotate>().rotateSpeed;
                        }
                        //순서5-6. 다시 우클릭을 누른다.
                        else
                        {
                            //순서5-7. 줌 변수를 false로 한다.
                            zoom = false;
                            //순서5-9. 그 외의 UI를 활성화한다.
                            //playerUI1.SetActive(true);
                            playerUI2.SetActive(true);

                            Camera.main.fieldOfView = 60;

                            gameObject.GetComponent<PlayerRotate>().rotateSpeed = mainCamera.GetComponent<CamRotate>().rotateSpeed *= 4;

                            ////순서5-8. 조준 UI를 비활성화한다.
                            //zoomUI.SetActive(false);

                            ////순서5-10. 조준 카메라를 비활성화하고 메인 카메라를 활성화한다.
                            //mainCamera.SetActive(true);
                            //mainCamera.transform.forward = zoomCamera.transform.forward;
                            //mainCamera.GetComponent<CamRotate>().mX = zoomCamera.GetComponent<CamRotate>().mX;
                            //mainCamera.GetComponent<CamRotate>().mY = zoomCamera.GetComponent<CamRotate>().mY;
                            //zoomCamera.SetActive(false);

                            //gameObject.GetComponent<PlayerRotate>().rotateSpeed = mainCamera.GetComponent<CamRotate>().rotateSpeed;
                        }
                    }
                    break;
            }
        }

        //순서9-3. 1번을 누르면 노멀모드, 2번을 누르면 스나이퍼모드로 설정한다.
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            weaponModeText.text = "Normal";
            if (weaponMode == WeaponMode.Sniper)
            {
                weaponPower -= 10;
            }
            weaponMode = WeaponMode.Normal;
            if (zoom)
            {
                zoom = false;
                playerUI1.SetActive(true);
                playerUI2.SetActive(true);

                Camera.main.fieldOfView = 60;

                gameObject.GetComponent<PlayerRotate>().rotateSpeed = mainCamera.GetComponent<CamRotate>().rotateSpeed *= 4;
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)) 
        {
            weaponModeText.text = "Sniper";
            if (weaponMode == WeaponMode.Normal)
            {
                weaponPower += 10;
            }
            weaponMode = WeaponMode.Sniper;
        }


        //순서2-1. 마우스 좌클릭을 누른다.
        if (Input.GetMouseButtonDown(0))
        {
            if (currentBullet > 0)
            {
                if (animator.GetFloat("MoveMotion") == 0)
                {
                    animator.SetTrigger("Attack");
                }

                //순서10-1. 총을 발사한다.
                StartCoroutine(FireFlashEffect(0.05f));

                //순서6-1. 총을 쏘면 총알이 줄어든다.
                currentBullet--;

                //순서2-2. 레이를 생성하고 발사 위치와 방향을 설정한다.
                //if (Camera.main != null)
                //{
                ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
                //}
                //else
                //{
                //    ray = new Ray(zoomCamera.transform.position, zoomCamera.transform.forward);
                //}

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
            //순서6-2. 현재총알이 0발일 경우 총을 쏘는 대신에 재장전을 한다.
            else if (!isReloading)
            {
                StartCoroutine(ReloadBullet());
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (!isReloading)
            {
                StartCoroutine(ReloadBullet());
            }
        }


        bulletText.text = currentBullet.ToString() + "/" + maxBullet.ToString();
    }

    IEnumerator ReloadBullet()
    {
        isReloading = true;
        reloadUI.gameObject.SetActive(true);
        //순서6-3. 재장전이 끝나면 모든총알에서 30발만큼 줄어들고 현재총알이 30발이 된다.
        maxBullet += currentBullet;
        currentBullet = 0;
        yield return new WaitForSeconds(2f);
        if (maxBullet < 30)
        {
            currentBullet = maxBullet;
            maxBullet = 0;
        }
        else
        {
            maxBullet -= 30;
            currentBullet = 30;
        }
        isReloading = false;
        reloadUI.gameObject.SetActive(false);
    }

    IEnumerator FireFlashEffect(float duration)
    {
        //순서10-2. 랜덤하게 이펙트를 발생시킨다.
        int rand = Random.Range(0, fireFlashEffect.Length);
        fireFlashEffect[rand].SetActive(true);
        yield return new WaitForSeconds(duration);
        fireFlashEffect[rand].SetActive(false);
    }
}
