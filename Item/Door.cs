using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public int arrangeId = 0; // セーブする際の識別番号
    public bool isGoldDoor = false; //自分が金のドアかどうか

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //ぶつかった相手がPlayer
        if (collision.gameObject.tag == "Player")
        {
            //自分がゴールドなら
            if (isGoldDoor)
            {
                //ゴールドキーを持っていれば
                if (ItemKeeper.hasGoldKeys > 0)
                {
                    //ゴールドキーを消耗
                    ItemKeeper.hasGoldKeys--;
                    Destroy(this.gameObject); //自分を削除
                }
            }
            else //自分がゴールドではない
            {
                //シルバーキーを持っていれば
                if (ItemKeeper.hasSilverKeys > 0)
                {
                    //シルバーキーを消耗
                    ItemKeeper.hasSilverKeys--;
                    Destroy(gameObject); //自分を削除
                }
            }
        }
    }
}
