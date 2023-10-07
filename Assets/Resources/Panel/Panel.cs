using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel : MonoBehaviour
{
    GunPlayer gPlayer;
    public bool isExistNest;
    private void Start()
    {
        gPlayer = GameObject.FindWithTag("GunPlayer").GetComponent<GunPlayer>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (null == gPlayer)
        {
            return;
        }
        if(collision.gameObject.tag == "GunPlayer")
        {
            gPlayer.canMoveArea = !isExistNest;
            Debug.Log(gPlayer.canMoveArea);
        }
    }

    public void DestoyNest()
    {
        isExistNest = false;
        gPlayer.canMoveArea = !isExistNest;
    }

    private void Update()
    {
        //デバッグ用
        if (Input.GetKeyDown(KeyCode.B))
        {
            DestoyNest();
        }
    }

}
