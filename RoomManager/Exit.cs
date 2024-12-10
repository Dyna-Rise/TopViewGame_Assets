using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//プレイヤーの出る位置
public enum ExitDirection
{
    right,  //右方向
    left,  //左方向
    down,  //下方向
    up,  //上方向
}

public class Exit : MonoBehaviour
{
    public string sceneName = ""; //移動先のシーン
    public int doorNumber = 0; //ドア番号
    public ExitDirection direction = ExitDirection.down; //プレイヤーの出る位置


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Exitオブジェクトが何かと接触したら
    void OnTriggerEnter2D(Collider2D collision)
    {
        //接触した相手がPlayerだったら
        if(collision.gameObject.tag == "Player")
        {
            if(doorNumber == 100)
            {
                //BGM停止
                SoundManager.soundManager.StopBgm();

                //ゲームクリアSE再生
                SoundManager.soundManager.SEPlay(SEType.GameClear);

                //ゲームクリアの発動
                GameObject.FindObjectOfType<UIManager>().GameClear();
            }
            else
            {
                string nowScene = PlayerPrefs.GetString("LastScene");
                //配置データを保存
                SaveDataManager.SaveArrangeData(nowScene);

                //RoomManagerクラスのChangeSceneメソッドの力を使い、
                //Exit自身がもっている「目的のシーン」と「ドア番号」の情報を引数に指定しながらシーンを切り替える。
                //これにより、目的のシーンに切り替わる際に、同時に該当のExitのドア番号がstatic変数のDoorNumberに記録され、
                //次シーンの冒頭(Start)において、
                //RoomManagerがstatic変数に記憶されたドア番号と同じ番号を所持するExitオブジェクトを検索していく行為に繋がる
                RoomManager.ChangeScene(sceneName, doorNumber);
            }

        }
    }
}
