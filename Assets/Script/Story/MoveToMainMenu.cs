using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveToMainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Move();
    }

    public void Move()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
