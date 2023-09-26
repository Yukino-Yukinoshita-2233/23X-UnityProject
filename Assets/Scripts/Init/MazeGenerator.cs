using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class MazeGenerator : MonoBehaviour
{
    public GameObject WallPrefab;   // ǽ��Ԥ�Ƽ�
    public GameObject GroundPrefab; // ����Ԥ�Ƽ�
    public GameObject Player;
    public GameObject Key;
    public int PlayerNum = 1;
    public int Rows = 50;            // �Թ�����
    public int Columns = 50;         // �Թ�����
    public int Spacing = 2;          //���
    private Transform mazeHolder;   // ���������Թ�����ĸ�����
    private bool[,] visited;        // ����׷����Щ�����Ѿ������ʹ�
    private float cellSize = 1.5f;       // ÿ�����ӵĴ�С������ȷ��ǽ�ں͵���λ��

    private void Start()
    {
        GenerateMaze();

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
        int PNum = 0;
        while(PNum < PlayerNum)
        {
            int XP = Random.Range(0, Rows);
            int ZP = Random.Range(0, Columns);

            // ��ȡ��ɫ��λ��
            Vector3 objectPosition = new Vector3(XP, 1.5f, ZP);//Player.transform.position;
                                                               // �������������·���һ������
            Ray ray = new Ray(objectPosition, -Vector3.up);
            // ����һ�� RaycastHit �������ڴ洢���߼��Ľ��
            RaycastHit hit;
            // �������߼��
            if (Physics.Raycast(ray, out hit))
            {
                // ��⵽����
                GameObject hitObject = hit.collider.gameObject;
                if (hitObject != null)
                {
                    // Generate Player
                    Instantiate(Player, new Vector3(XP, 1.5f, ZP), Quaternion.identity);
                    PNum++;
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
                Debug.Log(visited[wallRow, wallCol]);
                // �ݹ�����·��
                GeneratePath(newRow, newCol);
            }
        }
    }
}