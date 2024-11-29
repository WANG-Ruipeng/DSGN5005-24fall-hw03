using UnityEngine;

public class ObjectSwitcher : MonoBehaviour
{
    [Header("Object References")]
    public GameObject object1; // ����1
    public GameObject object2; // ����2
    public GameObject object3; // ����3
    public GameObject object4; // ����4

    [Header("Skybox Colors")]
    public Color color1 = Color.red;    // ��Ӧ����1����պ���ɫ
    public Color color2 = Color.green;  // ��Ӧ����2����պ���ɫ
    public Color color3 = Color.blue;   // ��Ӧ����3����պ���ɫ
    public Color color4 = Color.yellow; // ��Ӧ����4����պ���ɫ

    private Camera mainCamera; // �����

    void Start()
    {
        // ��ȡ MainCamera
        mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("Main Camera not found. Please tag your camera as 'MainCamera'.");
            return;
        }

        // ȷ������һ���������ã�����ʼ����պ���ɫ
        if (object1 != null) SetActiveObject(1);
    }

    void Update()
    {
        // ��ⰴ��1��2��3��4
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetActiveObject(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetActiveObject(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SetActiveObject(3);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SetActiveObject(4);
        }
    }

    private void SetActiveObject(int index)
    {
        // ����ָ�����󣬽�����������
        if (object1 != null) object1.SetActive(index == 1);
        if (object2 != null) object2.SetActive(index == 2);
        if (object3 != null) object3.SetActive(index == 3);
        if (object4 != null) object4.SetActive(index == 4); 

        // ���������պ���ɫ
        UpdateSkyboxColor(index);
    }

    private void UpdateSkyboxColor(int index)
    {
        if (mainCamera == null) return;

        switch (index)
        {
            case 1:
                mainCamera.backgroundColor = color1;
                break;
            case 2:
                mainCamera.backgroundColor = color2;
                break;
            case 3:
                mainCamera.backgroundColor = color3;
                break;
            case 4:
                mainCamera.backgroundColor = color4;
                break;
        }
    }
}