using UnityEngine;


public class Portal : Collidable
{
    [SerializeField]
    private string[] sceneNames;

    protected override void OnCollide(Collider2D col)
    {
        if(col.name == "Player")
        {

            GameManager.instance.SaveState();

            // Select a random dungeon name from array
            string sceneName = sceneNames[Random.Range(0, sceneNames.Length)];

            // Load the selected dungeon scene
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
        }
    }

}
