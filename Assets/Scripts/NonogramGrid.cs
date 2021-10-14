using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Nonogram.Puzzle
{
    public class NonogramGrid : MonoBehaviour
    {
        public PuzzleManager puzzle;
        public Cell[,] grid;
        public RectTransform playArea;
        public GameObject cell;
        public GridLayoutGroup gridLayoutGroup;
        private int cellScale;
        public int cellCount;
        public int gridSize;

        /*public void Awake(){
            playArea = GetComponent<RectTransform>();
            gridLayoutGroup = GetComponent<GridLayoutGroup>();

            //Set cell count
            // cellCount = 25;

            //Initialize containers
            // gridSize = (int)Mathf.Sqrt(25);
            

            
            // grid = new Cell[gridSize,gridSize];

            // Populate();
        }*/

        private void Initialize(){
            playArea = GetComponent<RectTransform>();
            gridLayoutGroup = GetComponent<GridLayoutGroup>();

            SetCellScale();
            grid = new Cell[gridSize,gridSize];
        }

        private void SetCellScale(){
            gridLayoutGroup.cellSize = new Vector2(playArea.sizeDelta.x / gridSize, playArea.sizeDelta.y / gridSize);
        }

        /*private void Populate(){
            //populate grid horizontally
            for(int x = 0; x < gridSize; x++){
                for(int y = 0; y < gridSize; y++){
                    GameObject obj = Instantiate(cell, transform);
                    
                    Cell currentCell = obj.GetComponent<Cell>();
                    grid[y,x] = currentCell;
                    obj.name = "Cell (" + y + ',' + x + ')';
                }
            }
        }*/

        public void Generate(bool[,] _cellStates, int _size){
            //Receive grid size from puzzle
            gridSize = _size;
            
            //Initialize cells
            Initialize();

            //populate grid horizontally
            Debug.Log("Populating grid...");
            for(int x = 0; x < gridSize; x++){
                for(int y = 0; y < gridSize; y++){
                    //Instantiate cell
                    GameObject obj = Instantiate(cell, transform);
                    Cell currentCell = obj.GetComponent<Cell>();

                    //Set state according to nonogram puzzle
                    currentCell.state = _cellStates[y,x];

                    //Place into grid and rename
                    grid[y,x] = currentCell;
                    obj.name = "Cell (" + y + ',' + x + ')';
                }
            }
        }

        public void SwitchCellSprite(bool state){
            for(int x = 0; x < gridSize; x++){
                for(int y = 0; y < gridSize; y++){
                    if(!grid[y,x].revealed){
                        //Switch hidden cell sprite to currently selected reveal state
                        grid[y,x].DisplayState(state);
                    }
                }
            }
        }

        public Cell[] GetRow(int r){
            // Debug.Log("Fetching cells in column " + r);
            Cell[] row = new Cell[gridSize];
            for(int y = 0; y < gridSize; y++){
                row[y] = grid[y, r];
            }
            Debug.Log(row);
            return row;
        }

        public Cell[] GetColumn(int c){
            Cell[] column = new Cell[gridSize];
            for(int x = 0; x < gridSize; x++){
                column[x] = grid[c, x];
            }
            return column;
        }

        public int GetGridSize(){
            return gridSize;
        }
    }
}
