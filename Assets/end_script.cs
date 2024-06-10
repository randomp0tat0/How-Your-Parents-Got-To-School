using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class end_script : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            string name = SceneManager.GetActiveScene().name;
            int sceneNumber = int.Parse(name.Substring(2)) + 1;
            string sceneToLoad = "lv" + sceneNumber;
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
