using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class CellGrid : MonoBehaviour
{
    private int _cellWidth;
    private int _cellHeight;
    private int _num;

    private UIWidget widget; // 可设置格子宽高
    public UILabel label; // 可设置格子中的文本
    //public NGUIText text;
    private string bgColor;

    public int Num { get => _num; set => _num = value;}
    public int CellWidth { get => _cellWidth; set => _cellWidth = value; }
    public int CellHeight { get => _cellHeight; set => _cellHeight = value; }

    public bool CanMerge = true;

    private void Awake()
    {
        widget = this.GetComponent<UIWidget>();
        label = this.GetComponentInChildren<UILabel>();
        label.text = ""; // 初始所有格子没有数字
        //Assert.IsNotNull(label, "");
        //Debug.Log(GetComponent<UISprite>().color); 
    }

    private void Start()
    {
        widget.width = Constant.cellSize;
        widget.width = Constant.cellSize;
        //Debug.Log("height  " + this.GetComponent<UIWidget>().width);
        //Debug.Log("Num  " + int.Parse(GetComponentInChildren<UILabel>().text));
    }

    public void ClearNumber()
    {
        label.text = "";
    }

    public void SetNumber(string num)
    {
        label.text = num;
    }

    public string GetNumber()
    {
        return label.text;
    }

    public void doubleNumber(string num)
    {
        label.text = (int.Parse(num) * 2).ToString();
    }
}
