using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Nonogram.Puzzle{
    public class GuideContainer : MonoBehaviour
    {
        public GameObject blockGroup;
        public NonogramGrid grid;
        public int index;

        public Cell[] myCells;
        public Stack<Stack<Cell>> myGroups;
        public TextMeshProUGUI[] myGuides;

        public enum ContainerOrientation{
            Row,
            Column
        };

        public ContainerOrientation myOrientation;  //Set in scene

        private void Start() {
            grid = GameObject.FindObjectOfType<NonogramGrid>();

            //Collect cells from nonogram grid
            if(myOrientation == ContainerOrientation.Row)
                myCells = grid.GetRow(index);
            else if(myOrientation == ContainerOrientation.Column)
                myCells = grid.GetColumn(index);

            //Initialize cell groups
            myGroups = new Stack<Stack<Cell>>();
            ExamineCells();

            //Initialize text group
            myGuides = new TextMeshProUGUI[myGroups.Count];
            GenerateGuides();
        }

        public void SetIndex(int i){
            index = i;
        }

        private void ExamineCells(){
            //Navigate cells and mark groups of blocks
            Stack<Cell> currentGroup = new Stack<Cell>();
            foreach(Cell cell in myCells){
                if(cell.state){
                    //Current cell is a block, and belongs in the current group                    
                    currentGroup.Push(cell);
                }
                else{
                    //Current cell is a cross
                    if(currentGroup.Count > 0){
                        //First cell after the group, push group and reinitialize
                        myGroups.Push(currentGroup);
                        currentGroup = new Stack<Cell>();
                    }
                }
            }
            
            //Outside of loop, if currentGroup is not empty, we still need to push the last group
            if(currentGroup.Count > 0 || myGroups.Count == 0){
                myGroups.Push(currentGroup);
                currentGroup = new Stack<Cell>();
            }
        }

        private void GenerateGuides(){
            //Navigate backwards through cell groups
            foreach(Stack<Cell> group in myGroups) {
                GameObject obj = Instantiate(blockGroup, transform);
                
                TextMeshProUGUI currentText = obj.GetComponent<TextMeshProUGUI>();
                Debug.Log("Current Text: " + currentText);

                //Set text
                int myGroupSize = group.Count;
                if(myGroupSize == 0)
                    currentText.text = " ";
                else
                    currentText.text = group.Count.ToString();
            }
        }
    }
}