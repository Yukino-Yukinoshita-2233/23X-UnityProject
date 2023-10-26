using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class MazeGenerator : MonoBehaviour
{
    public static MazeGenerator Instance;
    public Vector3 spawnpositon { get; set; }
    public GameObject WallPrefab;   // ǽ��Ԥ�Ƽ�
    public GameObject GroundPrefab; // ����Ԥ�Ƽ�
    public GameObject Player;
    public GameObject Key;
    public GameObject Light;
    public GameObject SkyLight;
    public int PlayerNum = 1;
    public int LightNum = 20;
    public int Rows = 50;            // �Թ�����
    public int Columns = 50;         // �Թ�����
    public int Spacing = 2;          //���
    private Transform mazeHolder;   // ���������Թ�����ĸ�����
    private Transform LightHolder;   // �������ɵƹ�����ĸ�����
    private bool[,] visited;        // ����׷����Щ�����Ѿ������ʹ�
    private float cellSize = 1.5f;       // ÿ�����ӵĴ�С������ȷ��ǽ�ں͵���λ��
    public LayerMask GroundLayer;
    private void Start()
    {
        Instance = this;
        GenerateMaze();
        PlayerCreate();
        LightCreate();
        SkyLightCreate();
        // Generate Key
        Instantiate(Key, new Vector3(Rows-2,1.5f,Columns-2), Quaternion.identity);


    }

    void GenerateMaze()
    {
        mazeHolder = new GameObject("Maze").transform;
        visited = new bool[Rows, Columns];

        // ��ʼ��visited����
        for (int row = 0; row < Rows; row++)
        {
            for (int col = 0; col < Columns; col++)
            {
                visited[row, col] = false;
            }
        }

        // ����ʼ�㿪ʼ�����Թ�
        GeneratePath(0, 0);

        // ����visited���������Թ�ǽ�ں͵���
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

        // ��������ĸ������˳��
        int[] directions = { 0, 1, 2, 3 };
        System.Random random = new System.Random();
        for (int i = 0; i < directions.Length; i++)
        {
            int temp = directions[i];
            int randomIndex = random.Next(i, directions.Length);
            directions[i] = directions[randomIndex];
            directions[randomIndex] = temp;
        }

        // �������ĸ�����ǰ��
        for (int i = 0; i < 4; i++)
        {
            int newRow = row;
            int newCol = col;

            if (directions[i] == 0) // ����
                newRow -= Spacing;
            else if (directions[i] == 1) // ����
                newCol += Spacing;
            else if (directions[i] == 2) // ����
                newRow += Spacing;
            else if (directions[i] == 3) // ����
                newCol -= Spacing;

            // ����µ�λ���Ƿ���Ч
            if (newRow >= 0 && newRow < Rows && newCol >= 0 && newCol < Columns && !visited[newRow, newCol])
            {
                // ��ͨǽ��
                int wallRow = (row + newRow) / 2;
                int wallCol = (col + newCol) / 2;
                visited[wallRow, wallCol] = true;
                //Debug.Log(visited[wallRow, wallCol]);
                // �ݹ�����·��
                GeneratePath(newRow, newCol);
            }
        }
    }
    public void PlayerCreate()
    {
        int PNum = 0;
        while (PNum < PlayerNum)
        {
            int XP = Random.Range(0, Rows/2);
            int ZP = Random.Range(0, Columns/2);

            spawnpositon = new Vector3(XP, 1.5f, ZP);

            // �������������·���һ������
            Ray ray = new Ray(spawnpositon, -Vector3.up);
            // ����һ�� RaycastHit �������ڴ洢���߼��Ľ��
            RaycastHit hit;
            // �������߼��
            if (Physics.Raycast(ray, out hit,2))
            {
                // ��⵽����
                GameObject hitObject = hit.collider.gameObject;
                if (hitObject != null)
                {
                    // Generate Player
                    Instantiate(Player, spawnpositon, Quaternion.identity);
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
            Instantiate(Light, new Vector3(XP, 20f, ZP), Quaternion.Euler(90f,0f,0f), LightHolder);
            LNum++;
        }
    }
     void SkyLightCreate()
    {
        Instantiate(SkyLight, new Vector3(Rows * cellSize / 2, 50f, Columns * cellSize / 2), Quaternion.Euler(90f,0f,0f));
    }

}