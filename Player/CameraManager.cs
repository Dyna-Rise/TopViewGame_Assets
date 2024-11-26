using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    //アレンジ
    GameObject player;　//スコープの兼ね合いからplayerをここで用意しておく

    // Start is called before the first frame update
    void Start()
    {
        //アレンジ
        //最初に一度だけプレイヤーオブジェクトを検索
        player = GameObject.FindGameObjectWithTag("Player");

    }

    // Update is called once per frame
    void Update()
    {
        //プレイヤーオブジェクトを検索
        //GameObject player = GameObject.FindGameObjectWithTag("Player");

        //もしもプレイヤーがいれば処理する
        if(player != null)
        {
            //カメラの位置を決める
            transform.position = new Vector3(player.transform.position.x,player.transform.position.y,-10);
        }
    }
}
