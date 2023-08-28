using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

//목적1 : 게임의 상태(Ready, Start, GameOver)를 구별하고, 게임의 시작과 끝을 Text UI로 표현한다.
//속성1 : TMPro, Text UI, 게임 상태 리스트, 현재 게임 상태

//목적2 : 게임이 시작된 후 3초가 지나면 Start상태로 변경한다.
//순서2-1. 게임이 시작된다.
//순서2-2. 3초 카운트다운을 시작한다.
//순서2-3. Start상태로 변경한다.
//순서2-4. Start text를 출력한다.
//순서2-5. Text를 안 보이게 한다.

//목적3 : 플레이어의 HP가 0 이하가 되면 GameOver 상태로 변경한다.
//속성3 : 플레이어 HP가 담겨있는 PlayerMove
//순서3-1. 플레이어의 HP가 0 이하가 된다.
//순서3-2. 상태를 GameOver상태로 변경한다.
//순서3-3. Text를 GAME OVER로 변경하고 표시한다.

//목적4 : 플레이어의 HP가 0 이하가 되면 플레이어의 애니메이션을 멈춘다.
//속성4 : 플레이어의 애니메이터 컴포넌트

//목적5 : Setting 버튼을 누르면 Option UI가 켜지고 게임 속도를 조절한다(0 or 1).
//속성5 : Option UI 게임오브젝트, 일시정지 상태, 정지 전 상태
//순서5-1. Option 버튼을 누르면 게임을 정지시키고 옵션 화면을 표시한다.
//순서5-2. Resume 버튼을 누르면 게임의 정지를 해제하고 옵션 화면을 지운다.
//순서5-3. Restart 버튼을 누르면 게임을 초기 상태로 되돌리고 옵션 화면을 지운다.
//순서5-4. Exit 버튼을 누르면 게임을 종료한다.

//목적6 : 게임 오버시 Restart와 Exit 버튼을 활성화 한다.
//순서6-1. 게임 오버 당한다.
//순서6-2. 버튼들을 활성화 한다.


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    //속성1 : TMPro, Text UI, 게임 상태 리스트, 현재 게임 상태
    public TMP_Text stateTextUI;
    public enum GameState
    {
        Ready,
        Start,
        GameOver,
        Pause
    }
    public GameState state = GameState.Ready;

    //속성3 : 플레이어 HP가 담겨있는 PlayerMove
    PlayerMove playerHP;

    //속성4 : 플레이어의 애니메이터 컴포넌트
    private Animator animator;

    //속성5 : Option UI 게임오브젝트, 일시정지 상태, 정지 전 상태
    public GameObject optionUI;
    private GameState preState;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //시작 시 UI 텍스트를 READY로, 색을 주황색으로 변경
        stateTextUI.text = "READY";
        stateTextUI.color = new Color32(255, 150, 0, 255);

        //순서2-1. 게임이 시작된다.
        StartCoroutine(CountDownText());

        playerHP = GameObject.Find("Player").GetComponent<PlayerMove>();

        animator = GameObject.Find("Player").GetComponentInChildren<Animator>();
    }

    IEnumerator CountDownText()
    {
        //순서2-2. 3초 카운트다운을 시작한다.
        yield return new WaitForSeconds(2);
        stateTextUI.text = "3";
        yield return new WaitForSeconds(1);
        stateTextUI.text = "2";
        yield return new WaitForSeconds(1);
        stateTextUI.text = "1";
        yield return new WaitForSeconds(1);
        //순서2-3. Start상태로 변경한다.
        state = GameState.Start;

        //순서2-4. Start text를 출력한다.
        stateTextUI.text = "START";
        stateTextUI.color = new Color32(0, 255, 0, 255);

        //순서2-5. Text를 안 보이게 한다.
        yield return new WaitForSeconds(0.5f);
        stateTextUI.gameObject.SetActive(false);
    }

    void GameOver()
    {
        animator.SetFloat("MoveMotion", 0);

        //순서3-2. 상태를 GameOver상태로 변경한다.
        //순서6-1. 게임 오버 당한다.
        state = GameState.GameOver;

        //순서3-3. Text를 GAME OVER로 변경하고 표시한다.
        stateTextUI.text = "GAME OVER";
        stateTextUI.color = new Color32(255, 255, 255, 255);
        stateTextUI.gameObject.SetActive(true);

        //순서6-2. 버튼들을 활성화 한다.
        GameObject restartButton = stateTextUI.transform.GetChild(0).gameObject;
        GameObject exitButton = stateTextUI.transform.GetChild(1).gameObject;
        restartButton.SetActive(true);
        exitButton.SetActive(true);

        GameObject.Find("HP Bar").SetActive(false);
        GameObject.Find("Bullet (TMP)").SetActive(false);
        GameObject.Find("WeaponMode (TMP)").SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //순서3-1. 플레이어의 HP가 0 이하가 된다.
        if (playerHP.healthPoint <= 0)
        {
            GameOver();
        }
    }

    //순서5-1. Option 버튼을 누르면 게임을 정지시키고 옵션 화면을 표시한다.
    public void OpenOptionWindow()
    {
        optionUI.SetActive(true);

        Time.timeScale = 0f;

        if(state != GameState.Pause)
        {
            preState = state;
        }

        state = GameState.Pause;
    }

    //순서5-2. Resume 버튼을 누르면 게임의 정지를 해제하고 옵션 화면을 지운다.
    public void ResumeGame()
    {
        optionUI.SetActive(false);

        Time.timeScale = 1f;

        state = preState;
    }

    //순서5-3. Restart 버튼을 누르면 게임을 초기 상태로 되돌리고 옵션 화면을 지운다.
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        Time.timeScale = 1;
    }

    //순서5-4. Exit 버튼을 누르면 게임을 종료한다.
    public void ExitGame()
    {
        Application.Quit();
    }
}
