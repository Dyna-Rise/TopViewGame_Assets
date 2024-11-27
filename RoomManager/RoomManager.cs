using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviour
{
    //static変数
    public static int doorNumber = 0; //ドア番号（シーンを跨いで記憶されるもの）

    // Start is called before the first frame update
    void Start()
    {
        //移動前のシーンで触ったExitオブジェクトのdoorNumberと一致する番号を
        //移動先（現シーン）のExitオブジェクトから探し出さなければならない

        //すべてのEixtオブジェクトの情報を配列entersに確保
        GameObject[] enters = GameObject.FindGameObjectsWithTag("Exit");

        //配列entersに確保したすべてのExitオブジェクトを総点検
        for(int i = 0; i < enters.Length; i++)
        {
            //配列の先頭のExitオブジェクトから順に、一時的にdoorObj変数に情報を格納
            GameObject doorObj = enters[i];
            //一時的に確保した、とあるExitオブジェクトのExitスクリプトを扱えるようにする
            Exit exit = doorObj.GetComponent<Exit>();

            //一時的に確保したExitオブジェクトのdoorNumberと、探しているdoorNumberが一致するか比べる
            //doorNumberが一致した時のExitオブジェクトが、目的のExitオブジェクトという事になる
            if (doorNumber == exit.doorNumber)
            {

                //Playerを該当のExitの近くに配置する
                //見つかったExitの座標をX、Y別々に取得しておく
                float x = doorObj.transform.position.x;
                float y = doorObj.transform.position.y;

                //見つかったExitのExitDirection型の値によって
                //プレイヤーの配置すべき位置をずらす
                //まずは座標の獲得（変数x,yに取得）
                if(exit.direction == ExitDirection.up)
                {
                    y += 1;
                }
                else if(exit.direction == ExitDirection.right)
                {
                    x += 1;
                }
                else if(exit.direction == ExitDirection.down)
                {
                    y -= 1;
                }
                else if (exit.direction == ExitDirection.left)
                {
                    x -= 1;
                }

                //いざPlayerを移動させるために、Playerオブジェクトの情報を取得
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                //事前に決めておいた変数x,yの値をつかってPlayerを移動
                player.transform.position = new Vector3(x, y);

                //ここまでくれば目的は達成したので、for文による残りの探索は切り上げる
                break; //ループを抜ける
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //シーンを移動するstaticメソッド
    public static void ChangeScene(string scenename,int doornum)
    {
        doorNumber = doornum; //引数を通して情報として与えられたドア番号をstatic変数のdoorNumberに記憶しておく
        SceneManager.LoadScene(scenename); //シーン移動
    }

}
