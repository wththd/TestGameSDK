#if UNITY_EDITOR
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Build;
using UnityEditor.AddressableAssets.Settings;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace SDK.Builder
{
    public class GameSDKAndroidBuilder
    {
        [MenuItem("TestSI/BuildAndroid")]
        public static void BuildAndroid()
        {
            var configPath = AssetDatabase.FindAssets($"t:{nameof(GameSDKAndroidBuildConfig)}");
            if (configPath.Length == 0)
            {
                Debug.LogError("No config");
                EditorApplication.Exit(-1);
            }

            var config = AssetDatabase.LoadAssetAtPath<GameSDKAndroidBuildConfig>(AssetDatabase.GUIDToAssetPath(configPath[0]));

            BuildAddressables(config.AddressableDataBuilder);

            ClearBuildFolder(config.BuildsPath);
            var opts = new BuildPlayerOptions
            {
                scenes = EditorBuildSettings.scenes.Select(scene => scene.path).ToArray(),
                target = BuildTarget.Android,
                options = config.CreateBuildOptions()
            };
            
            PlayerSettings.Android.androidIsGame = true;
            EditorUserBuildSettings.buildAppBundle = config.IsAab;
            
            var ext = config.IsAab ? "aab" : "apk";
            opts.locationPathName = $"{config.BuildsPath}/{PlayerSettings.applicationIdentifier}.{ext}";
            
            var buildReport = BuildPipeline.BuildPlayer(opts);
            if (buildReport.summary.result == BuildResult.Succeeded)
            {
                EditorApplication.Exit(0);
            }
            else
            {
                EditorApplication.Exit(-1);
            }
        }

        private static void ClearBuildFolder(string path)
        {
            var directoryInfo = new DirectoryInfo(Path.Combine(Application.dataPath.Replace("/Assets", ""), path));
            foreach (var file in directoryInfo.GetFiles())
            {
                File.Delete(file.FullName);
            }
            
            foreach (var directory in directoryInfo.GetDirectories())
            {
                directory.Delete(true); 
            }
        }

        private static void BuildAddressables(IDataBuilder builder)
        {
            AddressableAssetSettings.CleanPlayerContent(AddressableAssetSettingsDefaultObject.Settings.ActivePlayerDataBuilder);
            if (builder != null)
            {
                var settings = AddressableAssetSettingsDefaultObject.Settings;

                var activeBuilderSet = false;
                for (var i = 0; i < settings.DataBuilders.Count; i++)
                {
                    if (settings.GetDataBuilder(i) == builder)
                    {
                        settings.ActivePlayerDataBuilderIndex = i;
                        activeBuilderSet = true;
                    }
                }

                if (!activeBuilderSet)
                {
                    AddressableAssetSettingsDefaultObject.Settings.AddDataBuilder(builder);
                    settings.ActivePlayerDataBuilderIndex =
                        AddressableAssetSettingsDefaultObject.Settings.DataBuilders.Count - 1;
                }
            }

            AddressableAssetSettings.BuildPlayerContent();
        }
    }

}
#endif