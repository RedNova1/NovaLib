using BepInEx;
using BepInEx.Bootstrap;
using BepInEx.Logging;
using System;
using UnityEngine;

namespace NovaLib
{
 public class Anticheat : MonoBehaviour
    {
        [SerializeField] PlayerUtilities playerUtil;
        [SerializeField] OnCheatsEnable cheats;
        
        [Tooltip("Plug Canvas Blocker Into here")] [SerializeField] GameObject missingDepUI; // if you wish to stop the game if you don't have the mod, put your Canvas Blocker in here.
        void prereq() // Disable the blocker UI if present
        {
            if (missingDepUI != null) { missingDepUI.gameObject.SetActive(false); playerUtil.EnablePlayer(); }
        }
        
        
        [Header("Module Toggles")]
        public bool TimeScaleLocked; //toggle for locking time scale
        [SerializeField] bool blockUnityExplorer;
        public void unityExplorerCheck() {
            foreach (var plugin in Chainloader.PluginInfos)
            {
                var metadata = plugin.Value.Metadata;
                if (metadata.GUID.Equals("com.sinai.unityexplorer"))
                {
                    Console.WriteLine($"Found {"com.sinai.unityexplorer"}, quitting");
                    AnticheatActivate();
                    break;
                }
            }
        }
        [SerializeField] bool blockCheats; public void cheatBlocker() { if (blockCheats == true) { cheats.enabled = true; } }


        [Header("Anticheat Actions")]
        [SerializeField] bool exitMap; public void kickPlayer() { if (exitMap == true) { playerUtil.QuitMap(); } }
        [SerializeField] bool disablePlayer; public void freezePlayer() { if (disablePlayer == true) { playerUtil.DisablePlayer(); } } //freezes game 

        [Header("TimeScale Lock")]
        [Min(0.1f)] public float timeScaleSet = 1; //what the timescale is locked to
        void timeScaleLock () { if (TimeScaleLocked == true) { Time.timeScale = timeScaleSet; } }
        public void changeTimeScale(float timeScale)
        {
            timeScaleSet = timeScale;
            timeScaleLock();
        }



        void Start()
        {
            prereq();
            unityExplorerCheck();
            cheatBlocker();
        }
        

 

        void Update()
        {
            timeScaleLock();
        }

        public void AnticheatActivate()
        {
            kickPlayer();
            freezePlayer();
        }

    }



   
}
