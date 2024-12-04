using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemKeeper : MonoBehaviour
{

    public static int hasGoldKeys = 0; //金のカギ
    public static int hasSilverKeys = 0; //銀のカギ
    public static int hasArrows = 0; //矢の残数
    public static int hasLights = 0; //ライトの残数

    // Start is called before the first frame update
    void Start()
    {
        //アイテムをパソコンから読み込む
        hasGoldKeys = PlayerPrefs.GetInt("GoldKeys");
        hasSilverKeys = PlayerPrefs.GetInt("SilberKeys");
        hasArrows = PlayerPrefs.GetInt("Arrows");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //アイテムを保存するメソッド
    public static void SaveItem()
    {
        PlayerPrefs.SetInt("GoldKeys",hasGoldKeys);
        PlayerPrefs.SetInt("SilerKeys",hasSilverKeys);
        PlayerPrefs.SetInt("Arrows",hasArrows);
    }
}
