using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//アイテム種類
public enum ItemType
{
    Arrow, //矢
    GoldKey, //金のカギ
    SilverKey, //銀のカギ
    Life, //ライフ
    Light, //ライト
}

public class ItemData : MonoBehaviour
{
    public ItemType type;
    public int count = 1;
    public int aranngeId = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //金の鍵なら
            if (type == ItemType.GoldKey)
            {
                ItemKeeper.hasGoldKeys += count;
            }
            // 銀の鍵なら
            else if (type == ItemType.SilverKey)
            {
                ItemKeeper.hasSilverKeys += count;
            }
            // 矢なら
            else if (type == ItemType.Arrow)
            {
                ArrowShoot shoot = collision.gameObject.GetComponent<ArrowShoot>();
                ItemKeeper.hasArrows += count;
            }
            else if (type == ItemType.Life)
            {
                //ライフ
                if (PlayerController.hp < 3)
                {
                    //HPが3未満の場合は回復する
                    PlayerController.hp++;
                }
            }
            else if (type == ItemType.Light)
            {
                //ライト
                ItemKeeper.hasLights += count;
                //ライトを拾ったらPlayerの子オブジェクトであるLight2Dの距離とアイテム数を連動
                GameObject.FindObjectOfType<PlayerLightController>().LightUpdate();
            }

            //アイテム取得の演出
            //当たり判定を消す
            GetComponent<CircleCollider2D>().enabled = false;
            //Rigidbody2Dを操作する
            Rigidbody2D itemBody = GetComponent<Rigidbody2D>();
            //重力を戻す
            itemBody.gravityScale = 2.5f;

            //上に少し跳ねあがる演出
            itemBody.AddForce(new Vector2(0, 6), ForceMode2D.Impulse);

            //0.5秒
            Destroy(gameObject, 0.5f);

        }
    }
}
