using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

//목적1 : 플레이어가 버튼을 누르면 다리가 켜지고, 네비게이션 매시를 다시 만든다.
//속성1 : 다리 게임 오브젝트, 네비게이션 navMeshSurface
//순서1-1.  

public class ButtonScript : MonoBehaviour
{
    //속성1 : 다리 게임 오브젝트, 네비게이션 navMeshSurface
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
