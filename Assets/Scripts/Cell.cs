using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

namespace Nonogram.Puzzle
{
    public class Cell : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        public bool revealed = false;
        public bool state;
        public RevealSwitch revealSwitch;
        public PointerWatcher pointerWatcher;

        private Color color0;
        private Color color1;

        private Image myImage;
        private CellResources resources;
        private Animator animator;
        private PuzzleManager manager;

        /*public enum CellState
        {
            cross,
            block
        }

        public CellState state;*/

        /*public Cell(bool _state)
        {
            revealed = false;
            state = _state;
        }

        public Cell(bool _revealed, bool _state)
        {
            revealed = _revealed;
            state = _state;
        }*/

        private void OnEnable() {
            revealSwitch = GameObject.FindObjectOfType<RevealSwitch>();
            pointerWatcher = GameObject.FindObjectOfType<PointerWatcher>();
            manager = GameObject.FindObjectOfType<PuzzleManager>();

            myImage = GetComponent<Image>();
            resources = GetComponent<CellResources>();
            animator = GetComponent<Animator>();

            // Color tempBlack = new Color(0,0,0,0);
            myImage.color = Color.clear;
        }

        public void OnPointerEnter(PointerEventData eventData) {
            if(!revealed){
                if(pointerWatcher.GetPointerState()){
                    //mouse over with click down!
                    // Debug.Log("Mouse drag over cell " + gameObject.name);
                    Reveal(revealSwitch.GetRevealState());
                }
                else{
                    animator.SetBool("MouseOver", true);
                    // Debug.Log("Mouse hover over Cell " + gameObject.name);
                }
            }
        }

        public void OnPointerExit(PointerEventData eventData) {
            if(!revealed){
                if(pointerWatcher.GetPointerState()){
                    //mouse exit with click down!
                    // Debug.Log("Drag over cell " + gameObject.name);
                    Reveal(revealSwitch.GetRevealState());
                }
                else{
                    animator.SetBool("MouseOver", false);
                }
            }
        }

        public void OnPointerClick(PointerEventData eventData){
            if(!revealed){
                //Reveal!
                Reveal(revealSwitch.GetRevealState());
            }
        }

        public bool Reveal(bool input)
        {
            revealed = true;
            // DisplayState(input);
            DisplayState(state);
            animator.SetBool("Revealed", true);

            if(input == state)
            {
                //Player answered cell correctly
                Debug.Log(name + " answered correctly");
                return true;
            }
            else
            {
                //Player answered cell incorrectly
                //Subtract health
                manager.SubtractHealth();
                Debug.Log(name + " answered incorrectly");
                return false;
            }
        }

        public void DisplayState(bool input)
        {
            //Display cross or block
            if(input){
                myImage.sprite = resources.blank;
            }
            else{
                myImage.sprite = resources.cross;
            }
        }
        
        private void Awake() {
            SetColor(); //remove from awake
        }

        private void SetColor(){
            Image myImage = GetComponent<Image>();
            if(state)
                myImage.color = color1;
            else
                myImage.color = color0;
        }
    }
}