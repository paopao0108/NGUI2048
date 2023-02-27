using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GetMoveType : MonoBehaviour
{
    private bool enabled = false; // 控制是否启用OnDrag

    public void OnPress(bool IsDown)
    {
        if (IsDown) enabled = true; // 每次鼠标按下启用OnDrag
    }

    public void OnDrag(Vector2 delta)
    {
        if (!enabled) return; // 若没启用，则返回

        if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y)) // 水平方向滑动
            SendMessageUpwards(delta.x > 0 ? "MoveToRight" : "MoveToLeft");
        else
            SendMessageUpwards(delta.y > 0 ? "MoveToUp" : "MoveToDown");

        enabled = false; // 禁用OnDrag
    }
}
