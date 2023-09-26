using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;

public class MazeGenerator : MonoBehaviour
{
    public GameObject GroundPrefab; // 地板预制件
    public GameObject Player;
    public GameObject Key;
    private List<GameObject> groundLists = new();

    public int Rows = 5;            // 迷宫行数
    public int Columns = 5;         // 迷宫列数

    private Transform mazeHolder;   // 用于容纳迷宫物体的父物体
    private bool[,] visited;        // 用于追踪哪些格子已经被访问过
    private int cellSize = 1;       // 每个格子的大小，用于确定地板位置
    int x = 50;
    private void Start()
    {
        GenerateMaze();

        var height = new Vector3(0, 1, 0);

        // Generate Player
        int num2 = Random.Range(0, groundLists.Count);
        Instantiate(Player, groundLists[num2].transform.position + height, Quaternion.identity);

    }

    void GenerateMaze()
    {
        mazeHolder = new GameObject("Maze").transform;
        visited = new bool[Rows, Columns];

        // 初始化visited数组
        for (int row = 0; row < Rows; row++)
        {
            for (int col = 0; col < Columns; col++)
            {
                visited[row, col] = false;
            }
        }

        // 从起始点开始生成迷宫
        GeneratePath(0, 0);

        // 根据visited数组生成迷宫
        for (int row = 0; row < Rows; row++)
        {
            for (int col = 0; col < Columns; col++)
            {
                if (!visited[row, col])
                {
                    //Instantiate(WallPrefab, new Vector3(col * cellSize, 0, row * cellSize), Quaternion.identity, mazeHolder);
                }
                else if(x != 0)
                {
                    Instantiate(GroundPrefab, new Vector3(col * cellSize, 0, row * cellSize), Quaternion.identity, mazeHolder);
                    x--;
                }
            }
        }
    }

    void GeneratePath(int row, int col)
    {
        visited[row, col] = true;

        // 随机打乱四个方向的顺序
        int[] directions = { 0, 1, 2, 3 };
        System.Random random = new System.Random();
        for (int i = 0; i < directions.Length; i++)
        {
            int temp = directions[i];
            int randomIndex = random.Next(i, directions.Length);
            directions[i] = directions[randomIndex];
            directions[randomIndex] = temp;
        }

        // 尝试向四个方向前进
        for (int i = 0; i < 4; i++)
        {
            int newRow = row;
            int newCol = col;

            if (directions[i] == 0) // 向上
                newRow--;
            else if (directions[i] == 1) // 向右
                newCol++;
            else if (directions[i] == 2) // 向下
                newRow++;
            else if (directions[i] == 3) // 向左
                newCol--;

            // 检查新的位置是否有效
            if (newRow >= 0 && newRow < Rows && newCol >= 0 && newCol < Columns && !visited[newRow, newCol])
            {
                if()
                // 打通墙壁
                //int wallRow = (row + newRow) / 2;
                //int wallCol = (col + newCol) / 2;
                //visited[wallRow, wallCol] = true;

                // 递归生成路径
                GeneratePath(newRow, newCol);
            }
        }
    }
}

