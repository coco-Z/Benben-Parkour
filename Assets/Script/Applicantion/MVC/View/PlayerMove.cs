using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Consts;

public class PlayerMove : View
{


    #region 常量
    #endregion

    #region 事件
    #endregion

    #region 字段
    [Header("Character")]
    public float speed = 20.0f;
    public float landDistance = 2.0f;// 每条车道之间的距离
    public int currentLane = 0;// 当前所在车道（-1表示最左边，0表示中间，1表示最右边）
    public float gravityValue = -9.8f;
    public float jumpHeight = 1.0f;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    [Header("Speed")]
    public float speedAddDis = 100.0f;
    public float MaxspeedAdd = 60.0f;
    public float speedAdd = 10.0f;
    public int TimeADDNum = 20;
    [Header("Goal")]
    public GameObject ball;
    public GameObject flyBall;
    public GameObject flyBallLocation;
    public ParticleSystem ballPar;
    public GameObject Effect_QiuWang;
    [Header("material")]
    public SkinnedMeshRenderer playerSkml;
    public Material material1;
    public Material material2;
    public Material material3;

    private CharacterController m_cc;
    private InputDirection m_inputDir = InputDirection.Null;
    private bool activeInput = false;
    private Vector3 m_mousePos;
    private bool m_isGround = true;
    private Vector3 playerVelocity;
    private bool m_IsSlide = false;
    private float Distance = 0.0f;
    private GameModel gm;
    private bool m_IsHit = false;
    private float m_MaskSpeed = 0;
    private float m_AddRate = 10;
    private int m_DoubleTime = 1;//是否双倍金币
    private float m_SkillTime =5;//道具持续时间
    private GameObject m_MagnetCollider;
    private bool m_ISInvincible = false;
    //协程
    IEnumerator MultiplyCor;
    IEnumerator MagnetCor;
    IEnumerator InvincibleCor;
    IEnumerator GoalCor;
    #endregion

    #region 属性
    public override string Name
    {
        get { return Consts.V_PlayerMove; }
    }
    #endregion

