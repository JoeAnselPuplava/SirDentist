using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class video_switch_scene : MonoBehaviour
{
    // Start is called before the first frame update
    private VideoPlayer videoPlayer;

    private void Start()
    {
        videoPlayer.loopPointReached += EndReached;
        videoPlayer.Play();
    }

    private void EndReached(VideoPlayer vp)
    {
        SceneManager.LoadScene("Tutorial_1_Arted_V2");
    }
}
