using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Restart : MonoBehaviour
{

    private void Update()
    {
        Cursor.lockState = CursorLockMode.None;
    }
    public void LoadMenu()
    {
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(0);
    }
}
