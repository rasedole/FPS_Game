using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

//����1 : �÷��̾ ��ư�� ������ �ٸ��� ������, �׺���̼� �Žø� �ٽ� �����.
//�Ӽ�1 : �ٸ� ���� ������Ʈ, �׺���̼� navMeshSurface
//����1-1.  

public class ButtonScript : MonoBehaviour
{
    //�Ӽ�1 : �ٸ� ���� ������Ʈ, �׺���̼� navMeshSurface
    public GameObject bridge;
    public GameObject environment;



    // Start is called before the first frame update
    void Start()
    {
        bridge.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            bridge.SetActive(true);
            NavMeshSurface[] navMeshSurface = environment.GetComponents<NavMeshSurface>();
            foreach (var nav in navMeshSurface)
            {
                nav.BuildNavMesh();
            }
        }
    }
}
