using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StaticData :MonoSingleton<StaticData>{

    //足球信息（材质，价格）
    public Dictionary<int, FootBallInfo> m_FootballData = new Dictionary<int, FootBallInfo>();
    public List<Material> FootballMat;

    //人物信息（材质，价格）
    public Dictionary<int, Dictionary<int, FootBallInfo>> m_PlayerSkinData = new Dictionary<int, Dictionary<int, FootBallInfo>>();
    public List<Material> PlayerMat;

    protected override void Awake()
    {
        base.Awake();
        InitFootballInfo();
        InitPlayerSkinInfo();
    }

    public void InitFootballInfo()
    {
        m_FootballData.Add(0, new FootBallInfo { coin = 0, FootballMaterial = FootballMat[0] });
        m_FootballData.Add(1, new FootBallInfo { coin = 1000, FootballMaterial = FootballMat[1] });
        m_FootballData.Add(2, new FootBallInfo { coin = 2000, FootballMaterial = FootballMat[2] });
    }
    
    public void InitPlayerSkinInfo()
    {
        int t = 0;
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (!m_PlayerSkinData.ContainsKey(i))
                {
                    m_PlayerSkinData.Add(i, new Dictionary<int, FootBallInfo>());
                }
                print(t);
                m_PlayerSkinData[i].Add(j, new FootBallInfo { coin = t * 1000, FootballMaterial = PlayerMat[t] });
                t++;
            }
        }
    }

    public FootBallInfo GetFootballInfo(int i)
    {
        return m_FootballData[i];
    }

    public FootBallInfo GetPlayerSkinInfo(int i, int j)
    {
        return m_PlayerSkinData[i][j];
    }
}
