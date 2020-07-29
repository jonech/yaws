using System;
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
using Service.DataSource;
using Service.API;
using yaws.Core;
using yaws.Droid.Source;

namespace yaws.Droid
{
    [Application]
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

            builder.RegisterType<WorldStateDataSource>()
                   .AsSelf();

            builder.RegisterType<WorldStateRepository>()
                   .AsSelf();

            builder.RegisterType<AppStateService>()
                   .AsSelf()
                   .SingleInstance();

            builder.RegisterType<AppSettings>()
                   .AsSelf();

            builder.RegisterType<AppHttpClientHandler>()
                   .AsSelf();

            builder.Register(c => new APIFactory().Create())
                   .AsSelf();

            Container = builder.Build();
        }
    }
}