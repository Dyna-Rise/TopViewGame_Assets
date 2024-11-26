using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    public float deleteTime = 2; //消滅までの時間設定

    // Start is called before the first frame update
    void Start()
    {
        //生成されてからdeleteTime秒後に
        //消滅が決まっている
        Destroy(gameObject, deleteTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        transform.SetParent(collision.transform); //ぶつかった相手の子オブジェクトになる　※Enemyに刺さった場合はEnemyと一緒に移動する
        GetComponent<CircleCollider2D>().enabled = false; //当たり判定のコンポーネント機能を無効に
        GetComponent<Rigidbody2D>().simulated = false; //物理シミュレーションを無効か

    }
}
