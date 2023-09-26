using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;

public class MazeGenerator : MonoBehaviour
{
    public GameObject GroundPrefab; // �ذ�Ԥ�Ƽ�
    public GameObject Player;
    public GameObject Key;
    private List<GameObject> groundLists = new();

    public int Rows = 5;            // �Թ�����
    public int Columns = 5;         // �Թ�����

    private Transform mazeHolder;   // ���������Թ�����ĸ�����
    private bool[,] visited;        // ����׷����Щ�����Ѿ������ʹ�
    private int cellSize = 1;       // ÿ�����ӵĴ�С������ȷ���ذ�λ��
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

        // ����visited���������Թ�
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
                newRow--;
            else if (directions[i] == 1) // ����
                newCol++;
            else if (directions[i] == 2) // ����
                newRow++;
            else if (directions[i] == 3) // ����
                newCol--;

            // ����µ�λ���Ƿ���Ч
            if (newRow >= 0 && newRow < Rows && newCol >= 0 && newCol < Columns && !visited[newRow, newCol])
            {
                if()
                // ��ͨǽ��
                //int wallRow = (row + newRow) / 2;
                //int wallCol = (col + newCol) / 2;
                //visited[wallRow, wallCol] = true;

                // �ݹ�����·��
                GeneratePath(newRow, newCol);
            }
        }
    }
}

