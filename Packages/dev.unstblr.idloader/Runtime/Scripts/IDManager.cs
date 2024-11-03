
using UdonSharp;
using UnityEngine;
using Unstblr.Common;
using VRC.SDKBase;
using VRC.Udon;

namespace Unstblr.IDLoader
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class IDManager : UdonSharpBehaviour
    {
        public bool EnableLoadFromURL = true;
        [Space]
        [Header("Set your URL here if EnableLoadFromURL is true.")]
        public VRCUrl url;
        [Space]
        [Header("If EnableLoadFromURL is false. Set your display name below.")]
        public string[] displayNames;
        [Space]
        [Header("Make sure your scripts have custom event \"havePerm\".")]
        public UdonBehaviour[] listeners;
        StringLoader stringLoader;
        void Start()
        {
            stringLoader = gameObject.GetComponentInChildren<StringLoader>();
            if (EnableLoadFromURL)
            {
                if (stringLoader != null) { stringLoader._Start(); }
            }
            else
            {
                if (displayNames != null) GetPerm();
            }
        }
        public void GetDisplayName()
        {
            displayNames = stringLoader.displayNames;
        }
        public void GetPerm()
        {
            foreach (var name in displayNames)
            {
                if (Networking.LocalPlayer.displayName == name)
                {
                    foreach (UdonBehaviour listener in listeners)
                    {
                        listener.SendCustomEvent("havePerm");
                    }
                }
            }
        }
    }
}