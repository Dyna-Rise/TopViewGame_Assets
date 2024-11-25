
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 3.0f; //移動スピード
    int direction = 0; //移動方向のアニメ番号
    float axisH; //横軸の値
    float axisV; //縦軸の値
    public float angleZ = -90.0f; //回転角度
    Rigidbody2D rbody; //Rigidbody2Dを参照予定
    Animator animator; //Animatorを参照予定
    bool isMoving = false; //移動中か判断するフラグ

    //p1からp2の角度を返すメソッド
    float GetAngle(Vector2 p1, Vector2 p2)
    {
        float angle;
        if (axisH != 0 || axisV != 0)
        {
            //移動中であれば角度を更新する
            //p1からp2への差分（原点を0にする)
            float dx = p2.x - p1.x;
            float dy = p2.y - p1.y;
            //アークタンジェント2メソッドの能力で角度（ラジアン値：円周率）を求める
            float rad = Mathf.Atan2(dy, dx);
            //ラジアン値をオイラー値（〇度）という表現に変換する
            angle = rad * Mathf.Rad2Deg;
        }
        else
        {
            //停止中であれば以前の角度を維持していく
            angle = angleZ;
        }
        return angle;
    }

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>(); //PlayerのRigidbody2Dを参照
        animator = GetComponent<Animator>(); //PlayerのAnimatorを参照
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving == false)
        {
            axisH = Input.GetAxisRaw("Horizontal"); //左右のキーを検知
            axisV = Input.GetAxisRaw("Vertical"); //上下のキーを検知
        }

        //キー値から移動角度を求める
        Vector2 fromPt = transform.position;
        Vector2 toPt = new Vector2(fromPt.x + axisH, fromPt.y + axisV);
        angleZ = GetAngle(fromPt, toPt);

        //移動角度から向いている方向を決めてアニメーションを更新

        int dir;
        if (angleZ >= -45 && angleZ < 45)
        {
            //向くべき方向は右向き
            dir = 3;
        }
        else if (angleZ >= 45 && angleZ <= 135)
        {
            //向くべき方向は上向き
            dir = 2;
        }
        else if (angleZ >= -135 && angleZ <= -45)
        {
            //向くべき方向は下向き
            dir = 0;
        }
        else
        {
            //それ以外は消去法で左向き
            dir = 1;
        }
        //向くべき方向が前と違っていたらアニメ更新
        if (dir != direction)
        {
            direction = dir; //向くべきアニメ方向の番号を更新
            animator.SetInteger("Direction", direction);
        }
    }

    void FixedUpdate()
    {
        //移動速度の更新
        rbody.velocity = new Vector2(axisH, axisV).normalized * speed;
    }

    //バーチャルパッド対策
    public void SetAxis(float h, float v)
    {
        axisH = h;
        axisV = v;
        if(axisH == 0 && axisV == 0)
        {
            isMoving = false;
        }
        else
        {
            isMoving = true;
        }
    }
}
