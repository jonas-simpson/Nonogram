using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nonogram.Puzzle{
    public class GuideParent : MonoBehaviour
    {
        public GameObject guideContainer;
        public NonogramGrid grid;
        public GuideContainer[] myGuideContainers;

        private void Awake() {
            grid = GameObject.FindObjectOfType<NonogramGrid>();
        }

        // Start is called before the first frame update
        void Start()
        {
            //Fetch grid size from Nonongram Grid
            GenerateGuides(grid.GetGridSize());
        }

        private void GenerateGuides(int size){
            //Determine if columns or rows
            GuideContainer temp = guideContainer.GetComponent<GuideContainer>();
            string prefix = "";
            if(temp.myOrientation == GuideContainer.ContainerOrientation.Row)
                prefix = "Row";
            else if(temp.myOrientation == GuideContainer.ContainerOrientation.Column)
                prefix = "Column";

            //Initialize containers
            myGuideContainers = new GuideContainer[size];

            //Instantiate guide containers
            for(int i = 0; i < size; i++){
                GameObject obj = Instantiate(guideContainer, transform);
                        
                GuideContainer container = obj.GetComponent<GuideContainer>();
                obj.name = prefix + ' ' + i.ToString();
                container.SetIndex(i);
            }
        }
    }
}