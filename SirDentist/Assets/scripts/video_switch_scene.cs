using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class video_switch_scene : MonoBehaviour
{
    // Start is called before the first frame update
    public VideoPlayer videoPlayer;
    public string nextScene;

    private void Start()
    {
        videoPlayer.loopPointReached += EndReached;
        videoPlayer.Play();
        print("playing");
    }

    private void EndReached(VideoPlayer vp)
    {
        SceneManager.LoadScene(nextScene);
    }
}
