using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModel :Model {


    #region 常量
    public const int InitCoin = 1000;
    #endregion

    #region 事件
    #endregion

    #region 字段
    private bool m_IsPlay = true;
    private bool m_IsPause = false;
    private int m_SkillTime = 5;
    public int m_Magnet;
    public int m_Multiply;
    public int m_Invincible;

    private int m_level;
    private int m_exp;
    private int m_coin;

    #endregion

    #region 属性
    public override string Name
    {
        get
        {
            return Const.M_GameModel;
        }
    }

    public bool IsPlay
    {
        get
        {
            return m_IsPlay;
        }

        set
        {
            m_IsPlay = value;
        }
    }

    public bool IsPause
    {
        get
        {
            return m_IsPause;
        }

        set
        {
            m_IsPause = value;
        }
    }

    public int SkillTime
    {
        get
        {
            return m_SkillTime;
        }

        set
        {
            m_SkillTime = value;
        }
    }

    public int Magnet
    {
        get
        {
            return m_Magnet;
        }

        set
        {
            m_Magnet = value;
        }
    }

    public int Multiply
    {
        get
        {
            return m_Multiply;
        }

        set
        {
            m_Multiply = value;
        }
    }

    public int Invincible
    {
        get
        {
            return m_Invincible;
        }

        set
        {
            m_Invincible = value;
        }
    }

    public int Level
    {
        get
        {
            return m_level;
        }

        set
        {
            m_level = value;
        }
    }

    public int Exp
    {
        get
        {
            return m_exp;
        }

        set
        {
            while(value > 500+Level*100)
            {
                value -= 500 + Level * 100;
                Level++;
            }
            m_exp = value;
        }
    }

    public int Coin
    {
        get
        {
            return m_coin;
        }

        set
        {
            m_coin = value;
            Debug.Log("还剩" + Coin + "金币");
        }
    }

    #endregion

    #region 方法
    public void Init()
    {
        m_Magnet = 1;
        m_Multiply = 2;
        m_Invincible = 3;
        m_SkillTime = 5;
        Exp = 0;
        Level = 1;
        Coin = InitCoin;
    }

    //金币是否足够
    public bool PayCoin(int coin)
    {
        if (coin<=Coin)
        {
            Coin -= coin;
            return true;
        }
        return false;
    }
    #endregion

    #region Unity回调
    #endregion

    #region 事件回调
    #endregion

    #region 帮助方法
    #endregion

}
