using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GetMoveType : MonoBehaviour
{
    private bool enabled = false; // �����Ƿ�����OnDrag

    public void OnPress(bool IsDown)
    {
        if (IsDown) enabled = true; // ÿ����갴������OnDrag
    }

    public void OnDrag(Vector2 delta)
    {
        if (!enabled) return; // ��û���ã��򷵻�

        if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y)) // ˮƽ���򻬶�
            SendMessageUpwards(delta.x > 0 ? "MoveToRight" : "MoveToLeft");
        else
            SendMessageUpwards(delta.y > 0 ? "MoveToUp" : "MoveToDown");

        enabled = false; // ����OnDrag
    }
}
