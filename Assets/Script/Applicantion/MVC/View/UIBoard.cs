using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UI;
using static Consts;

public class UIBoard : View
{
    #region 常量
    #endregion

    #region 事件
    #endregion

    #region 字段
    private int m_Distance = 0;
    private int m_Coin = 0;
    private int m_Goal = 0;
    private float m_Time = 60.0f;
    private float startTime = 60.0f;
    private GameModel gm;
    [Header("Head")]
    public Text textDistance;
    public Text textCoin;
    public Text textTimer;
    public Slider sliderTimer;
    [Header("Button")]
    public Button buttonInvincible;
    public Button buttonMagnet;
    public Button buttonMultiply;
    [Header("Show")]
    public Image MultiplyImage;
    public Text MultiplyTextNumber;
    public Image MagnetImage;
    public Text MagnetTextNumber;
    public Image InvincibleImage;
    public Text InvincibleTextNumber;
    [Header("Goal")]
    public Button buttonGoal;
    public Slider sliderGoal;


    //协程
    IEnumerator MultiplyCor;
    IEnumerator MagnetCor;
    IEnumerator InvincibleCor;
    #endregion

    #region 属性

    public override string Name
    {
        get
        {
            return Consts.V_Board;
        }
    }

    public int Distance {
        get
        {
            return m_Distance;
        }
        set 
        { 
            m_Distance = value;
            textDistance.text = value.ToString();   
        }
    }

    public int Coin
    {
        get
        {
            return m_Coin;
        }
        set
        {
            m_Coin = value;
            textCoin.text = value.ToString();
        }
    }

    public float Times { 
        get => m_Time;
        set
        {
            if(value < 0)
            {
                value = 0;
                SendEvent(Consts.E_EndGame);
            }
            if(value > startTime)
            {
                value = startTime;
            }
            m_Time = value;
            textTimer.text = value.ToString("f2");
            sliderTimer.value = value / startTime;
        }
    }

    public int Goal { get => m_Goal; set => m_Goal = value; }

    #endregion

    #region 方法
    public void OnPauseClick()
    {
        PauseArgs args = new PauseArgs()
        {
            coin = Coin,
            Distance = Distance,
            score = (Coin + Distance) * 2
        };
        SendEvent(Consts.E_PauseGame, args);
    }
    public void UpdateUI()
    {
        ShowORHide(gm.Invincible, buttonInvincible);
        ShowORHide(gm.Magnet, buttonMagnet);
        ShowORHide(gm.Multiply, buttonMultiply);
    }
    public void ShowORHide(int i,Button button1)
    {
        if(i>0)
        {
            button1.interactable = true;
            button1.transform.Find("Mask").gameObject.SetActive(false);
        }
        else
        {
            button1.interactable = false;
            button1.transform.Find("Mask").gameObject.SetActive(true);
        }
         button1.transform.Find("Number").gameObject.GetComponent<Text>().text ="x" + i.ToString();
    }

