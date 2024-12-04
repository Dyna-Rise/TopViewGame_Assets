using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public int arrangeId = 0; //配置ID
    public string objTag = ""; //配置物のタグ
}

[System.Serializable]
public class SaveDataList
{
    public SaveData[] saveDatas; //SaveDataクラスのデータを取り扱う配列
}