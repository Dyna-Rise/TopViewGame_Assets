using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowShoot : MonoBehaviour
{
    public float shootSpeed = 12.0f; //矢の速度
    public float shootDelay = 0.25f; //発射間隔
    public GameObject bowPrefab; //弓のプレハブ
    public GameObject arrowPrefab; //矢のプレハブ
    bool inAttack = false; //攻撃中フラグ※連射しないようにする
    GameObject bowObj; //弓のげーうオブジェクト


    //攻撃メソッド
    public void Attack()
    {
        //矢を持っている＆すでに攻撃中でないとき
        if(ItemKeeper.hasArrows > 0 && inAttack == false)
        {
            ItemKeeper.hasArrows--; //矢を1減らす
            inAttack = true; //連射できないように攻撃中フラグを立てる

            //矢を放つ

            //その時プレイヤーが向いている角度を調べて変数angleZに入れる
            PlayerController playerCnt = GetComponent<PlayerController>();
            float angleZ = playerCnt.angleZ;

            //矢のプレハブを生成する
            //まずは生成時にどの角度を向いているべきかを変数rに設定しておく
            //※プレイヤーの角度を参考にしている
            Quaternion r = Quaternion.Euler(0, 0, angleZ);
            //変数rに設定した角度を指定しながらInstantiateメソッドで矢を生成
            //生成した矢のオブジェの情報をarrowObj変数にさっそく代入しておく
            GameObject arrowObj = Instantiate(arrowPrefab,transform.position,r);

            //矢を発射するベクトルを調べる
            //X方向とY方向という材料からベクトルを作っていく
            //※angleZの角度に伸びる線を1とした場合の割合

            //angleZの角度に対してX方向の割合(Cosメソッド）
            float x = Mathf.Cos(angleZ * Mathf.Deg2Rad);

            //angleZの角度に対してY方向の割合(Sinメソッド）
            float y = Mathf.Sin(angleZ * Mathf.Deg2Rad);

            //X方向とY方向のデータからVector3型の方向データを予めつくっておく
            //作ったベクトルにshootSpeedのパワーをかけ算しておく
            //※Z方向は省略されて0
            Vector3 v = new Vector3(x, y) * shootSpeed;

            //Rigidbody2DのAddForceメソッドの力で矢を押し出す。あらかじめつくっておいたベクトルデータvを使う
            Rigidbody2D body = arrowObj.GetComponent<Rigidbody2D>();
            body.AddForce(v, ForceMode2D.Impulse);

            //矢を放ったのでshootDelay秒後にstopAttackメソッド（自作メソッド）を発動し攻撃中フラグを解除
            Invoke("StopAttack", shootDelay);

            //矢のSEを鳴らす
            SoundManager.soundManager.SEPlay(SEType.Shoot);
        }

    }

    //攻撃中フラグを下げるメソッド
    public void StopAttack()
    {
        inAttack = false; //攻撃中フラグを下げる
    }

    // Start is called before the first frame update
    void Start()
    {
        //弓のオブジェクトをプレイヤーに配置
        Vector3 pos = transform.position; //プレイヤーの位置を確認
        bowObj = Instantiate(bowPrefab, pos, Quaternion.identity); //弓オブジェクトをプレイヤーの位置に生成して、bowObj変数に生成したオブジェクト情報を格納
        bowObj.transform.SetParent(transform); //生成した弓の親としてプレイヤーを指名※以後プレイヤーについてくる
    }

    // Update is called once per frame
    void Update()
    {
        //Fire3(Shitキー）をおしたら
        //if (Input.GetButtonDown("Fire3"))
        //Eキーをおしたらにアレンジ
        if (Input.GetKeyDown(KeyCode.E))
        {
            //自作した攻撃メソッドの発動
            Attack();
        }

        //弓の動きを制御

        //基本は弓のZの値を-1（プレイヤーより手前にする※カメラに近くする）
        float bowZ = -1; 

        //プレイヤーが上向きであれば弓は逆にプレイヤーの奥にいなければならない
        PlayerController plmv = GetComponent<PlayerController>();
        //もしもプレイヤーの向きが上向きなら
        if(plmv.angleZ > 30 && plmv.angleZ < 150)
        {
            //弓をプレイヤーより後ろのZ座標にする※カメラから遠ざける
            bowZ = 1; //プレイヤーはZ座標＝0
        }

        //※ここまでbowZはプレイヤーの手間に映るか、奥に映るかの値をセッティングされているだけで、まだ描画には反映されていない

        //弓の回転方向をプレイヤーと同じにする
        bowObj.transform.rotation = Quaternion.Euler(0, 0, plmv.angleZ);

        //あらかじめ設定しておいたbowZの設定を実際にpositionに反映させる
        bowObj.transform.position = new Vector3(transform.position.x, transform.position.y, bowZ);
    }
}
