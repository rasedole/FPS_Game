using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

//목적1 : 사용자의 ID와 PW를 입력하여 저장하거나(회원가입) 저장된 데이터를 읽어서 일치 여부에 따라 로그인 한다.
//속성1 : ID InputField, PW InputField, 인증텍스트
//순서1-1. 사용자가 로그인 버튼을 눌렀을 때 입력된 ID, PW를 저장된 ID, PW와 비교한다.
//순서1-2. 저장된 ID, PW와 일치할 경우 로그인 한다.
//순서1-3. 일치하지 않을 경우 인증텍스트에 실패했음을 알린다.
//순서1-4. 사용자가 회원가입 버튼을 눌렀을 때 일치하는 ID가 없을 경우 입력된 ID와 PW를 저장한다.
//순서1-5. 일치하는 ID가 있을 경우 회원가입이 실패했음을 알린다.


public class LogInManager : MonoBehaviour
{
    //속성1 : ID InputField, PW InputField, 인증텍스트
    public TMP_InputField id;
    public TMP_InputField password;
    public TMP_Text authText;

    // Start is called before the first frame update
    void Start()
    {
        authText.text = string.Empty;
    }

    //순서1-1. 사용자가 로그인 버튼을 눌렀을 때 입력된 ID, PW를 저장된 ID, PW와 비교한다.
    public void LogIn()
    {
        if(id.text == string.Empty || password.text == string.Empty)
        {
            authText.text = "Please Input ID or Password";
            return;
        }
        //순서1-2. 저장된 ID, PW와 일치할 경우 로그인 한다.
        if (PlayerPrefs.HasKey(id.text))
        {
            if(PlayerPrefs.GetString(id.text) == password.text)
            {
                SceneManager.LoadScene(1);
                authText.text = "Log In Sucessful!";
                return;
            }
        }
        //순서1-3. 일치하지 않을 경우 인증텍스트에 실패했음을 알린다.
        authText.text = "Please Check ID & Password";
    }

    public void SignUp()
    {
        if (id.text == string.Empty || password.text == string.Empty)
        {
            authText.text = "Please Input ID or Password";
            return;
        }

        //순서1-4. 사용자가 회원가입 버튼을 눌렀을 때 입력된 ID와 PW를 저장한다.
        if (!PlayerPrefs.HasKey(id.text))
        {
            PlayerPrefs.SetString(id.text, password.text);
            authText.text = "Sign Up Succesfully!";
        }
        //순서1-5. 일치하는 ID가 있을 경우 회원가입이 실패했음을 알린다.
        else
        {
            authText.text = "Already exist ID";
        }
    }
}