    #region 方法
    //捕捉按键
    private void GetInputDirection()
    {
        m_inputDir = InputDirection.Null;
        if (Input.GetMouseButtonDown(0))
        {
            activeInput = true;
            m_mousePos = Input.mousePosition;
        }

        if (Input.GetMouseButton(0) && activeInput)
        {
            //手机
            Vector3 Dir = Input.mousePosition - m_mousePos;
            if (Dir.magnitude > 20)
            {
                if (Mathf.Abs(Dir.x) > Mathf.Abs(Dir.y) && Dir.x > 0)
                {
                    m_inputDir = InputDirection.Right;
                }
                else if (Mathf.Abs(Dir.x) > Mathf.Abs(Dir.y) && Dir.x < 0)
                {
                    m_inputDir = InputDirection.Left;
                }
                else if (Mathf.Abs(Dir.x) < Mathf.Abs(Dir.y) && Dir.y > 0)
                {
                    m_inputDir = InputDirection.Up;
                }
                else if (Mathf.Abs(Dir.x) < Mathf.Abs(Dir.y) && Dir.y < 0)
                {
                    m_inputDir = InputDirection.Down;
                }
                activeInput = false;
            }
        }

        //电脑
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space))
        {
            m_inputDir = InputDirection.Up;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            m_inputDir = InputDirection.Down;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            m_inputDir = InputDirection.Right;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            m_inputDir = InputDirection.Left;
        }

    }
    // 更新位置
    private void UpdatePosition()
    {
        switch (m_inputDir)
        {
            case InputDirection.Null:
                break;
            case InputDirection.Left:
                MoveLane(false);// 向左移动车道
                SendMessage("AnimManager", m_inputDir);
                Game.Instance.sound.PlayEffect("Se_UI_Huadong");
                break;
            case InputDirection.Right:
                MoveLane(true);// 向右移动车道
                SendMessage("AnimManager", m_inputDir);
                Game.Instance.sound.PlayEffect("Se_UI_Huadong");
                break;
            case InputDirection.Up:
                if (m_isGround)
                {
                    // 如果玩家按下跳跃键并且在地面上，则施加跳跃力
                    playerVelocity.y = playerVelocity.y + Mathf.Sqrt(jumpHeight * -3 * gravityValue);
                    SendMessage("AnimManager", m_inputDir);
                    Game.Instance.sound.PlayEffect("Se_UI_Slide");
                }
                break;
            case InputDirection.Down:
                if (!m_IsSlide)
                {
                    m_IsSlide = true;
                    Invoke("SlideEnd", 0.73f);
                    SendMessage("AnimManager", m_inputDir);
                    Game.Instance.sound.PlayEffect("Se_UI_Jump");
                }
                break;
            default:
                break;
        }
    }

    //移动
    private void DoMove()
    {
        //isGround = m_cc.isGrounded;// 检测玩家是否在地面上

        UpDateSpeed();

        m_isGround = Physics.OverlapSphere(groundCheck.position, groundCheckRadius, groundLayer).Length > 0;

        if (m_isGround && playerVelocity.y < 0)// 如果玩家在地面上并且垂直速度小于0，则重置垂直速度
        {
            playerVelocity.y = 0;
        }

        float targetX = currentLane * landDistance;// 计算目标位置的X坐标
        Vector3 targetPosition = new Vector3(targetX, transform.position.y, transform.position.z);// 计算目标位置

        Vector3 moveDirection = targetPosition - transform.position;// 计算移动方向
        m_cc.Move((moveDirection + transform.forward) * speed * Time.deltaTime);// 执行移动

        // 应用重力
        playerVelocity.y = playerVelocity.y + gravityValue * Time.deltaTime;
        m_cc.Move(playerVelocity * Time.deltaTime);
    }
    private void MoveLane(bool right)
    {
        currentLane = currentLane + (right ? 1 : -1);// 更新当前车道
        currentLane = Mathf.Clamp(currentLane, -1, 1);// 确保当前车道在合法范围内
    }
    private void SlideEnd()
    {
        m_IsSlide = false;
    }

    //递增速度
    private void UpDateSpeed()
    {
        if (speed < MaxspeedAdd)
        {
            Distance = Distance + speed * Time.deltaTime;
            if (Distance > speedAddDis)
            {
                Distance = 0;
                speed = speed + speedAdd;
            }
        }
    }

    //障碍物
    private void HitObstacles(Collider other)
    {
        if(other.gameObject.tag == "SmallFence")
        {
            if(m_ISInvincible){ return; }
            other.gameObject.SendMessage("HitPlayer", transform.position);
            Game.Instance.sound.PlayEffect("Se_UI_Hit");

            HitSpeed();
        }
        else if (other.gameObject.tag =="BigFence")
        {
            if (m_IsSlide){ return; }
            if (m_ISInvincible) { return; }
            other.gameObject.SendMessage("HitPlayer", transform.position);

            Game.Instance.sound.PlayEffect("Se_UI_Hit");

            HitSpeed();
        }
        else if (other.gameObject.tag == "Block")
        {
            other.gameObject.SendMessage("HitPlayer", transform.position);

            Game.Instance.sound.PlayEffect("Se_UI_End");

            SendEvent(Consts.E_EndGame);
        }
        else if (other.gameObject.tag == "SmallBlock")
        {
            other.transform.parent.parent.SendMessage("HitPlayer", transform.position);

            Game.Instance.sound.PlayEffect("Se_UI_End");

            SendEvent(Consts.E_EndGame);
        }
        else if (other.gameObject.tag == "BeforeTrigger")
        {
            other.transform.parent.SendMessage("HitTrigger", SendMessageOptions.RequireReceiver);
        }
        else if(other.gameObject.tag == "BeforeGoalTrigger")
        {
            //UI表现
            SendEvent(Consts.E_HitGoalTrigger);
        }
        else if(other.gameObject.tag == "Goalkeeper")
        {
            HitSpeed();
            other.transform.parent.parent.parent.SendMessage("HitGoalKeeper",SendMessageOptions.RequireReceiver);
        }
        else if(other.gameObject.tag == "BallDoor")
        {
            HitSpeed();
            Effect_QiuWang.SetActive(true);
            Invoke("HideQiuWang", 3.0f);
        }
    }
    private void HitSpeed()
    {
        if (m_IsHit) { return; }

        m_IsHit = true;

        m_MaskSpeed = speed;

        speed = 0;

        StartCoroutine(DecreaseSpeed());
    }
    IEnumerator DecreaseSpeed()
    {
        while(speed<=m_MaskSpeed)
        {
            speed = speed + Time.deltaTime* m_AddRate;
            yield return 0;
        }
        m_IsHit = false;
    }


    //金币
    public void HitCoin()
    {
        //Debug.Log("吃到金币了");
        SendEvent(Consts.E_UpdateCoin, m_DoubleTime);
    }

    public void HitItem(ItemKind item)
    {
        ItemArgs e = new ItemArgs
        {
            hitCount = 0,
            kind = item
        };
        SendEvent(Consts.E_HitItem,e);
    }

    //双倍金币
    public void HitMultiply()
    {
        //Debug.Log("双倍金币");
        if (MultiplyCor !=null)
        {
            StopCoroutine(MultiplyCor);
        }
        MultiplyCor = MultiplyCor2();
        StartCoroutine(MultiplyCor);

    }
    IEnumerator MultiplyCor2()
    {
        m_DoubleTime = 2;
        float timer = m_SkillTime;
        while(timer > 0)
        {
            if(!gm.IsPause&&gm.Isplay)
            {
                timer= timer - Time.deltaTime;
            }
            yield return 0;
        }
        m_DoubleTime = 1;
    }

    //磁铁
    public void HitMagnet()
    {
        if(MagnetCor !=null)
        {
            StopCoroutine(MagnetCor);
        }
        MagnetCor = MagnetCor2();
        StartCoroutine (MagnetCor);
    }
    IEnumerator MagnetCor2()
    {
        m_MagnetCollider.SetActive(true);
        float timer = m_SkillTime;
        while (timer > 0)
        {
            if (!gm.IsPause && gm.Isplay)
            {
                timer = timer - Time.deltaTime;
            }
            yield return 0;
        }
        m_MagnetCollider.SetActive(false);
    }

    //增加时间
    public void HitAddTime()
    {
        /*Debug.Log("增加时间");*/
        SendEvent(Consts.E_ADDTime, TimeADDNum);
    }

    //无敌效果
    public void HitInvincible()
    {
        if (InvincibleCor !=null)
        {
            StopCoroutine(InvincibleCor);
        }
        InvincibleCor = InvincibleCor2();
        StartCoroutine(InvincibleCor);
    }
    IEnumerator InvincibleCor2()
    {
        m_ISInvincible = true;
        float timer = m_SkillTime;
        while (timer > 0)
        {
            if (!gm.IsPause && gm.Isplay)
            {
                timer = timer - Time.deltaTime;
            }
            yield return 0;
        }
        m_ISInvincible = false;
    }

    public void UpDateDis()
    {
        SendEvent(Consts.E_UpdateDis,(int)transform.position.z);
    }

    public void OnGoalClick()
    {
        if (GoalCor != null)
        {
            StopCoroutine(GoalCor);
        }
        SendMessage("PlayShootMessage");
        ball.SetActive(false);
        flyBall.SetActive(true);
        GoalCor = MoveBall();
        StartCoroutine(GoalCor);
        SendEvent(Consts.E_UpdateGoal, 1);
    }

    IEnumerator MoveBall()
    { 
        while(true)
        {
            if(gm.Isplay&&!gm.IsPause)
            {
                flyBall.transform.Translate(transform.forward * Time.deltaTime * 40.0f);
            }
            yield return 0;
        }       
    }

    public void HitBallDoor()
    {
        StopCoroutine(GoalCor);
        flyBall.transform.position = flyBallLocation.transform.position;
        flyBall.SetActive(false);
        ball.SetActive(true);
        ballPar.Play();
        Game.Instance.sound.PlayEffect("Se_UI_goal");
    }

    public void HideQiuWang()
    {
        Effect_QiuWang.SetActive(false);
    }

    #endregion

    #region Unity回调
    private void Awake()
    {
        groundCheck = GameObject.FindWithTag("ISGround").transform;
        m_cc = GetComponent<CharacterController>();
        //游戏模式
        gm = GetModel<GameModel>();
        m_SkillTime = gm.SkillTime;
        //磁力启用
        m_MagnetCollider = GameObject.FindWithTag("MagnetCollider");
        m_MagnetCollider.SetActive(false);

        ballPar.Stop();
    }
    private void Start()
    {
        if (gm.clothe1fit)
        {
            playerSkml.material = material1;
        }
        else if (gm.clothe2fit)
        {
            playerSkml.material = material2;
        }
        else if (gm.clothe3fit)
        {
            playerSkml.material = material3;
        }
    }

    private void Update()
    {
        if (!gm.IsPause && gm.Isplay)
        {
            GetInputDirection();
            UpdatePosition();
            DoMove();
            UpDateDis();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        HitObstacles(other);
    }
    #endregion

    #region 事件回调
    public override void HandleEvent(string name, object data)
    {
        switch (name)
        {
            case Consts.E_ClickGoalButton:
                OnGoalClick();
                break;
        }
    }
    public override void RegisterAttentionEvent()
    {
        AttentionList.Add(Consts.E_ClickGoalButton);
    }
    #endregion

    #region 帮助方法
    #endregion






    
    
}
