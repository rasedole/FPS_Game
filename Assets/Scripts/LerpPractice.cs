using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//����1 : ���������� �������� �����ð��ȿ� �̵��Ѵ�.
//�Ӽ�1 : ������ ��ġ, ���� ��ġ, �����ð�, ����ð�

public class LerpPractice : MonoBehaviour
{
    //�Ӽ�1 : ������ ��ġ, ���� ��ġ, �����ð�
    public Transform startPoint;
    public Transform endPoint;
    public float moveTime = 3;
    private float currentTime = 0;
    private bool moveLeftToRight = true;

    // Update is called once per frame
    void Update()
    {
        if (moveLeftToRight)
        {
            currentTime += Time.deltaTime;
        }
        else
        {
            currentTime -= Time.deltaTime;
        }

        if ((transform.position - endPoint.position).magnitude < 0.2f)
        {
            moveLeftToRight = false;
        }
        else if ((transform.position - startPoint.position).magnitude < 0.2f)
        {
            moveLeftToRight = true;
        }

        transform.position = Vector3.Lerp(startPoint.position, endPoint.position, currentTime / moveTime);

    }
}
