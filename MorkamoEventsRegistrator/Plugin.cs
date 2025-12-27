using System;
using System.Collections.Generic;
using DarkAPI;
using Exiled.API.Enums;
using Exiled.API.Features;
using MorkamoEventsRegistrator.Components;

namespace MorkamoEventsRegistrator
{
    public class Plugin : Plugin<Config>
    {
        public override string Name => nameof(MorkamoEventsRegistrator);
        public override string Prefix => Name;
        public override string Author => "Morkamo";
        public override Version RequiredExiledVersion => new(9, 12, 1);
        public override Version Version => new(1, 0, 0);
        public override PluginPriority Priority => PluginPriority.Lowest;

        public static Plugin Instance;
        public static readonly List<IEventsRegistrator> EventsRegistrators = new();

        public override void OnEnabled()
        {
            Instance = this;
            
            foreach(var events in EventsRegistrators)
                events.RegisterEvents();
            
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            foreach(var events in EventsRegistrators)
                events.UnregisterEvents();
            
            EventsRegistrators.Clear();
            
            Instance = null;
            base.OnDisabled();
        }

        public static void AddRegistrator(IEventsRegistrator registrator) => EventsRegistrators.Add(registrator);
        public static void AddRegistrator(List<IEventsRegistrator> registrators) => EventsRegistrators.ForEach(registrators.Add);
        public static void RemoveRegistrator(IEventsRegistrator registrator) => EventsRegistrators.Remove(registrator);
        public static void RemoveRegistrator(List<IEventsRegistrator> registrators)
        {
            foreach (var registrator in registrators) EventsRegistrators.Remove(registrator);
        }
    }
}