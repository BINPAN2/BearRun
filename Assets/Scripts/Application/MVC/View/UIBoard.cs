using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBoard : View
{
    #region 常量
    public const float StartTime = 50;
    #endregion

    #region 事件
    #endregion

    #region 字段
    int m_Coin = 0;
    int m_Distance = 0;
    int m_Goal = 0;
    float m_Time;

    public Text txtCoin;
    public Text txtDis;
    public Text txtTimer;

    public Text txtGizmoMultiply;
    public Text txtGizmoMagnet;
    public Text txtGizmoInvincible;

    public Button btnMagnet;
    public Button btnMultiply;
    public Button btnInvincible;
    public Button btnGoal;

    public Slider sliderTime;
    public Slider sliderGoal;

    private GameModel gm;

    public float m_SkillTime;

    IEnumerator Multiplycor;//双倍金币协程
    IEnumerator Magnetcor;//吸铁石协程
    IEnumerator Invinciblecor;//无敌协程

    #endregion

    #region 属性

    public override string Name
    {
        get
        {
            return Const.V_Board;
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
            txtCoin.text = m_Coin.ToString();
        }
    }

    public int Distance
    {
        get
        {
            return m_Distance;
        }

        set
        {
            m_Distance = value;
            txtDis.text = m_Distance.ToString() + "米";
        }
    }

    public float Times
    {
        get
        {
            return m_Time;
        }

        set
        {
            if (value<0)
            {
                value = 0;
                //游戏结束
                SendEvent(Const.E_EndGame);
            }

            if (value >StartTime)
            {
                value = StartTime;
            }
            m_Time = value;
            txtTimer.text = m_Time.ToString("f2")+"s";
            sliderTime.value = value / StartTime;
        }
    }

    public int Goal
    {
        get
        {
            return m_Goal;
        }

        set
        {
            m_Goal = value;
        }
    }
    #endregion

    #region 方法
    //更新按钮是否可用
    public void UpdateUI()
    {
        ShowOrHide(gm.Magnet, btnMagnet);
        ShowOrHide(gm.Multiply, btnMultiply);
        ShowOrHide(gm.Invincible, btnInvincible);
    }

    public void ShowOrHide(int i,Button btn)
    {
        if (i>0)
        {
            btn.interactable = true;
            btn.transform.Find("Mask").gameObject.SetActive(false);
        }
        else
        {
            btn.interactable = false;
            btn.transform.Find("Mask").gameObject.SetActive(true);
        }
    }

    public void OnPauseButtonClick()
    {
        PauseArgs e = new PauseArgs
        {
            coin = Coin,
            distance = Distance,
            score = Coin * 3 + Distance + Goal * 20
        };
        SendEvent(Const.E_PauseGame,e);
    }

    public string GetTimer(float time)
    {
        return ((int)time+1).ToString();
    }

    //双倍金币
    public void HitDouble()
    {
        if (Multiplycor != null)
        {
            StopCoroutine(Multiplycor);
        }
        Multiplycor = MultiplyCoroutine();
        StartCoroutine(Multiplycor);
    }

    IEnumerator MultiplyCoroutine()
    {
        txtGizmoMultiply.transform.parent.gameObject.SetActive(true);
        float Timer = m_SkillTime;
        while (Timer > 0)
        {
            if (!gm.IsPause && gm.IsPlay)
            {
                Timer -= Time.deltaTime;
                txtGizmoMultiply.text = GetTimer(Timer);
            }
            yield return 0;
        }
        //yield return new WaitForSeconds(m_SkillTime);
        txtGizmoMultiply.transform.parent.gameObject.SetActive(false);
    }

    //吸铁石

    public void HitMagnet()
    {
        if (Magnetcor != null)
        {
            StopCoroutine(Magnetcor);
        }
        Magnetcor = MagnetCoroutine();
        StartCoroutine(Magnetcor);
    }

    IEnumerator MagnetCoroutine()
    {
        txtGizmoMagnet.transform.parent.gameObject.SetActive(true);
        float Timer = m_SkillTime;
        while (Timer > 0)
        {
            if (!gm.IsPause && gm.IsPlay)
            {
                Timer -= Time.deltaTime;
                txtGizmoMagnet.text = GetTimer(Timer);
            }
            yield return 0;
        }
        // yield return new WaitForSeconds(m_SkillTime);
        txtGizmoMagnet.transform.parent.gameObject.SetActive(false);
    }

    //无敌

    public void HitInvincible()
    {
        if (Invinciblecor != null)
        {
            StopCoroutine(Invinciblecor);
        }
        Invinciblecor = InvincibleCoroutine();
        StartCoroutine(Invinciblecor);
    }

    IEnumerator InvincibleCoroutine()
    {
        txtGizmoInvincible.transform.parent.gameObject.SetActive(true);
        float Timer = m_SkillTime;
        while (Timer > 0)
        {
            if (!gm.IsPause && gm.IsPlay)
            {
                Timer -= Time.deltaTime;
                txtGizmoInvincible.text = GetTimer(Timer);
            }
            yield return 0;
        }
        //yield return new WaitForSeconds(m_SkillTime);
        txtGizmoInvincible.transform.parent.gameObject.SetActive(false);
    }


    public void OnMagnetClick()
    {
        ItemArgs e = new ItemArgs
        {
            itemkind = ItemKind.MagnetItem,
            itemCount = 1
        };

        SendEvent(Const.E_HitItem, e);
    }

    public void OnInvincibleClick()
    {
        ItemArgs e = new ItemArgs
        {
            itemkind = ItemKind.InvincibleItem,
            itemCount = 1
        };

        SendEvent(Const.E_HitItem, e);
    }

    public void OnMultiplyClick()
    {
        ItemArgs e = new ItemArgs
        {
            itemkind = ItemKind.MultiplyItem,
            itemCount = 1
        };

        SendEvent(Const.E_HitItem, e);
    }


    void ShowGoalbtn()
    {
        StartCoroutine(StartGoalCor());
    }


    IEnumerator StartGoalCor()
    {
        btnGoal.interactable = true;
        sliderGoal.value = 1;
        while (sliderGoal.value > 0 )
        {
            if (!gm.IsPause && gm.IsPlay)
            {
                sliderGoal.value -= 1.5f * Time.deltaTime;
            }
            yield return 0;
        }
        btnGoal.interactable = false;
        sliderGoal.value = 0;

    }


    public void OnGoalBtnClick()
    {
        SendEvent(Const.E_GoalBtnClick);
        btnGoal.interactable = false;
        sliderGoal.value = 0;
    }


    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    #endregion



    #region Unity回调

    private void Awake()
    {
        Times = StartTime;
        gm = GetModel<GameModel>();
        UpdateUI();
        m_SkillTime = gm.SkillTime;
    }

    private void Update()
    {
        if (!gm.IsPause&&gm.IsPlay)
        {
            Times -= Time.deltaTime;
        }

    }


    #endregion

    #region 事件回调

    public override void RegisterAttentionList()
    {
        AttentionList.Add(Const.E_UpdateDis);
        AttentionList.Add(Const.E_UpdateCoin);
        AttentionList.Add(Const.E_HitAddtime);
        AttentionList.Add(Const.E_HitGoalTrigger);
        AttentionList.Add(Const.E_ShootGoal);
    }


    public override void HandleEvent(string eventName, object data)
    {
        switch (eventName)
        {
            case Const.E_UpdateDis:
                DistanceArgs e1 = data as DistanceArgs;
                Distance = e1.distance;
                break;
            case Const.E_UpdateCoin:
                CoinArgs e2 = data as CoinArgs;
                Coin += e2.Coin;
                break;
            case Const.E_HitAddtime:
                Times += 20;
                break;
            case Const.E_HitGoalTrigger:
                ShowGoalbtn();
                break;
            case Const.E_ShootGoal:
                Goal += 1;
                print("进了" + Goal + "个球");
                break;
        }

    }
    #endregion

    #region 帮助方法
    #endregion



}
