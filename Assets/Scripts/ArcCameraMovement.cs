using UnityEngine;

public class ArcCameraMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public Transform pointA;      // 起点
    public Transform pointB;      // 终点
    public Transform lookAt;      // 相机注视的目标
    public float height = 5f;     // 弧线的高度
    public float duration = 2f;   // 完成一次运动的时间

    [Header("Motion Control")]
    public AnimationCurve movementCurve = AnimationCurve.EaseInOut(0, 0, 1, 1); // 控制运动速度的曲线

    private float timer;          // 计时器
    private bool moving = false;  // 是否开始运动
    private bool movingForward = true; // 控制运动方向
    private bool paused = false;  // 是否暂停

    void Start()
    {
        if (!pointA || !pointB || !lookAt)
        {
            Debug.LogError("Please assign PointA, PointB, and LookAt transforms.");
        }

        StartMoving();
    }

    void Update()
    {
        // 检测ESC键退出游戏
        if (Input.GetKeyDown(KeyCode.Escape))
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false; // 在编辑器中停止播放
#else
        Application.Quit(); // 在打包后的游戏中退出
#endif
        }
        // 检测按键切换暂停状态
        if (Input.GetKeyDown(KeyCode.P))
        {
            paused = !paused; // 切换暂停状态
        }

        if (moving && !paused)
        {
            timer += Time.deltaTime;

            // 根据曲线调整插值值
            float t = Mathf.Clamp01(timer / duration);
            float curvedT = movementCurve.Evaluate(t);

            // 根据运动方向调整插值
            if (!movingForward)
            {
                curvedT = 1 - curvedT; // 反向插值
            }

            // 计算弧形插值位置
            Vector3 start = pointA.position;
            Vector3 end = pointB.position;
            Vector3 mid = (start + end) / 2 + Vector3.up * height; // 弧顶点

            // 使用二次贝塞尔曲线计算位置
            Vector3 position = (1 - curvedT) * (1 - curvedT) * start
                             + 2 * (1 - curvedT) * curvedT * mid
                             + curvedT * curvedT * end;

            // 更新相机位置
            transform.position = position;

            // 相机注视目标
            transform.LookAt(lookAt);

            // 停止或切换方向
            if (t >= 1f)
            {
                timer = 0f;
                movingForward = !movingForward; // 反转方向
            }
        }
    }

    // 开始运动
    public void StartMoving()
    {
        timer = 0f;
        moving = true;
        movingForward = true; // 初始化为正向
    }

    // 可视化辅助工具
    private void OnDrawGizmos()
    {
        if (pointA && pointB)
        {
            Gizmos.color = Color.green;

            // 绘制弧线的可视化
            Vector3 start = pointA.position;
            Vector3 end = pointB.position;
            Vector3 mid = (start + end) / 2 + Vector3.up * height;
            for (float t = 0; t < 1f; t += 0.05f)
            {
                Vector3 p1 = (1 - t) * (1 - t) * start + 2 * (1 - t) * t * mid + t * t * end;
                Vector3 p2 = (1 - (t + 0.05f)) * (1 - (t + 0.05f)) * start + 2 * (1 - (t + 0.05f)) * (t + 0.05f) * mid + (t + 0.05f) * (t + 0.05f) * end;
                Gizmos.DrawLine(p1, p2);
            }
        }
    }
}
