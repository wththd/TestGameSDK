using UnityEngine;
using UnityEngine.Video;

[RequireComponent(typeof(VideoPlayer))]
public class BackgroundVideo : MonoBehaviour
{
    private VideoClip[] _videoClips;
    private VideoPlayer _videoPlayer;

    private void Start()
    {
        _videoClips = Resources.LoadAll<VideoClip>("Videos");

        _videoPlayer = GetComponent<VideoPlayer>();

        _videoPlayer.clip = PickRandomVideoClip();
        _videoPlayer.Play();
    }

    private VideoClip PickRandomVideoClip()
    {
        return _videoClips[Random.Range(0, _videoClips.Length)];
    }
}