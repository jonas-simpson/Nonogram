using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PointerWatcher : MonoBehaviour
{
    public bool isDown;
    private Touch currentTouch;

    void Update(){
        if(Input.GetMouseButtonDown(0) || Input.touchCount > 0){
            //User has pointer down
            // Debug.Log("Touch down");
            isDown = true;

            //Track touch, if available
            if(Input.touchCount > 0)
                currentTouch = Input.touches[0];
        }

        if(Input.GetMouseButtonUp(0) || currentTouch.phase == TouchPhase.Ended){
            //User has removed pointer
            // Debug.Log("Touch up");
            isDown = false;
        }
    }

    public bool GetPointerState(){
        return isDown;
    }

}
