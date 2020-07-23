using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Playslector : MonoBehaviour
{

    public Text userDisplay;
    public Text levelDisplay;
    public Text avatarDisplay;

    //this is to automatically populate levels right when we start our game
    private void Awake()
    {
        if (DBManager.username == null) //this and 17 line is to check if we are not logged in and have reached here
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
        userDisplay.text = "User: " + DBManager.username;
        levelDisplay.text = "Level: " + DBManager.level;
        avatarDisplay.text = "Avatar: " + DBManager.avatar;
    }
    public List<GameObject> models;
    //Default Index of the model
    private int selectionIndex;


    private void Start()
    {
        models = new List<GameObject>();
        foreach (Transform t in transform)
        {
            models.Add(t.gameObject);
            //       models[selectionIndex].SetActive(false);
            t.gameObject.SetActive(false);
        }
        selectionIndex = DBManager.avatar;
        models[selectionIndex].SetActive(true);
        //       models.transform.position = SpawnPoint.transform.position;

    }

    public void Loggoff()
    {
       if (Input.GetKeyDown(KeyCode.E))
        {
        DBManager.LogOut(); //if we are quitting our game then log out
        UnityEngine.SceneManagement.SceneManager.LoadScene(0); //go back to the main menu
        }

    }

}
