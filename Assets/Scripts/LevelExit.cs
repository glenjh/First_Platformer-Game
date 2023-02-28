using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [SerializeField] AudioClip levelExitSFX;
    bool isPlayed = false;
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player" && !isPlayed)
        {
            AudioSource.PlayClipAtPoint(levelExitSFX, Camera.main.transform.position);
            isPlayed = true;
            StartCoroutine(LoadNextLevel());
        }
    }

    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSecondsRealtime(3);
        
        int currStageIndex = SceneManager.GetActiveScene().buildIndex;
        int nextStageIndex = currStageIndex + 1;

        if (nextStageIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextStageIndex = 0;
        }
        
        FindObjectOfType<InteractableManager>().Restart();
        SceneManager.LoadScene(nextStageIndex);
    }
}
