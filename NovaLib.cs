using BepInEx;
using BepInEx.Bootstrap;
using BepInEx.Logging;
using System;
using UnityEngine;

namespace NovaLib
{
    [BepInPlugin("RedNova.ULTRAKILL.NovaLib", "NovaLib", "1.0.0.0")]
    [BepInDependency("com.sinai.unityexplorer", BepInDependency.DependencyFlags.SoftDependency)]
    public class NovaLib : BaseUnityPlugin
    {
        private void Awake()
        {
            // Plugin startup logic
            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
        }

    }
    
 public class Anticheat : MonoBehaviour
    {
        [SerializeField] PlayerUtilities playerUtil; 
        [SerializeField] GameObject passThrough; // Canvas UI Blocker
        void Start()
        {
            foreach (var plugin in Chainloader.PluginInfos)
            {
                var metadata = plugin.Value.Metadata;
                if (metadata.GUID.Equals("com.sinai.unityexplorer"))
                {
                    Console.WriteLine($"Found {"com.sinai.unityexplorer"}, quitting");
                    playerUtil.QuitMap();
                    break;
                }
            }

            passThrough.SetActive(false);
        }
    }

   
}
