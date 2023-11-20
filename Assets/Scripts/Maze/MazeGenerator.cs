using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class MazeGenerator : MonoBehaviour
{
    public static MazeGenerator Instance;
    public Vector3 Spawnpositon { get; set; }
    public GameObject WallPrefab;   // 墙壁预制件
    public GameObject GroundPrefab; // 地面预制件
    public GameObject Key;
    public GameObject Light;
    public GameObject SkyLight;
    public int PlayerNum = 1;
    public int LightNum = 20;
    public int Rows = 50;            // 迷宫行数
    public int Columns = 50;         // 迷宫列数
    public int Spacing = 2;          //间距
    private Transform mazeHolder;   // 用于容纳迷宫物体的父物体
    private Transform LightHolder;   // 用于容纳灯光物体的父物体
    private bool[,] visited;        // 用于追踪哪些格子已经被访问过
    private float cellSize = 1.5f;       // 每个格子的大小，用于确定墙壁和地面位置
    public LayerMask GroundLayer;
    private void Start()
    {
        Instance = this;
        GenerateMaze();
        PlayerCreate();
        LightCreate();
        SkyLightCreate();
        // Generate Key
        Instantiate(Key, new Vector3(Rows - 2, 1.5f, Columns - 2), Quaternion.identity);
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
        Debug.Log(visited[1,1]);
        // 根据visited数组生成迷宫墙壁和地面
        for (int row = 0; row < Rows; row++)
        {
            for (int col = 0; col < Columns; col++)
            {
                if (!visited[row, col])
                {
                    //Instantiate(WallPrefab, new Vector3(col * cellSize, 0, row * cellSize), Quaternion.identity, mazeHolder);
                }
                else
                {
                    Instantiate(GroundPrefab, new Vector3(col * cellSize, 0, row * cellSize), Quaternion.identity, mazeHolder);

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
                newRow -= Spacing;
            else if (directions[i] == 1) // 向右
                newCol += Spacing;
            else if (directions[i] == 2) // 向下
                newRow += Spacing;
            else if (directions[i] == 3) // 向左
                newCol -= Spacing;

            // 检查新的位置是否有效
            if (newRow >= 0 && newRow < Rows && newCol >= 0 && newCol < Columns && !visited[newRow, newCol])
            {
                // 打通墙壁
                int wallRow = (row + newRow) / 2;
                int wallCol = (col + newCol) / 2;
                visited[wallRow, wallCol] = true;
                //Debug.Log(visited[wallRow, wallCol]);
                // 递归生成路径
                GeneratePath(newRow, newCol);
            }
        }
    }

    public void PlayerCreate()
    {
        int PNum = 0;
        while (PNum < PlayerNum)
        {
            int XP = Random.Range(0, Rows / 2);
            int ZP = Random.Range(0, Columns / 2);

            Spawnpositon = new Vector3(XP, 1.5f, ZP);

            // 从物体中心向下发射一条射线
            Ray ray = new Ray(Spawnpositon, -Vector3.up);
            // 创建一个 RaycastHit 对象，用于存储射线检测的结果
            RaycastHit hit;
            // 进行射线检测
            if (Physics.Raycast(ray, out hit, 2))
            {
                // 检测到物体
                GameObject hitObject = hit.collider.gameObject;
                if (hitObject != null)
                {
                    //Generate Player
                    //Instantiate(Player, Spawnpositon, Quaternion.identity);
                    PlayerInit.Instance.SetPlayerSpawn(Spawnpositon);
                    PNum++;
                }
            }

        }
    }

    void LightCreate()
    {
        LightHolder = new GameObject("Light").transform;

        int LNum = 0;
        while (LNum < LightNum)
        {
            int XP = Random.Range(0, Mathf.FloorToInt(Rows * cellSize));
            int ZP = Random.Range(0, Mathf.FloorToInt(Columns * cellSize));
            Instantiate(Light, new Vector3(XP, 20f, ZP), Quaternion.Euler(90f, 0f, 0f), LightHolder);
            LNum++;
        }
    }

    void SkyLightCreate()
    {
        Instantiate(SkyLight, new Vector3(Rows * cellSize / 2, 50f, Columns * cellSize / 2), Quaternion.Euler(90f, 0f, 0f));
    }

}