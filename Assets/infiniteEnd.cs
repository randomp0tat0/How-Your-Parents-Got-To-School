using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InfiniteEnd : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            string name = SceneManager.GetActiveScene().name;
            int sceneNumber = int.Parse(name.Substring(2)) + 1;
            string sceneToLoad = "lv" + sceneNumber;

            // case: next level exists
            if(sceneNumber <= 10){ // replace 7 with the number of the level before infinite level generation
                SceneManager.LoadScene(sceneToLoad);
            }
            else{ // case: reload same level (infinite level generation)
                SceneManager.LoadScene("lv" + (sceneNumber - 1));
            }
        }
    }
}
