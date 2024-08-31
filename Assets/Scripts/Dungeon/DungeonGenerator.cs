using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    public class Cell
    {
        public bool visited = false;
        public bool finishline = false;
        public bool[] status = new bool[4];
    }

    public Vector2 size = new Vector2(3, 3);
    public int startPos = 0;
    public GameObject room; //prefab
    public GameObject tree; //prefab
    public Vector2 offset = new Vector2(6, 6); //distance between each room

    List<Cell> board = new List<Cell>();

    private void Start()
    {
        MazeGenerator();
    }

    private void GenerateDungeon()
    {
        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                Cell currentCell = board[Mathf.FloorToInt(i + j * size.x)];

                if (currentCell.visited)
                {
                    var newRoom = Instantiate(room, new Vector3(i * offset.x,0, -j * offset.y), Quaternion.identity, transform).GetComponent<RoomBehaviour>();
                    newRoom.UpdateRoom(board[Mathf.FloorToInt(i + j * size.x)].status);

                    newRoom.name += " " + i + "-" + j;

                    if (currentCell.finishline)
                    {
                        Instantiate(tree, new Vector3( newRoom.transform.position.x + 2f, 0, newRoom.transform.position.z - 2f), Quaternion.identity, transform);
                    }
                }
            }
        }
    }

    private void MazeGenerator()
    {
        board.Clear();

        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                board.Add(new Cell());
            }
        }

        int currentCell = startPos;

        Stack<int> path = new Stack<int>();

        int k = 0;

        while (k < 1000)
        {
            k++;

            board[currentCell].visited = true;

            if (currentCell == board.Count - 1) {
                board[currentCell].finishline = true;
                break; 
            }

            List<int> neighbors = CheckNeighbors(currentCell); //check cell's neighbors 

            if (neighbors.Count == 0)
            {
                if (path.Count == 0)
                    break;
                else
                {
                    currentCell = path.Pop();
                }
            }
            else
            {
                path.Push(currentCell);

                int newCell = neighbors[Random.Range(0, neighbors.Count)]; 

                if (newCell > currentCell)
                {
                    //down or right 
                    if (newCell - 1 == currentCell)
                    {
                        board[currentCell].status[2] = true;
                        currentCell = newCell;
                        board[currentCell].status[3] = true;
                    }
                    else
                    {
                        board[currentCell].status[1] = true;
                        currentCell = newCell;
                        board[currentCell].status[0] = true;
                    }
                }
                else
                {
                    //up or left 
                    if (newCell + 1 == currentCell)
                    {
                        board[currentCell].status[3] = true;
                        currentCell = newCell;
                        board[currentCell].status[2] = true;
                    }
                    else
                    {
                        board[currentCell].status[0] = true;
                        currentCell = newCell;
                        board[currentCell].status[1] = true;
                    }
                }
            }
        }
        GenerateDungeon();
    }

    private List<int> CheckNeighbors(int cell)
    {
        List<int> neighbors = new List<int>();

        //check up neighboor
        if (cell - size.x >= 0 && !board[Mathf.FloorToInt(cell - size.x)].visited)
        { 
            neighbors.Add(Mathf.FloorToInt(cell - size.x));
        }

        //check down neighboor
        if (cell + size.x < board.Count && !board[Mathf.FloorToInt(cell + size.x)].visited)
        {
            neighbors.Add(Mathf.FloorToInt(cell + size.x));
        }

        //check right neighboor
        if ((cell+1) % size.x != 0 && !board[Mathf.FloorToInt(cell + 1)].visited)
        {
            neighbors.Add(Mathf.FloorToInt(cell + 1));
        }

        //check left neighboor
        if (cell % size.x != 0 && !board[Mathf.FloorToInt(cell - 1)].visited)
        {
            neighbors.Add(Mathf.FloorToInt(cell - 1));
        }

        return neighbors;
    }
}
