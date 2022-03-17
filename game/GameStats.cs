using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameStats : MonoBehaviour
{
    public int nextLvl;

    public Texture2D cursorArrow;
    private GameObject player;
    private GameObject ts;

    void Start()
    {
        player = GameObject.Find("Player");
        Cursor.SetCursor(cursorArrow, new Vector2(27, 27), CursorMode.ForceSoftware);
    }

    void Update()
    {
        if (Input.GetKey("escape"))
        {
            exitGame(0);
        }
    }    

    public void exitGame(int scene)
    {
        TestSpell ts = player.GetComponent<TestSpell>();
        PlayerStats ps = player.GetComponent<PlayerStats>();
        ts.saveData();
        ps.saveData();

        if(scene == 1 ^ scene == 0)
        {
            PlayerPrefs.SetInt("numProj", 1);
            PlayerPrefs.SetFloat("min", 20);
            PlayerPrefs.SetFloat("max", 30);

        }
        SceneManager.LoadScene(scene);
    }
}
