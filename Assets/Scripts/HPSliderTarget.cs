using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//����1 : HP �����̴��� Ÿ���� �� �������� ���ϰ� �Ѵ�.
//�Ӽ�1 : Ÿ��
public class HPSliderTarget : MonoBehaviour
{
    //�Ӽ�1 : Ÿ��
    public Transform target;

    // Update is called once per frame
    void Update()
    {
        transform.forward = target.transform.forward;
    }
}
