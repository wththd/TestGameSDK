#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.AddressableAssets.Build;
using UnityEngine;

namespace SDK.Builder
{
    [CreateAssetMenu(menuName = "AutoBuilder/AndroidConfg", fileName = "AndroidBuildConfig")]
    public class GameSDKAndroidBuildConfig : ScriptableObject
    {
        [SerializeField] private bool _devBuild;
        [SerializeField] private bool _symlinkLibraries;
        [SerializeField] private bool _connectWithProfiler;
        [SerializeField] private bool _isAab;
        [SerializeField] private string _buildsPath;
        [SerializeField] private ScriptableObject _addressableDataBuilder;

        public bool IsAab => _isAab;
        public IDataBuilder AddressableDataBuilder => _addressableDataBuilder as IDataBuilder;
        public string BuildsPath => _buildsPath;

        private void OnValidate()
        {
            if (_addressableDataBuilder != null && _addressableDataBuilder is not IDataBuilder)
            {
                Debug.LogError("Invalid data builder");
                _addressableDataBuilder = null;
            }
        }
        
        public BuildOptions CreateBuildOptions()
        {
            var result = BuildOptions.None;
            if (_devBuild) result |= BuildOptions.Development;
            if (_symlinkLibraries) result |= BuildOptions.SymlinkSources;
            if (_connectWithProfiler) result |= BuildOptions.ConnectWithProfiler;
            return result;
        }
    }
}
#endif