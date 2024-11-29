using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox : MonoBehaviour
{
    public Sprite openImage; //開いた画像
    public GameObject itemPrefab; //出てくるアイテムのプレハブ
    public bool isClosed = true; //閉じているか
    public int arrangeId = 0;


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
        if (isClosed && collision.gameObject.tag == "Player")
        {
            //フタが閉まっているアイ状態でプレイヤーに接触したら
            GetComponent<SpriteRenderer>().sprite = openImage;
            isClosed = false; //閉じるフラグをオフに
            if (itemPrefab != null)
            {
                //指定したアイテムを生成
                Instantiate(itemPrefab, transform.position, Quaternion.identity);
            }
        }
    }
}
