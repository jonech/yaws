﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Autofac;

using WarframeStatService.API;
using yaws.Core;
using yaws.Droid.Source;

namespace yaws.Droid
{
#if DEBUG
    [Application(Debuggable = true)]
#else
    // Disable debug on release
    [Application(Debuggable = false)]
#endif
    public class App : Application
    {
        public static IContainer Container { get; set; }

        public App(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer) { }

        public override void OnCreate()
        {
            base.OnCreate();

            ConfigureServices();
        }

        private void ConfigureServices()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<AppStateService>()
                   .AsSelf()
                   .SingleInstance();

            builder.RegisterType<AppSettings>()
                   .AsSelf();

            builder.RegisterType<AppHttpClientHandler>()
                   .AsSelf();

            builder.Register(c => new WarframeStatAPIFactory().Create())
                   .AsSelf();

            Container = builder.Build();
        }
    }
}