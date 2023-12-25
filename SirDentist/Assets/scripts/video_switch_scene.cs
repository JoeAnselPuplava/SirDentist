using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class video_switch_scene : MonoBehaviour
{
    // Start is called before the first frame update
    //public VideoPlayer videoPlayer;
    public string nextScene;

    public GameObject[] slides;

    bool stay = false;
    private int counter = 0;

    public float delay = 3f;
    private void Start()
    {
        //videoPlayer.loopPointReached += EndReached;
        //videoPlayer.Play();
        //print("playing");
        
    }

    void Update(){
        if(!stay && counter == slides.Length){

            StartCoroutine(sceneswitch());
        }
        if(!stay && counter < slides.Length){
            stay = true;
            StartCoroutine(slideswitch());
        }
    }

    IEnumerator slideswitch(){
        foreach(GameObject slide in slides){
            slide.SetActive(false);
        }
        slides[counter].SetActive(true);
        yield return new WaitForSeconds(delay);
        counter++;
        stay = false;
        UnityEngine.Debug.Log(counter);    
    }
    
    IEnumerator sceneswitch(){
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(nextScene);
    }
}
