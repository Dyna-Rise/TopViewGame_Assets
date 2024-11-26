using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //プレイヤーオブジェクトを検索
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        //もしもプレイヤーがいれば処理する
        if(player != null)
        {
            //カメラの位置を決める
            transform.position = new Vector3(player.transform.position.x,player.transform.position.y,-10);
        }
    }
}
