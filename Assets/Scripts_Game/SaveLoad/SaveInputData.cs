using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveInputData
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

    public SaveInputData(InputManager inputManager_)
    {
        left = inputManager_.left;
        right = inputManager_.right;
        up = inputManager_.up;
        down = inputManager_.down;

        jump = inputManager_.jump;
        dash = inputManager_.dash;
        climb = inputManager_.climb;
        hook = inputManager_.hook;

        confirm = inputManager_.confirm;
        back = inputManager_.back;
        pause = inputManager_.pause;
        
    }

}
