using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//목적1 : 아이템과 플레이어가 만나면 사라진다

public class ItemAction : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        ItemManager.Instance.isItem = false;
        Destroy(this.gameObject);
    }
}
