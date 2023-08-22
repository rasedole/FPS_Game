using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

//����1 : ������ ����(Ready, Start, GameOver)�� �����ϰ�, ������ ���۰� ���� Text UI�� ǥ���Ѵ�.
//�Ӽ�1 : TMPro, Text UI, ���� ���� ����Ʈ, ���� ���� ����

//����2 : ������ ���۵� �� 3�ʰ� ������ Start���·� �����Ѵ�.
//����2-1. ������ ���۵ȴ�.
//����2-2. 3�� ī��Ʈ�ٿ��� �����Ѵ�.
//����2-3. Start���·� �����Ѵ�.
//����2-4. Start text�� ����Ѵ�.
//����2-5. Text�� �� ���̰� �Ѵ�.

//����3 : �÷��̾��� HP�� 0 ���ϰ� �Ǹ� GameOver ���·� �����Ѵ�.
//�Ӽ�3 : �÷��̾� HP�� ����ִ� PlayerMove
//����3-1. �÷��̾��� HP�� 0 ���ϰ� �ȴ�.
//����3-2. ���¸� GameOver���·� �����Ѵ�.
//����3-3. Text�� GAME OVER�� �����ϰ� ǥ���Ѵ�.

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    //�Ӽ�1 : TMPro, Text UI, ���� ���� ����Ʈ, ���� ���� ����
    public TMP_Text stateTextUI;
    public enum GameState
    {
        Ready,
        Start,
        GameOver
    }
    public GameState state = GameState.Ready;

    //�Ӽ�3 : �÷��̾� HP�� ����ִ� PlayerMove
    PlayerMove playerHP;

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
        //���� �� UI �ؽ�Ʈ�� READY��, ���� ��Ȳ������ ����
        stateTextUI.text = "READY";
        stateTextUI.color = new Color32(255, 150, 0, 255);

        //����2-1. ������ ���۵ȴ�.
        StartCoroutine(CountDownText());

        playerHP = GameObject.Find("Player").GetComponent<PlayerMove>();
    }

    IEnumerator CountDownText()
    {
        //����2-2. 3�� ī��Ʈ�ٿ��� �����Ѵ�.
        yield return new WaitForSeconds(2);
        stateTextUI.text = "3";
        yield return new WaitForSeconds(1);
        stateTextUI.text = "2";
        yield return new WaitForSeconds(1);
        stateTextUI.text = "1";
        yield return new WaitForSeconds(1);
        //����2-3. Start���·� �����Ѵ�.
        state = GameState.Start;

        //����2-4. Start text�� ����Ѵ�.
        stateTextUI.text = "START";
        stateTextUI.color = new Color32(0, 255, 0, 255);

        //����2-5. Text�� �� ���̰� �Ѵ�.
        yield return new WaitForSeconds(0.5f);
        stateTextUI.gameObject.SetActive(false);
    }

    void GameOver()
    {
        //����3-2. ���¸� GameOver���·� �����Ѵ�.
        state = GameState.GameOver;
        //����3-3. Text�� GAME OVER�� �����ϰ� ǥ���Ѵ�.
        stateTextUI.text = "GAME OVER";
        stateTextUI.color = new Color32(255, 255, 255, 255);
        stateTextUI.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        //����3-1. �÷��̾��� HP�� 0 ���ϰ� �ȴ�.
        if (playerHP.healthPoint <= 0)
        {
            GameOver();
        }
    }
}
