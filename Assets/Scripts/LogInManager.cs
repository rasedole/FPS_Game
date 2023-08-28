using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

//����1 : ������� ID�� PW�� �Է��Ͽ� �����ϰų�(ȸ������) ����� �����͸� �о ��ġ ���ο� ���� �α��� �Ѵ�.
//�Ӽ�1 : ID InputField, PW InputField, �����ؽ�Ʈ
//����1-1. ����ڰ� �α��� ��ư�� ������ �� �Էµ� ID, PW�� ����� ID, PW�� ���Ѵ�.
//����1-2. ����� ID, PW�� ��ġ�� ��� �α��� �Ѵ�.
//����1-3. ��ġ���� ���� ��� �����ؽ�Ʈ�� ���������� �˸���.
//����1-4. ����ڰ� ȸ������ ��ư�� ������ �� ��ġ�ϴ� ID�� ���� ��� �Էµ� ID�� PW�� �����Ѵ�.
//����1-5. ��ġ�ϴ� ID�� ���� ��� ȸ�������� ���������� �˸���.


public class LogInManager : MonoBehaviour
{
    //�Ӽ�1 : ID InputField, PW InputField, �����ؽ�Ʈ
    public TMP_InputField id;
    public TMP_InputField password;
    public TMP_Text authText;

    // Start is called before the first frame update
    void Start()
    {
        authText.text = string.Empty;
    }

    //����1-1. ����ڰ� �α��� ��ư�� ������ �� �Էµ� ID, PW�� ����� ID, PW�� ���Ѵ�.
    public void LogIn()
    {
        if(id.text == string.Empty || password.text == string.Empty)
        {
            authText.text = "Please Input ID or Password";
            return;
        }
        //����1-2. ����� ID, PW�� ��ġ�� ��� �α��� �Ѵ�.
        if (PlayerPrefs.HasKey(id.text))
        {
            if(PlayerPrefs.GetString(id.text) == password.text)
            {
                SceneManager.LoadScene(1);
                authText.text = "Log In Sucessful!";
                return;
            }
        }
        //����1-3. ��ġ���� ���� ��� �����ؽ�Ʈ�� ���������� �˸���.
        authText.text = "Please Check ID & Password";
    }

    public void SignUp()
    {
        if (id.text == string.Empty || password.text == string.Empty)
        {
            authText.text = "Please Input ID or Password";
            return;
        }

        //����1-4. ����ڰ� ȸ������ ��ư�� ������ �� �Էµ� ID�� PW�� �����Ѵ�.
        if (!PlayerPrefs.HasKey(id.text))
        {
            PlayerPrefs.SetString(id.text, password.text);
            authText.text = "Sign Up Succesfully!";
        }
        //����1-5. ��ġ�ϴ� ID�� ���� ��� ȸ�������� ���������� �˸���.
        else
        {
            authText.text = "Already exist ID";
        }
    }
}
