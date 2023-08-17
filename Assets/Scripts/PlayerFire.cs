using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//����1 : ���콺 ��Ŭ���� ������ ��ź�� Ư���������� �߻��Ѵ�.
//�Ӽ�1 : ��ź ���ӿ�����Ʈ, �߻� ��ġ, ����, ��
//����1-1. ���콺 ��Ŭ���� ������.
//����1-2. ��ź ������Ʈ�� �����Ѵ�.
//����1-3. ��ź ������Ʈ�� �߻� ��ġ�� �����Ѵ�.
//����1-4. ��ź ������Ʈ�� ī�޶�������� ���� ����ؼ� �߻��Ѵ�.

public class PlayerFire : MonoBehaviour
{
    //�Ӽ�1 : ��ź ���ӿ�����Ʈ, �߻� ��ġ, ����, ��
    public GameObject bomb;
    public GameObject firePosition;
    protected Vector3 direction;
    public float power = 10f;

    // Update is called once per frame
    void Update()
    {
        //����1-1. ���콺 ��Ŭ���� ������.
        if (Input.GetMouseButtonDown(1))
        {
            //����1-2. ��ź ������Ʈ�� �����Ѵ�.
            GameObject bombGO = Instantiate(bomb);

            //����1-3. ��ź ������Ʈ�� �߻� ��ġ�� �����Ѵ�.
            bombGO.transform.position = firePosition.transform.position;

            //����1-4. ��ź ������Ʈ�� ī�޶�������� ���� ����ؼ� �߻��Ѵ�.
            Rigidbody rigidbody = bombGO.GetComponent<Rigidbody>();
            direction = Camera.main.transform.forward;
            rigidbody.AddForce(direction * power, ForceMode.Impulse);
        }
    }
}
