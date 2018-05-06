using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMove : View
{

    #region 常量
    const float JumpHight = 5;
    const float gravity = 9.8f;
    const float MaxSpeed = 40f;
    const float m_moveSpeed = 20f;
    const float SpeedAddDis = 200f;
    const float SpeedAddRate = 0.5f;
    #endregion

    #region 事件
    #endregion

    #region 字段

    public float speed = 20f;
    private CharacterController m_cc;
    private bool activeInput = false;
    private Vector3 m_mousePos = new Vector3(0, 0, 0);
    InputDirection m_inputDir = InputDirection.NULL;
    private int m_nowIndex = 1;
    private int m_targetIndex = 1;
    private float m_xdistance = 0;
    private float m_ydistance;
    bool m_IsSlide = false;
    private float RunDis = 0;
    float m_SlideTimer = 0.733f;
    public GameModel gm;

    //记录被撞时的速度
    private float m_MaskSpeed;
    //是否撞到障碍物
    private bool m_IsHit;
    //恢复速度
    private float m_AddRate = 10;

    //Item相关
    public int m_DoubleTime =1;//倍数
    private int m_SkillTime; //技能持续时间
    private SphereCollider m_MagnetCollier;
    bool m_IsInvincible = false;

    //射门相关
    public GameObject m_Ball;
    public GameObject m_trail;
    public bool isGoal=false;

    IEnumerator Multiplycor;//双倍金币协程
    IEnumerator Magnetcor;//吸铁石协程
    IEnumerator Invinciblecor;//无敌协程
    IEnumerator Goalcor;//射门协程

    public MeshRenderer ballMesh;
    public SkinnedMeshRenderer SkinMeshRender;

    #endregion

    #region 属性

    public override string Name
    {
        get
        {
            return Const.V_PlayerMove;
        }
    }

    public float Speed
    {
        get
        {
            return speed;
        }

        set
        {
            speed = value;
            if (speed >MaxSpeed)
            {
                speed = MaxSpeed;
            }
        }
    }

    #endregion

    #region 移动
    IEnumerator UpdateAction()
    {
        while (true)
        {
            if (!gm.IsPause&&gm.IsPlay)
            {
                //更新Distance UI
                UpdateDis();

                m_cc.Move((transform.forward * Speed + new Vector3(0, m_ydistance, 0)) * Time.deltaTime);
                m_ydistance -= gravity * Time.deltaTime;
                UpdatePosition();
                MoveControl();
                UpdateSpeed();
            }
            yield return null;
        }
    }


    private void UpdateDis()
    {
        DistanceArgs e = new DistanceArgs
        {
            distance = (int )transform.position.z
        };

        SendEvent(Const.E_UpdateDis, e);
    }

    private void GetDirection()
    {
        //识别鼠标滑动
        m_inputDir = InputDirection.NULL;
        if (Input.GetMouseButtonDown(0))
        {
            activeInput = true;
            m_mousePos = Input.mousePosition;
        }
        if (Input.GetMouseButton(0) && activeInput == true)
        {
            Vector3 Dir = Input.mousePosition- m_mousePos ;
            if (Dir.magnitude>50)
            {
                if (Mathf.Abs(Dir.x)>Mathf.Abs(Dir.y)&&Dir.x>0)
                {
                    m_inputDir = InputDirection.RIGHT;
                }
                else if (Mathf.Abs(Dir.x) < Mathf.Abs(Dir.y) && Dir.y > 0)
                {
                    m_inputDir = InputDirection.UP;
                }
                else if (Mathf.Abs(Dir.x) > Mathf.Abs(Dir.y) && Dir.x < 0)
                {
                    m_inputDir = InputDirection.LEFT;
                }
                else if (Mathf.Abs(Dir.x) < Mathf.Abs(Dir.y) && Dir.y < 0)
                {
                    m_inputDir = InputDirection.DOWN;
                }
                activeInput = false;
            }
            
        }

        //识别键盘操作
        if (Input.GetKeyDown(KeyCode.W)||Input.GetKeyDown(KeyCode.Space))
        {
            m_inputDir = InputDirection.UP;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            m_inputDir = InputDirection.LEFT;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            m_inputDir = InputDirection.RIGHT;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            m_inputDir = InputDirection.DOWN;
        }
        Debug.Log(m_inputDir);
    }

    private void UpdatePosition()
    {
        GetDirection();
        switch (m_inputDir)
        {
            case InputDirection.NULL:
                break;
            case InputDirection.UP:
                if (m_cc.isGrounded)
                {
                    m_ydistance = JumpHight;
                    SendMessage("AnimationManager", m_inputDir);
                    Game.Instance.sound.PlayEffect("Se_UI_Jump");
                }
                break;
            case InputDirection.DOWN:
                if (m_IsSlide ==false)
                {
                    m_IsSlide = true;
                    SendMessage("AnimationManager", m_inputDir);
                    Game.Instance.sound.PlayEffect("Se_UI_Slide");
                }
                break;
            case InputDirection.LEFT:
                if (m_targetIndex>0)
                {
                    m_targetIndex--;
                    m_xdistance = -2;
                    SendMessage("AnimationManager", m_inputDir);
                    Game.Instance.sound.PlayEffect("Se_UI_Huadong");
                }
                break;
            case InputDirection.RIGHT:
                if (m_targetIndex<2)
                {
                    m_targetIndex++;
                    m_xdistance = 2;
                    SendMessage("AnimationManager", m_inputDir);
                    Game.Instance.sound.PlayEffect("Se_UI_Huadong");
                }
                break;
            default:
                break;
        }
    }

    private void UpdateSpeed()
    {
        RunDis += Speed * Time.deltaTime;
        if (RunDis>SpeedAddDis)
        {
            RunDis = 0;
            Speed += SpeedAddRate;
        }
    }

    private void MoveControl()
    {
        //控制水平移动
        if (m_nowIndex!= m_targetIndex)
        {
            float move = Mathf.Lerp(0, m_xdistance, m_moveSpeed * Time.deltaTime);
            transform.position += new Vector3(move, 0, 0);
            m_xdistance -= move;
            if (Mathf.Abs(m_xdistance)<0.05f)
            {
                m_nowIndex = m_targetIndex;
                m_xdistance = 0;

                switch (m_targetIndex)
                {
                    case 0:
                        transform.position = new Vector3(-2, transform.position.y, transform.position.z);
                        break;
                    case 1:
                        transform.position = new Vector3(0, transform.position.y, transform.position.z);
                        break;
                    case 2:
                        transform.position = new Vector3(2, transform.position.y, transform.position.z);
                        break;
                    default:
                        break;
                }
            }
        }

        //Slide计时
        if (m_IsSlide)
        {
            m_SlideTimer -= Time.deltaTime;
            if (m_SlideTimer<0)
            {
                m_SlideTimer = 0;
                m_IsSlide = false;
            }
        }
    }
    #endregion

    #region 方法

    //减速（被撞）
    public void HitObstacles()
    {
        if (m_IsHit)
        {
            return;
        }
        m_IsHit = true;
        m_MaskSpeed = Speed;
        Speed = 0;
        StartCoroutine(DecreaseSpeed());
    }

    //恢复速度
    IEnumerator DecreaseSpeed()
    {
        while (Speed<=m_MaskSpeed)
        {
            Speed += m_AddRate * Time.deltaTime;
            yield return 0;
        }
        m_IsHit = false;
    }

    //吃金币
    public void HitCoin()
    {
        //Debug.Log("HitCoin");
        CoinArgs e = new CoinArgs { Coin = m_DoubleTime };
        SendEvent(Const.E_UpdateCoin, e);
    }

    //双倍金币
    public void HitDouble()
    {
        if (Multiplycor!=null)
        {
            StopCoroutine(Multiplycor);
        }
        Multiplycor = MultiplyCoroutine();
        StartCoroutine(Multiplycor);
    }

    IEnumerator MultiplyCoroutine()
    {
        m_DoubleTime = 2;
        float Timer = m_SkillTime;
        while (Timer > 0)
        {
            if (!gm.IsPause && gm.IsPlay)
            {
                Timer -= Time.deltaTime;
            }
            yield return 0;
            //print(Timer);
        }
        //yield return new WaitForSeconds(m_SkillTime);
        m_DoubleTime = 1;
    }

    //吸铁石

    public void HitMagnet()
    {
        if (Magnetcor!=null)
        {
            StopCoroutine(Magnetcor);
        }
        Magnetcor = MagnetCoroutine();
        StartCoroutine(Magnetcor);
    }

    IEnumerator MagnetCoroutine()
    {
        m_MagnetCollier.enabled = true;
        float Timer = m_SkillTime;
        while (Timer > 0)
        {
            if (!gm.IsPause && gm.IsPlay)
            {
                Timer -= Time.deltaTime;
            }
            yield return 0;
        }
       // yield return new WaitForSeconds(m_SkillTime);
        m_MagnetCollier.enabled = false;
    }

    //加时间

    public void HitAddTime()
    {
        //Debug.Log("Add Time");
        SendEvent(Const.E_HitAddtime);
    }

    //无敌

    public  void HitInvincible()
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
        m_IsInvincible = true;
        float Timer = m_SkillTime;
        while (Timer>0)
        {
            if (!gm.IsPause&&gm.IsPlay)
            {
                Timer -= Time.deltaTime;
            }
            yield return 0;
        }
        //yield return new WaitForSeconds(m_SkillTime);
        m_IsInvincible = false;
    }


    public void HitItem(ItemKind item)
    {
        ItemArgs e = new ItemArgs
        {
            itemkind = item,
            itemCount = 0
        };

        SendEvent(Const.E_HitItem, e);

        //switch (item)
        //{
        //    case ItemKind.InvincibleItem:
        //        HitInvincible();
        //        break;
        //    case ItemKind.MagnetItem:
        //        HitMagnet();
        //        break;
        //    case ItemKind.MultiplyItem:
        //        HitDouble();
        //        break;
        //    default:
        //        break;
        //}
    }

    public  void OnGoalClick()
    {
        if (Goalcor != null)
        {
            StopCoroutine(Goalcor);
        }
        SendMessage("MassageShoot");
        m_Ball.gameObject.SetActive(false);
        m_trail.gameObject.SetActive(true);
        Goalcor = MoveBall();
        StartCoroutine(Goalcor);
    }

    IEnumerator MoveBall()
    {
        while (true)
        {
            if (!gm.IsPause && gm.IsPlay)
            {
                m_trail.transform.Translate(transform.forward * 100 * Time.deltaTime);
            }
            yield return 0;
        }
    }

    //球进了
    public void HitBallDoor()
    {
        //1，停止协程
        StopCoroutine(Goalcor);

        //2，归位
        m_trail.transform.localPosition = new Vector3(0,1,4);
        m_trail.SetActive(false);
        m_Ball.SetActive(true);
        //3，isgoal 变为 true
        isGoal = true;

        //4，生成特效
        Game.Instance.objectPool.Spawn("FX_GOAL", m_trail.gameObject. transform.parent);

        //5,播放音效
        Game.Instance.sound.PlayEffect("Se_UI_Goal");

        //6,发送事件 goal +1
        SendEvent(Const.E_ShootGoal);
    }

    #endregion

    #region Unity回调

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == Tag.smallfence)
        {
            if (m_IsInvincible)
                return;
            other.gameObject.SendMessage("HitPlayer", transform.position,SendMessageOptions.RequireReceiver);
            //声音
            Game.Instance.sound.PlayEffect("Se_UI_Hit");
            HitObstacles();
        }

        else if (other.gameObject.tag == Tag.bigfence)
        {
            if (m_IsSlide)
                return;
            if (m_IsInvincible)
                return;
            other.gameObject.SendMessage("HitPlayer", transform.position,SendMessageOptions.RequireReceiver);
            //声音
            Game.Instance.sound.PlayEffect("Se_UI_Hit");
            HitObstacles();
        }
        else if (other.gameObject.tag == Tag.block)
        {
            other.gameObject.SendMessage("HitPlayer", transform.position, SendMessageOptions.RequireReceiver);
            //声音
            Game.Instance.sound.PlayEffect("Se_UI_End");
            HitObstacles();

            //sendevent 游戏结束
            SendEvent(Const.E_EndGame);
        }
        else if (other.gameObject.tag == Tag.smallblock)
        {
            other.gameObject.transform.parent.parent.SendMessage("HitPlayer", transform.position, SendMessageOptions.RequireReceiver);
            //声音
            Game.Instance.sound.PlayEffect("Se_UI_End");
            HitObstacles();

            //sendevent 游戏结束
            SendEvent(Const.E_EndGame);
        }
        else if (other.gameObject.tag == Tag.beforetrigger)//汽车移动
        {
            other.gameObject.transform.parent.SendMessage("HitTrigger", SendMessageOptions.RequireReceiver);

        }
        else if (other.gameObject.tag == Tag.beforeGoalTrigger)//准备射门
        {
            SendEvent(Const.E_HitGoalTrigger);

            //播放加速特效
            Game.Instance.objectPool.Spawn("FX_JiaSu", m_trail.gameObject.transform.parent);
        }
        else if (other.gameObject.tag == Tag.goalkeeper)//撞到守门员
        {
            HitObstacles();
            //发消息让守门员飞走
            other.transform.parent.parent.parent.SendMessage("HitGoalkeeper");
        }
        else if (other.gameObject.tag == Tag.ballDoor)//撞到球门
        {
            if (isGoal)
            {
                isGoal = false;
                return;
            }
            //1,减速
            HitObstacles();
            //2,球网挂在身上
            Game.Instance.objectPool.Spawn("Effect_QiuWang", m_trail.gameObject.transform.parent);
            //3,发消息，让球门播放动画，球网消失
            other.transform.parent.parent.SendMessage("HitDoor",m_nowIndex);
        }
    }

    private void Awake()
    {
        m_cc = gameObject.GetComponent<CharacterController>();
        gm = GetModel<GameModel>();
        m_SkillTime = gm.SkillTime;
        m_MagnetCollier = gameObject.GetComponentInChildren<SphereCollider>();
        m_MagnetCollier.enabled = false;

        //射门相关
        m_Ball = transform.Find("Ball").gameObject;
        m_trail = GameObject.Find("Trail").gameObject;
        m_trail.gameObject.SetActive(false);

        ballMesh.material = Game.Instance.staticData.GetFootballInfo(gm.TakeonFootball).FootballMaterial;
        SkinMeshRender.material = Game.Instance.staticData.GetPlayerSkinInfo(gm.TakeonSkin.SkinID, gm.TakeonSkin.ClothID).FootballMaterial;
    }
    private void Start()
    {
        StartCoroutine(UpdateAction());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            gm.IsPause = true;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            gm.IsPause = false;
        }
    }

    #endregion

    #region 事件回调
    public override void HandleEvent(string eventName, object data)
    {
        switch (eventName)
        {
            case Const.E_GoalBtnClick:
                OnGoalClick();
                break;
        }
    }

    public override void RegisterAttentionList()
    {
        AttentionList.Add(Const.E_GoalBtnClick);
    }

    #endregion

    #region 帮助方法
    #endregion


}
