using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public Transform character; // Tham chiếu đến transform của nhân vật

    void Update()
    {
        // Tính toán vị trí đối xứng của cái gương
        Vector3 mirrorPosition = new Vector3(character.position.x, character.position.y, character.position.z);
        mirrorPosition.x = -mirrorPosition.x; // Đổi chiều trục x

        // Cập nhật vị trí của cái gương
        transform.position = mirrorPosition;
    }
}
