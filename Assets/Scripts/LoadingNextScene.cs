using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


//목적1 : 다음 씬을 비동기 방식으로 로드하고 싶다.
//속성1 : 다음 씬의 번호

//목적2 : 현재 씬의 로딩 진행률을 슬라이더를 통해서 표현하고 싶다.
//속성2 : 로딩 슬라이더, 로딩 텍스트
//순서2-1. 씬의 로딩 진행률을 슬라이더와 텍스트로 표현한다.
//순서2-2. 씬의 진행률이 90% 이상일 때 로딩중인 씬을 표시한다.

public class LoadingNextScene : MonoBehaviour
{
    //속성1 : 다음 씬의 번호
    public int sceneNumber = 2;

    //속성2 : 로딩 슬라이더, 로딩 텍스트
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
            //순서2-1. 씬의 로딩 진행률을 슬라이더와 텍스트로 표현한다.
            loadingSlider.value = asyncOperation.progress;
            loadingText.text = (asyncOperation.progress * 100).ToString() + "%";

            //순서2-2. 씬의 진행률이 90% 이상일 때 로딩중인 씬을 표시한다.
            if(asyncOperation.progress >= 0.9f)
            {
                asyncOperation.allowSceneActivation = true;
            }
            yield return new WaitForSeconds(3f);
        }
    }
}
