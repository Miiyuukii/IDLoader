
using System;
using UdonSharp;
using UnityEngine;
using Unstblr.IDLoader;
using VRC.SDK3.StringLoading;
using VRC.SDKBase;
using VRC.Udon;
using VRC.Udon.Common.Interfaces;

namespace Unstblr.Common
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class StringLoader : UdonSharpBehaviour
    {
        public VRCUrl url;
        public bool Reload;
        public float Cooldown = 60f;
        public IDManager id;
        public string raw;
        public string[] displayNames;
        void Start()
        {
            id = gameObject.GetComponentInParent<IDManager>();
            url = id.url;
        }
        public void _Start()
        {
            VRCStringDownloader.LoadUrl(url, (IUdonEventReceiver)this);
        }
        public override void OnStringLoadSuccess(IVRCStringDownload result)
        {
            raw = result.Result;
            if (Reload) { SendCustomEventDelayedSeconds(nameof(LoadAgain), Cooldown); }
            StringList();
        }
        public override void OnStringLoadError(IVRCStringDownload result)
        {
            LoadAgain();
        }
        public void StringList()
        {
            displayNames = raw.Split(new string[] { "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            id.GetDisplayName();
        }
        public void LoadAgain()
        {
            VRCStringDownloader.LoadUrl(url, (IUdonEventReceiver)this);
        }
    }
}