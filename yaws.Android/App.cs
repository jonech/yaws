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
using Shared.DataSource;
using Shared.API;
using Core;

namespace yaws.Android
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

            builder.RegisterType<AppHttpClientHandler>()
                   .AsSelf();

            builder.Register(c =>
                             new APIFactory(
                                 c.Resolve<AppHttpClientHandler>()).Create())
                   .AsSelf();

            Container = builder.Build();
        }
    }
}