using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour {

    /// <summary>
    /// Loads a scene
    /// </summary>
    /// <param name="name">Scene name</param>
    public void LoadScene(string name)
    {
        Application.LoadLevel(name);
    }
    
    /// <summary>
    /// Exits the program
    /// </summary>
    public void ExitProgram()
    {
        Application.Quit();
    }
}
