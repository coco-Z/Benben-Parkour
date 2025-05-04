using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameModel : Model
{
    #region 常量
    #endregion

    #region 事件
    #endregion

    #region 字段
    //初始金币
    public int MyCoin = 3000;

    //游戏运行
    bool m_Isplay = true;
    bool m_IsPause = false;

    //初始设定
    float m_StartTime = 60.0f;
    float m_SkillTime = 5;
    int m_Coin = 0;

    //道具相关
    int m_Magnet = 0;
    int m_multiply = 0;
    int m_invincible = 0;

    //衣服相关
    public bool clothe2 = false;
    public bool clothe3 = false;
    public bool clothe1fit = true;
    public bool clothe2fit = false;
    public bool clothe3fit = false;

    #endregion

    #region 属性
    public override string Name{ get => Consts.M_GameModel;}
    public bool Isplay { get => m_Isplay; set => m_Isplay = value; }
    public bool IsPause { get => m_IsPause; set => m_IsPause = value; }
    public float SkillTime { get => m_SkillTime; set => m_SkillTime = value; }
    public int Coin { get => m_Coin; set => m_Coin = value; }
    public int Invincible { get => m_invincible; set => m_invincible = value; }
    public int Multiply { get => m_multiply; set => m_multiply = value; }
    public int Magnet { get => m_Magnet; set => m_Magnet = value; }
    public float StartTime { get => m_StartTime; set => m_StartTime = value; }
    #endregion

    #region 方法
    public void Init()
    {

    }
    public bool HasEnMoney(int coin)
    {
        if (coin <= MyCoin)
        {
            MyCoin -= coin;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void LoadGame()
    {
        string path = Path.Combine(Application.persistentDataPath, "MyCoin.txt");
        string path1 = Path.Combine(Application.persistentDataPath, "clothe2.txt");
        string path2 = Path.Combine(Application.persistentDataPath, "clothe3.txt");
        string path3 = Path.Combine(Application.persistentDataPath, "clothe1fit.txt");
        string path4 = Path.Combine(Application.persistentDataPath, "clothe2fit.txt");
        string path5 = Path.Combine(Application.persistentDataPath, "clothe3fit.txt");
        if (File.Exists(path) && File.Exists(path1) && File.Exists(path2) && File.Exists(path3) && File.Exists(path4) && File.Exists(path5))
        {
            string T1 = File.ReadAllText(path);
            string T2 = File.ReadAllText(path1);
            string T3 = File.ReadAllText(path2);
            string T4 = File.ReadAllText(path3);
            string T5 = File.ReadAllText(path4);
            string T6 = File.ReadAllText(path5);
            MyCoin = int.Parse(T1);
            clothe2 = bool.Parse(T2);
            clothe3 = bool.Parse(T3);
            clothe1fit = bool.Parse(T4);
            clothe2fit = bool.Parse(T5);
            clothe3fit = bool.Parse(T6);
            Debug.Log(path);
        }
        else
        {
            SaveGame();
        }
    }

    public void SaveGame()
    {
        string path = Path.Combine(Application.persistentDataPath, "MyCoin.txt");
        File.WriteAllText(path, MyCoin.ToString());
        string path1 = Path.Combine(Application.persistentDataPath, "clothe2.txt");
        File.WriteAllText(path1, clothe2.ToString());
        string path2 = Path.Combine(Application.persistentDataPath, "clothe3.txt");
        File.WriteAllText(path2, clothe3.ToString());
        string path3 = Path.Combine(Application.persistentDataPath, "clothe1fit.txt");
        File.WriteAllText(path3, clothe1fit.ToString());
        string path4 = Path.Combine(Application.persistentDataPath, "clothe2fit.txt");
        File.WriteAllText(path4, clothe2fit.ToString());
        string path5 = Path.Combine(Application.persistentDataPath, "clothe3fit.txt");
        File.WriteAllText(path5, clothe3fit.ToString());
    }
    #endregion


    #region 事件回调
    #endregion

    #region 帮助方法
    #endregion


}
