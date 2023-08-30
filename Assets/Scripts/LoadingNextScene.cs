using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


//����1 : ���� ���� �񵿱� ������� �ε��ϰ� �ʹ�.
//�Ӽ�1 : ���� ���� ��ȣ

//����2 : ���� ���� �ε� ������� �����̴��� ���ؼ� ǥ���ϰ� �ʹ�.
//�Ӽ�2 : �ε� �����̴�, �ε� �ؽ�Ʈ
//����2-1. ���� �ε� ������� �����̴��� �ؽ�Ʈ�� ǥ���Ѵ�.
//����2-2. ���� ������� 90% �̻��� �� �ε����� ���� ǥ���Ѵ�.

public class LoadingNextScene : MonoBehaviour
{
    //�Ӽ�1 : ���� ���� ��ȣ
    public int sceneNumber = 2;

    //�Ӽ�2 : �ε� �����̴�, �ε� �ؽ�Ʈ
    public Slider loadingSlider;
    public TMP_Text loadingText;

    private void Start()
    {
        StartCoroutine(ASyncNextScene(sceneNumber));
    }

    IEnumerator ASyncNextScene(int num)
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(num);

        asyncOperation.allowSceneActivation = false;

        while(!asyncOperation.isDone)
        {
            //����2-1. ���� �ε� ������� �����̴��� �ؽ�Ʈ�� ǥ���Ѵ�.
            loadingSlider.value = asyncOperation.progress;
            loadingText.text = (asyncOperation.progress * 100).ToString() + "%";

            //����2-2. ���� ������� 90% �̻��� �� �ε����� ���� ǥ���Ѵ�.
            if(asyncOperation.progress >= 0.9f)
            {
                asyncOperation.allowSceneActivation = true;
            }
            yield return new WaitForSeconds(3f);
        }
    }
}
