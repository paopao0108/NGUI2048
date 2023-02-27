using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Game : MonoBehaviour
{
    private List<List<CellGrid>> grids = new List<List<CellGrid>>(); // 所有格子
    private List<CellGrid> canCreateNumberGrids = new List<CellGrid>(); // 可以产生数字的格子

    public CellGrid cellGrid; // 存储单元格预制体
    public UITable gridTable; // 存放所有格子
    public int row = 4; // TODO
    


    private void Awake()
    {
        gridTable = this.GetComponentInChildren<UITable>();
        //gridTable = this.GetComponent<UITable>();
        gridTable.columns = row;
        Debug.Log("Table:  " + gridTable.columns);

        // 初始化格子
        CreateEmptyGrid();
        // 产生一个数字
        CreateNumber();
    }

    private void Start()
    {

        //Debug.Log(moveType.moveType);
    }

    private void Update()
    {
        //Debug.Log(moveType);
        //Debug.Log(moveType.moveType);

    }

    public void CreateEmptyGrid()
    {
        for (int i = 0; i < row; i++)
        {
            grids.Add(new List<CellGrid>());
            for (int j = 0; j < row; j++)
                grids[i].Add(Instantiate(cellGrid, gridTable.transform)); // 实例化预制体 并放入grids二维列表中
        }
    }

    public void CreateNumber()
    {
        canCreateNumberGrids = new List<CellGrid>(); // 每次计算可产生数字的格子都需要重新计算
        // 没有数字的格子可以产生数字
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < row; j++)
            {
                grids[i][j].CanMerge = true;
                if (grids[i][j].GetNumber() == "") canCreateNumberGrids.Add(grids[i][j]); // 若当前格子没有数字，那么添加到列'
                //else grids[i][j].CanMerge = true; // 有数字的格子在产生新数字时都可以合并
            }
        }

        Debug.Log("数量：" + canCreateNumberGrids.Count);
        if (canCreateNumberGrids.Count == 0) // 无法再产生数字
        {
            if (!IsExistMerge()) Debug.Log("游戏失败！！！");
            return;
        }
        int randomIndex = Random.Range(0, canCreateNumberGrids.Count);
        Debug.Log("RandomIndex: " + randomIndex);
        canCreateNumberGrids[randomIndex].SetNumber(Constant.startNumber);
    }

    public void MoveToRight()
    {
        Debug.Log("接收到Right");
        for (int i = 0; i < row; i++)
        for (int j = 0; j < row; j++)
        for (int k = j; k < row-1; k++)
            HandleMoveGrid(grids[i][k], grids[i][k + 1]);
        CreateNumber();
    }
    public void MoveToLeft()
    {
        Debug.Log("接收到Left");
        for (int i = 0; i < row; i++)
        for (int j = 0; j < row; j++)
        for (int k = j; k > 0; k--)
            HandleMoveGrid(grids[i][k], grids[i][k-1]);
        CreateNumber();
    }
    public void MoveToUp()
    {
        Debug.Log("接收到Up");
        for (int i = 0; i < row; i++)
        for (int j = 0; j < row; j++)
        for (int k = i; k > 0; k--)
            HandleMoveGrid(grids[k][j], grids[k - 1][j]);
        CreateNumber();
    }

    // 递归实现下滑操作
    public void MoveToDown()
    {
        Debug.Log("接收到Down");
        for (int i = 0; i < row; i++)
        for (int j = 0; j < row; j++)
            HandleMoveDown(i, j);
        CreateNumber();
    }

    public void HandleMoveDown(int i, int j)
    {
        if (i + 1 >= row) return; // 当前格子在最后一行，则无需移动
        HandleMoveGrid(grids[i][j], grids[i + 1][j]);
        HandleMoveDown(i + 1, j);
    }

    public void HandleMoveGrid(CellGrid currGrid, CellGrid LastGrid)
    {
        if (LastGrid.GetNumber() == "")
        {
            // 无数字 可上移
            LastGrid.SetNumber(currGrid.GetNumber());
            currGrid.ClearNumber();
        }
        else
        {
            // 判断数字是否相等
            if (!LastGrid.CanMerge || !currGrid.CanMerge || LastGrid.GetNumber() != currGrid.GetNumber()) return;
            // 相等则合并
            LastGrid.doubleNumber(currGrid.GetNumber());
            currGrid.ClearNumber();
            LastGrid.CanMerge = false;
        }
    }

    public bool IsExistMerge()
    {
        for (int i = 0; i < row; i++)
            for (int j = 0; j < row; j++)
            {
                int curNumber = int.Parse(grids[i][j].GetNumber());
                int upNumber = i > 0 ? int.Parse(grids[i - 1][j].GetNumber()) : -1;
                int downNumber = i + 1 < row ? int.Parse(grids[i + 1][j].GetNumber()) : -1;
                int leftNumber = j > 0 ? int.Parse(grids[i][j - 1].GetNumber()) : -1;
                int rightNumber = j + 1 < row ? int.Parse(grids[i][j + 1].GetNumber()) : -1;
                if (curNumber == upNumber || curNumber == downNumber || curNumber == leftNumber ||
                    curNumber == rightNumber) return true;
            }

        return false; 
    }
}
