using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    public GameObject startButton; //スタートボタンオブジェクト
    
    public GameObject continueButton; //コンティニューボタンオブジェクト

    public string firstSceneName; //ゲーム開始のシーン名


    // Start is called before the first frame update
    void Start()
    {
        //LastSceneキーワードに記憶されていたシーン名を取得
        string sceneName = PlayerPrefs.GetString("LastScene");

        //LastSceneにデータがない
        if(sceneName == "")
        {
            continueButton.GetComponent<Button>().interactable = false; //コンティニューボタンを無効
        }
        else //LastSceneにデータがある
        {
            continueButton.GetComponent<Button>().interactable = true; //コンティニューボタンを有効
        }

        //タイトルBGM再生
        SoundManager.soundManager.PlayBgm(BGMType.Title);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //スタートボタン押し
    public void StartButtonClicked()
    {
        //PlayerPrefsのデータをクリア
        PlayerPrefs.DeleteAll();
        
        //HPを戻す
        PlayerPrefs.SetInt("PlayerHP", 3);

        //最後どこにいたかの情報をクリア※変数に指名したシーン名に書き換え
        PlayerPrefs.SetString("LastScene", firstSceneName);

        RoomManager.doorNumber = 0;

        //変数に指名したシーン名にジャンプ
        SceneManager.LoadScene(firstSceneName);

    }

    //続きからボタン押し
    public void ContinueButtonClicked()
    {
        //最後のシーンどこだったか？
        string sceneName = PlayerPrefs.GetString("LastScene");

        //最後くぐったドアの番号
        RoomManager.doorNumber = PlayerPrefs.GetInt("LastDoor");

        //LastSceneキーワードから取得した最後のシーンに戻る
        SceneManager.LoadScene(sceneName);
    }
}
