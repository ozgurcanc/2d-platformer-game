using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager
{
    public KeyCode left;
    public KeyCode right;
    public KeyCode up;
    public KeyCode down;

    public KeyCode jump;
    public KeyCode dash;
    public KeyCode climb;
    public KeyCode hook;

    public KeyCode confirm;
    public KeyCode back;
    public KeyCode pause;

    public void Init(SaveInputData saveInput_)
    {
        left = saveInput_.left;
        right = saveInput_.right;
        up = saveInput_.up;
        down = saveInput_.down;

        jump = saveInput_.jump;
        dash = saveInput_.dash;
        climb = saveInput_.climb;
        hook = saveInput_.hook;

        confirm = saveInput_.confirm;
        back = saveInput_.back;
        pause = saveInput_.pause;
    }

    public void InitDefault()
    {
        left = KeyCode.LeftArrow;
        right = KeyCode.RightArrow;
        up = KeyCode.UpArrow;
        down = KeyCode.DownArrow;

        jump = KeyCode.Space;
        dash = KeyCode.Q;
        climb = KeyCode.W;
        hook = KeyCode.E;

        confirm = KeyCode.Return;
        back = KeyCode.Escape;
        pause = KeyCode.Escape;
    }

    public KeyCode this[int index]
    {
        get
        {
            if(index==0) return left;
            else if(index==1) return right;
            else if(index==2) return up;
            else if(index==3) return down;
            else if(index==4) return jump;
            else if(index==5) return dash;
            else if(index==6) return climb;
            else if(index==7) return hook;
            else if(index==8) return confirm;
            else if(index==9) return back;
            else if(index==10) return pause;
            return KeyCode.None;
        }
        set
        {
            if(index==0) left = value;
            else if(index==1) right= value;
            else if(index==2) up= value;
            else if(index==3) down= value;
            else if(index==4) jump= value;
            else if(index==5) dash= value;
            else if(index==6) climb= value;
            else if(index==7) hook= value;
            else if(index==8) confirm= value;
            else if(index==9) back= value;
            else if(index==10) pause= value;     
        }
    }

}
