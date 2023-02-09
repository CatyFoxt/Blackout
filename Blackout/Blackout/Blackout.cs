using Exiled.API.Enums;
using Features = Exiled.API.Features;
using ServerEvents = Exiled.Events.Handlers.Server;
using PlayerEvents = Exiled.Events.Handlers.Player;
using Exiled.Events.EventArgs;
using MEC;
using System.Linq;
using Exiled.Events.EventArgs.Player;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Exiled.API.Features;
using UnityEngine;
using Blackout;
using GameCore;
using Exiled.Events.Handlers;
using Exiled.Events.EventArgs.Server;
using Exiled.Events.EventArgs.Scp096;
using PluginAPI.Core;
using server = Exiled.Events.Handlers.Server;
using Log = Exiled.API.Features.Log;
using Cassie = Exiled.API.Features.Cassie;
using Random = System.Random;
using Subtitles;
using Mono.Unix.Native;

namespace Blackout
{
    public class Plugin : Features.Plugin<Config>
    {
        public override string Name { get; } = "Blackout";
        public override string Author { get; } = "CatyFoxt";
        public override PluginPriority Priority { get; } = PluginPriority.High;

        private static readonly Plugin InstanceValue = new Plugin();

        public static Plugin StaticInstance => InstanceValue;

        public int timeBetweenBlackouts;

        public override void OnEnabled()
        {
            server.RoundStarted += OnRoundStart;
            server.RoundEnded += OnRoundEnd;
        }
        public override void OnDisabled()
        {
            server.RoundStarted -= OnRoundStart;
            server.RoundEnded -= OnRoundEnd;
        }

        public void OnRoundStart()
        {
            Timing.KillCoroutines("BlackoutStart");
            Timing.RunCoroutine(Blackout(), "BlackoutStart");
        }

        public void OnRoundEnd(RoundEndedEventArgs ev)
        {
            Timing.KillCoroutines("BlackoutStart");
        }

        public IEnumerator<float> Blackout()
        {
            for (; ; )
            {
                // Wait for blackout
                yield return Timing.WaitForSeconds(Config.TimeBetweenBlackouts);

                // Blackout Message
                Cassie.GlitchyMessage("SYSTEM FAILURE REACTIVATING IN 20 SECONDS .", 1, 1);

                //Blackout Happen              
                Log.Info("Blackout Started");
                foreach (var room in Room.List)
                {
                    room.TurnOffLights(Config.BlackoutDuration);
                    room.LockDown(Config.BlackoutDuration, DoorLockType.Regular079);
                }

                // Wait for Blackout to end.
                yield return Timing.WaitForSeconds(Config.BlackoutDuration);

                // Announce that systems are online.
                Cassie.Message("ALL SYSTEMS ARE ONLINE .", false, false, true);
                Log.Info("Blackout Ended");
            }
        }

        private Color Color32(int v1, int v2, int v3, int v4)
        {
            throw new NotImplementedException();
        }
    }
}