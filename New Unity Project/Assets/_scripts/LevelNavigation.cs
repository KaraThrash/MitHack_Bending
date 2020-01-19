using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelNavigation : MonoBehaviour
{
    /*
     * Difficulty = input - 1,3, or 5 (Difficult, Medium, Easy)
Lives = difficulty

Scenes = [list of scenes]

Next Room = (random scene)

Remove Next room from Scenes

Build room Next Room

If fail:
	Reset current Room
	If lives = 0:
		DETH and full restart/quit

When enter Next Room:
	Close Door behind
	Current Room = Next Room
	Next Room = (see above)
	Previous Room = GOes to the HEck relm
If # of completed puzzles > difficulty:
	Winner Winner Chicken Dinner

     */
    public LevelManager levelManager;
    public bool goToNext;
    public List<string> scenes;

    // Start is called before the first frame update
    void Start()
    {
        scenes.Add("level1_meditate");
        scenes.Add("Level8_2");
        scenes.Add("ObjectRoomScene");
        scenes.Add("TempleDor2");
    }

    // Update is called once per frame
    void Update()
    {
        if (goToNext)
        {
            string cur = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(scenes[Random.Range(0, scenes.Count)]);
            scenes.Remove(cur);
        }

    }
}
    
