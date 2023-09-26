using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;
using UnityEngine.UIElements;

public class Maze : MonoBehaviour
{
    [SerializeField]
    public int Column = 1; // 列
    public int Row = 1; // 行
    public float spacing = 1f;
    public GameObject MazeGround;
    public GameObject WallGround;
    public GameObject Player;
    public GameObject Key;
    private List<GameObject> groundLists = new();

    private Transform mazeHolder; // 用于容纳迷宫物体的父物体
    private bool[,] visited;        // 用于追踪哪些格子已经被访问过
    private int cellSize = 2;       // 每个格子的大小，用于确定墙壁位置
    void Start()
    {
        Debug.Log("Create Maze Ground");

        GenerateMaze();

        var height = new Vector3(0, 1, 0);
        if (MazeGround != null)
        {
            //Debug.Log("Groung_Row:" + Row);
            //Debug.Log("Groung_Column:" + Column);
            //for (int row = 0; row < Row; row++)
            //{
            //    for (int col = 0; col < Column; col++)
            //    {
            //        float x = col * spacing;
            //        float z = row * spacing;
            //        GameObject goj = GameObject.Instantiate(MazeGround, new Vector3(x, 0, z), Quaternion.identity);
            //        groundLists.Add(goj);
            //    }
            //}

            // Generate Key
            int num1 = Random.Range(0, groundLists.Count);
            Instantiate(Key, groundLists[num1].transform.position + height, Quaternion.identity);

            // Generate Player
            int num2 = Random.Range(0, groundLists.Count);
            Instantiate(Player, groundLists[num2].transform.position + height, Quaternion.identity);
        }
    }
    void GenerateMaze()
    {
        mazeHolder = new GameObject("Maze").transform;
        visited = new bool[Row, Column];

        // 初始化visited数组
        for (int row = 0; row < Row; row++)
        {
            for (int col = 0; col < Column; col++)
            {
                visited[row, col] = false;
            }
        }

        GeneratePath(0, 0);

        for (int row = 0; row < Row; row++)
        {
            for (int col = 0; col < Column; col++)
            {
                if (!visited[row, col])
                {
                    Instantiate(WallGround, new Vector3(col * cellSize, 0, row * cellSize), Quaternion.identity, mazeHolder);
                }
                else
                {
                    Instantiate(MazeGround, new Vector3(col * cellSize, 0, row * cellSize), Quaternion.identity, mazeHolder);
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
            if (newRow >= 0 && newRow < Row && newCol >= 0 && newCol < Column && !visited[newRow, newCol])
            {
                // 打通墙壁
                int wallRow = (row + newRow) / 2;
                int wallCol = (col + newCol) / 2;
                visited[wallRow, wallCol] = true;

                Debug.Log(visited[wallRow, wallCol]);
                // 递归生成路径
                GeneratePath(newRow, newCol);
            }
        }
    }
}
