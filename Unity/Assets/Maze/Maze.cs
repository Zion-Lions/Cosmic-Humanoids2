﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Maze : MonoBehaviour
{
    [System.Serializable]

    public class Cell
    {
        public bool visited;
        public GameObject north; //1
        public GameObject east; //2
        public GameObject west; //3
        public GameObject south; //4
    }

    public GameObject wall;
    private float wallLength = 4.0f;
    public int xSize = 30;
    public int ySize = 30;

    private Vector3 initialPos;
    private GameObject wallHolder;
    private Cell[] cells;

    private int currentCell = 0;
    private int totalCells;
    private int visitedCells = 0;
    private bool startedBuilding = false;
    private int currentNeighbour = 0;
    private List<int> lastCells;
    private int backingUp = 0;
    private int wallToBreak = 0;

    // Use this for initialization
    void Start()
    {
        CreateWalls();
    }

    void CreateWalls()
    {
        wallHolder = new GameObject();
        wallHolder.name = "Maze";

        initialPos = new Vector3((-xSize / 2) + wallLength / 2, 0.0f, (-ySize / 2) + wallLength / 2);
        Vector3 myPos = initialPos;
        GameObject tempWall;

        //Axe X
        for (int i = 0; i < ySize; i++)
        {
            for (int j = 0; j <= xSize; j++)
            {
                myPos = new Vector3(initialPos.x + (j * wallLength) - wallLength / 2, 0.0f, initialPos.z + (i * wallLength) - wallLength / 2);
                tempWall = Instantiate(wall, myPos, Quaternion.identity) as GameObject;
                tempWall.transform.parent = wallHolder.transform;
            }
        }

        //Axe Y
        for (int i = 0; i <= ySize; i++)
        {
            for (int j = 0; j < xSize; j++)
            {
                myPos = new Vector3(initialPos.x + (j * wallLength), 0.0f, initialPos.z + (i * wallLength) - wallLength);
                tempWall = Instantiate(wall, myPos, Quaternion.Euler(0.0f, 90.0f, 0.0f)) as GameObject;
                tempWall.transform.parent = wallHolder.transform;
            }
        }

        CreatCells();
    }

    void CreatCells()
    {
        lastCells = new List<int>();
        lastCells.Clear();
        totalCells = xSize * ySize;
        GameObject[] allWalls;
        int children = wallHolder.transform.childCount;
        allWalls = new GameObject[children];

        cells = new Cell[xSize * ySize];

        int EastWestProc = 0;
        int childProc = 0;
        int termCount = 0;

        //Get all the children
        for (int i = 0; i < children; i++)
        {
            allWalls[i] = wallHolder.transform.GetChild(i).gameObject;
        }

        //Walls to Cells
        for (int i = 0; i < cells.Length; i++)
        {
            if (termCount == xSize)
            {
                EastWestProc ++;
                termCount = 0;
            }
               
            cells[i] = new Cell();
            cells[i].east = allWalls[EastWestProc];
            cells[i].south = allWalls[childProc + (xSize + 1) * ySize];

            EastWestProc++;

            termCount++;
            childProc++;

            cells[i].west = allWalls[EastWestProc];
            cells[i].north = allWalls[(childProc + (xSize + 1) * ySize) + xSize - 1];
        }

        CreateMaze();
    }

    void CreateMaze()
    {
        while (visitedCells < totalCells)
        {
            if (startedBuilding)
            {
                GiveMeNeighour();

                if (cells[currentNeighbour].visited == false && cells[currentCell].visited == true)
                {
                    BreakWall();
                    cells[currentNeighbour].visited = true;
                    visitedCells++;
                    lastCells.Add(currentCell);
                    currentCell = currentNeighbour;

                    if (lastCells.Count > 0)
                    {
                        backingUp = lastCells.Count - 1;
                    }
                }
            }
            else
            {
                currentCell = Random.Range(0, totalCells);
                cells[currentCell].visited = true;
                visitedCells++;
                startedBuilding = true;
            }
        }
    }

    void GiveMeNeighour()
    {
        int lenght = 0;
        int[] neighour = new int[4];
        int[] connectingWall = new int[4];

        int check = 0;
        check = ((currentCell + 1) / xSize);
        check -= 1;
        check *= xSize;
        check += xSize;

        //west
        if (currentCell + 1 < totalCells && (currentCell + 1) != check)
        {
            if (cells[currentCell + 1].visited == false)
            {
                neighour[lenght] = currentCell + 1;
                connectingWall[lenght] = 3;
                lenght++;
            }
        }

        //east
        if (currentCell - 1 >= 0 && currentCell != check)
        {
            if (cells[currentCell - 1].visited == false)
            {
                neighour[lenght] = currentCell - 1;
                connectingWall[lenght] = 2;
                lenght++;
            }
        }

        //north
        if (currentCell + xSize < totalCells)
        {
            if (cells[currentCell + xSize].visited == false)
            {
                neighour[lenght] = currentCell + xSize;
                connectingWall[lenght] = 1;
                lenght++;
            }
        }

        //south
        if (currentCell - xSize >= 0)
        {
            if (cells[currentCell - xSize].visited == false)
            {
                neighour[lenght] = currentCell - xSize;
                connectingWall[lenght] = 4;
                lenght++;
            }
        }


        if (lenght != 0)
        {
            int theOne = Random.Range(0, lenght);
            currentNeighbour = neighour[theOne];
            wallToBreak = connectingWall[theOne];
        }
        else
        {
            if (backingUp > 0)
            {
                currentCell = lastCells[backingUp];
                backingUp--;
            }
        }
    }

    void BreakWall()
    {
        switch (wallToBreak)
        {
            case 1:
                Destroy(cells[currentCell].north);
                break;

            case 2:
                Destroy(cells[currentCell].east);
                break;

            case 3:
                Destroy(cells[currentCell].west);
                break;

            case 4:
                Destroy(cells[currentCell].south);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
