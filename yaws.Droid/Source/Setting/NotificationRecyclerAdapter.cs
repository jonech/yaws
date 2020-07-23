using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Firebase.Messaging;
using yaws.Core;
using yaws.Droid.Source.Util;

namespace yaws.Droid.Source.Setting
{
    public class NotificationRecyclerAdapter : RecyclerView.Adapter, NotificationSettingViewHolder.IValueChangedListener
    {
        private readonly AppSettings appSettings;
        public List<NotificationSetting> ItemSource { get; private set; }
        public override int ItemCount => ItemSource.Count;

        public NotificationRecyclerAdapter(List<NotificationSetting> items, AppSettings appSettings)
        {
            ItemSource = items;
            this.appSettings = appSettings;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            if (holder is NotificationSettingViewHolder)
            {
                var viewHolder = holder as NotificationSettingViewHolder;
                viewHolder.Bind(ItemSource[position]);
            }
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            LayoutInflater inflater = LayoutInflater.From(parent.Context);
            View view = inflater.Inflate(viewType, parent, false);
            return new NotificationSettingViewHolder(view, this);
        }

        public override int GetItemViewType(int position)
        {
            return Resource.Layout.item_setting_switch;
        }

        public void OnNotificationSettingChanged(NotificationSetting model)
        {
            if (model.IsChecked)
            {
                FirebaseMessaging.Instance.SubscribeToTopic(appSettings.Platform, model.Topic);
            }
            else
            {
                FirebaseMessaging.Instance.UnsubscribeFromTopic(appSettings.Platform, model.Topic);
            }

            appSettings.SetSwitchNotificationSetting(model.Topic, model.IsChecked);
        }
    }
}