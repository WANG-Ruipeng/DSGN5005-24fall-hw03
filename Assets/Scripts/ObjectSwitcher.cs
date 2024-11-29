using UnityEngine;

public class ObjectSwitcher : MonoBehaviour
{
    [Header("Object References")]
    public GameObject object1; // 对象1
    public GameObject object2; // 对象2
    public GameObject object3; // 对象3
    public GameObject object4; // 对象4

    [Header("Skybox Colors")]
    public Color color1 = Color.red;    // 对应对象1的天空盒颜色
    public Color color2 = Color.green;  // 对应对象2的天空盒颜色
    public Color color3 = Color.blue;   // 对应对象3的天空盒颜色
    public Color color4 = Color.yellow; // 对应对象4的天空盒颜色

    private Camera mainCamera; // 主相机

    void Start()
    {
        // 获取 MainCamera
        mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("Main Camera not found. Please tag your camera as 'MainCamera'.");
            return;
        }

        // 确保至少一个对象启用，并初始化天空盒颜色
        if (object1 != null) SetActiveObject(1);
    }

    void Update()
    {
        // 检测按键1、2、3、4
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
        // 激活指定对象，禁用其他对象
        if (object1 != null) object1.SetActive(index == 1);
        if (object2 != null) object2.SetActive(index == 2);
        if (object3 != null) object3.SetActive(index == 3);
        if (object4 != null) object4.SetActive(index == 4); 

        // 更改相机天空盒颜色
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