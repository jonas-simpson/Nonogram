using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Nonogram.Puzzle {
    public class RevealSwitch : MonoBehaviour
    {
        public NonogramGrid grid;

        private bool revealState;
        private Text myText;

        public bool GetRevealState(){
            return revealState;
        }

        private void Awake() {
            grid = GameObject.FindObjectOfType<NonogramGrid>();

            myText = GetComponentInChildren<Text>();
            revealState = true;
            SetText();
        }

        public void SwitchCellSprites(){
            grid.SwitchCellSprite(revealState);
        }

        public void SwitchText(){
            if(revealState){
                revealState = false;
            }
            else{
                revealState = true;
            }

            SetText();
        }

        private void SetText(){
            if(revealState){
                myText.text = "Block []";
            }
            else{
                myText.text = "Cross X";
            }
        }
    }
}