using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Tags;

public class PLayerInput : MonoBehaviour
{

    private PLayerController playerController;

    private int x = 0, y = 0;//cum se misca sarpele

    public enum Axis
    {
        Horizontal,
        Vertical// axele mapei
    }

    void Awake()
    {
        playerController = GetComponent<PLayerController>();
    }

    void Update()
    {
        x = 0;
        y = 0;

        GetKeyboardInput();

        SetMovement();
    }

    void GetKeyboardInput()
    {

        //x = (int)Input.GetAxisRaw("Horizontal");
        //y = (int)Input.GetAxisRaw("Vertical");
        x = GetAxisRaw(Axis.Horizontal);
        y = GetAxisRaw(Axis.Vertical);
        if(x != 0)
        {
            y = 0;
        }
    }

    void SetMovement()
    {
        if( y != 0)//verifica stanga-dreapta/sus-jos
        {
            playerController.SetInputDirection((y == 1) ? Comenzile.UP : Comenzile.DOWN);

        } else if( x != 0 )
        {
            playerController.SetInputDirection((x == 1) ?
                                                   Comenzile.RIGHT : Comenzile.LEFT);
        }
    }

    int GetAxisRaw(Axis axis)
    {
        if(axis == Axis.Horizontal)
        {
            bool left = Input.GetKeyDown(KeyCode.LeftArrow);
            bool right = Input.GetKeyDown(KeyCode.RightArrow);

            if(left)
            {
                return -1;
            }

            if(right)
            {
                return 1;
            }
            //muta sarpele cu un patratel
            return 0;
        } else if(axis == Axis.Vertical)
        {
            bool up = Input.GetKeyDown(KeyCode.UpArrow);
            bool down = Input.GetKeyDown(KeyCode.DownArrow);

            if(up)
            {
                return 1;
            }

            if(down)
            {
                return -1;
            }
            return 0;
        }

        return 0;
    }

}//class
