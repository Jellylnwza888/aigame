using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class playercontrol : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float sprintMultiplier = 1.5f;
    public Joystick joystick; // ตัวแปรสำหรับเก็บภาพจอยสติ๊ก

    void Update()
    {
        // เช็คว่ากด Shift เพื่อวิ่งหรือไม่
        float speed = moveSpeed * (Input.GetKey(KeyCode.LeftShift) ? sprintMultiplier : 1f);
        // รับค่าการบังคับด้วย WASD
        float moveX = joystick.Horizontal; 
        float moveZ = joystick.Vertical; 

        Vector3 move = new Vector3(moveX, 0f, moveZ).normalized;

        // เดิน
        transform.Translate(move * speed * Time.deltaTime);
    }


}


