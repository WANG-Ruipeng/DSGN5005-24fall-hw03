using UnityEngine;

public class ArcCameraMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public Transform pointA;      // ���
    public Transform pointB;      // �յ�
    public Transform lookAt;      // ���ע�ӵ�Ŀ��
    public float height = 5f;     // ���ߵĸ߶�
    public float duration = 2f;   // ���һ���˶���ʱ��

    [Header("Motion Control")]
    public AnimationCurve movementCurve = AnimationCurve.EaseInOut(0, 0, 1, 1); // �����˶��ٶȵ�����

    private float timer;          // ��ʱ��
    private bool moving = false;  // �Ƿ�ʼ�˶�
    private bool movingForward = true; // �����˶�����
    private bool paused = false;  // �Ƿ���ͣ

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
        // ���ESC���˳���Ϸ
        if (Input.GetKeyDown(KeyCode.Escape))
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false; // �ڱ༭����ֹͣ����
#else
        Application.Quit(); // �ڴ�������Ϸ���˳�
#endif
        }
        // ��ⰴ���л���ͣ״̬
        if (Input.GetKeyDown(KeyCode.P))
        {
            paused = !paused; // �л���ͣ״̬
        }

        if (moving && !paused)
        {
            timer += Time.deltaTime;

            // �������ߵ�����ֵֵ
            float t = Mathf.Clamp01(timer / duration);
            float curvedT = movementCurve.Evaluate(t);

            // �����˶����������ֵ
            if (!movingForward)
            {
                curvedT = 1 - curvedT; // �����ֵ
            }

            // ���㻡�β�ֵλ��
            Vector3 start = pointA.position;
            Vector3 end = pointB.position;
            Vector3 mid = (start + end) / 2 + Vector3.up * height; // ������

            // ʹ�ö��α��������߼���λ��
            Vector3 position = (1 - curvedT) * (1 - curvedT) * start
                             + 2 * (1 - curvedT) * curvedT * mid
                             + curvedT * curvedT * end;

            // �������λ��
            transform.position = position;

            // ���ע��Ŀ��
            transform.LookAt(lookAt);

            // ֹͣ���л�����
            if (t >= 1f)
            {
                timer = 0f;
                movingForward = !movingForward; // ��ת����
            }
        }
    }

    // ��ʼ�˶�
    public void StartMoving()
    {
        timer = 0f;
        moving = true;
        movingForward = true; // ��ʼ��Ϊ����
    }

    // ���ӻ���������
    private void OnDrawGizmos()
    {
        if (pointA && pointB)
        {
            Gizmos.color = Color.green;

            // ���ƻ��ߵĿ��ӻ�
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
