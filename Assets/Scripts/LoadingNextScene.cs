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
    public string sceneName = "FPSGame";

    //�Ӽ�2 : �ε� �����̴�, �ε� �ؽ�Ʈ
    public Slider loadingSlider;
    public TMP_Text loadingText;

    private void Start()
    {
        StartCoroutine(ASyncNextScene(sceneName));
    }

    IEnumerator ASyncNextScene(string name)
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(name);

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

                MainGameManager.Instance.StartTimer();
            }
            yield return new WaitForSeconds(3f);
        }
    }
}
