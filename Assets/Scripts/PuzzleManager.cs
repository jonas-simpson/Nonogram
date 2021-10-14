using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Nonogram.Puzzle;

public class PuzzleManager : MonoBehaviour
{
    //private int width;
    //private int height;
    private int size;
    
    public Texture2D nonogram;
    private bool[,] pixels;

    public NonogramGrid grid;

    public int health;
    private int numBlocks;

    private void Awake() {
        grid = FindObjectOfType<NonogramGrid>();
        health = 3;

        ReadTexture();
        grid.Generate(pixels, size);
    }

    public void ReadTexture() {
        // width = nonogram.width;
        // height = nonogram.height;

        size = nonogram.width;
        Debug.Log("Grid Size: " + size + 'x' + size);

        Color[] colors = nonogram.GetPixels();
        pixels = ConvertTo2D(ref colors);
    }

    public void SubtractHealth(){
        health--;
        //Play animation

        if(health <= 0){
            //Game over
        }
    }

    private bool[,] ConvertTo2D(ref Color[] color1d)
    {
        bool[,] color2d = new bool[size,size];

        for(int y = 0; y < size; y++)
        {
            for(int x = 0; x < size; x++)
            {
                Color myColor = color1d[size * y + x];
                if (myColor == Color.white)
                    color2d[x, y] = false;
                else if (myColor == Color.black)
                    color2d[x, y] = true;
                // Debug.Log("Analyzing pixel (" + y + ',' + x + ") - " + myColor);
            }
        }

        Debug.Log(color2d);
        return color2d;
    }
}