    //双倍金币
    public void HitMultiply()
    {
        //Debug.Log("双倍金币");
        if (MultiplyCor != null)
        {
            StopCoroutine(MultiplyCor);
        }
        MultiplyCor = MultiplyCor2();
        StartCoroutine(MultiplyCor);

    }
    IEnumerator MultiplyCor2()
    {

        float timer = gm.SkillTime;
        MultiplyImage.gameObject.SetActive(true);
        while (timer > 0)
        {
            if (!gm.IsPause && gm.Isplay)
            {
                timer = timer - Time.deltaTime;
                MultiplyTextNumber.text = ((int)timer + 1).ToString();
            }
            yield return 0;
        }
        MultiplyImage.gameObject.SetActive(false);
    }
    //磁铁
    public void HitMagnet()
    {
        if (MagnetCor != null)
        {
            StopCoroutine(MagnetCor);
        }
        MagnetCor = MagnetCor2();
        StartCoroutine(MagnetCor);
    }
    IEnumerator MagnetCor2()
    {

        float timer = gm.SkillTime;
        MagnetImage.gameObject.SetActive(true);
        while (timer > 0)
        {
            if (!gm.IsPause && gm.Isplay)
            {
                timer = timer - Time.deltaTime;
                MagnetTextNumber.text = ((int)timer + 1).ToString();
            }
            yield return 0;
        }
        MagnetImage.gameObject.SetActive(false);
    }
    //无敌
    public void HitInvincible()
    {
        if (InvincibleCor != null)
        {
            StopCoroutine(InvincibleCor);
        }
        InvincibleCor = InvincibleCor2();
        StartCoroutine(InvincibleCor);
    }
    IEnumerator InvincibleCor2()
    {

        float timer = gm.SkillTime;
        InvincibleImage.gameObject.SetActive(true);
        while (timer > 0)
        {
            if (!gm.IsPause && gm.Isplay)
            {
                timer = timer - Time.deltaTime;
                InvincibleTextNumber.text = ((int)timer + 1).ToString();
            }
            yield return 0;
        }
        InvincibleImage.gameObject.SetActive(false);
    }
    //按钮点击事件
    public void OnMagnetClick()
    {
        ItemArgs e = new ItemArgs
        {
            hitCount = 1,
            kind = Consts.ItemKind.ItemMagnet
        };
        SendEvent(Consts.E_HitItem, e);
    }
    public void OnInvincibleClick()
    {
        ItemArgs e = new ItemArgs
        {
            hitCount = 1,
            kind = Consts.ItemKind.ItemInvincible
        };
        SendEvent(Consts.E_HitItem, e);
    }
    public void OnMultiplyClick()
    {
        ItemArgs e = new ItemArgs
        {
            hitCount = 1,
            kind = Consts.ItemKind.ItemMultiply
        };
        SendEvent(Consts.E_HitItem, e);
    }

    public void ShowGoalClick()
    {
        StartCoroutine(StartCountDown());
    }

    IEnumerator StartCountDown()
    {
        buttonGoal.interactable = true; 
        sliderGoal.value = 1;
        while(sliderGoal.value > 0)
        {
            if(gm.Isplay&&!gm.IsPause)
            {
                sliderGoal.value -= 1f * Time.deltaTime;
            }
            yield return 0;
        }
        buttonGoal.interactable = false;
        sliderGoal.value = 0;
    }

    //按下射门
    public void OnGoalButtonClick()
    {
        sliderGoal.value = 0;
        SendEvent(Consts.E_ClickGoalButton);
    }


    #endregion

    #region Unity回调
    private void Awake()
    {
        gm = GetModel<GameModel>();
        Times = gm.StartTime;
        startTime = gm.StartTime;
    }
    private void Start()
    {
        UpdateUI();
        Game.Instance.sound.PlayBG("Bgm_ZhanDou");
    }
    private void Update()
    {
        if(!gm.IsPause && gm.Isplay)
        {
            Times = Times - Time.deltaTime;
        }
        Debug.Log(gm.MyCoin);
        
    }



    #endregion

    #region 事件回调

    public override void HandleEvent(string name, object data)
    {
        switch(name)
        {
            case Consts.E_UpdateDis:
                Distance = (int)data;
                break;
            case Consts.E_UpdateCoin:
                Coin = Coin + (int)data;
                break;
            case Consts.E_UpdateGoal:
                Goal = Goal + (int)data;
                break;
            case Consts.E_ADDTime:
                Times = Times + (int)data;
                break;
            case Consts.E_HitGoalTrigger:
                ShowGoalClick();
                break;
        }
    }

    public override void RegisterAttentionEvent()
    {
        AttentionList.Add(Consts.E_UpdateDis);
        AttentionList.Add(Consts.E_UpdateCoin);
        AttentionList.Add(Consts.E_UpdateGoal);
        AttentionList.Add(Consts.E_ADDTime);
        AttentionList.Add(Consts.E_HitGoalTrigger);
    }

    #endregion

    #region 帮助方法
    #endregion




}
