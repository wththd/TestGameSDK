using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Video;

[RequireComponent(typeof(VideoPlayer))]
public class BackgroundVideo : MonoBehaviour
{
    [SerializeField] private AssetReferenceT<VideoClip>[] _videoClips;
    private VideoPlayer _videoPlayer;

    private async void Start()
    {
        _videoPlayer = GetComponent<VideoPlayer>();

        _videoPlayer.clip = await PickRandomVideoClipAsync();
        _videoPlayer.Play();
    }

    private async Task<VideoClip> PickRandomVideoClipAsync()
    {
        var clipReference = _videoClips[Random.Range(0, _videoClips.Length)];
        var result = await clipReference.LoadAssetAsync().Task;
        return result;
    }
}