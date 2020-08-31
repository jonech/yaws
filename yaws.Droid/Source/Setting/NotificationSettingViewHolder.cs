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
using Android.Widget;

namespace yaws.Droid.Source.Setting
{
    public class NotificationSettingViewHolder : RecyclerView.ViewHolder
    {
        private readonly SwitchCompat settingSwitch;

        private readonly IValueChangedListener valueChangedListener;

        public NotificationSetting Model { get; private set; }
        public NotificationSettingViewHolder(View itemView, IValueChangedListener listener) : base(itemView)
        {
            settingSwitch = itemView.FindViewById<SwitchCompat>(Resource.Id.switch_setting);
            valueChangedListener = listener;
        }


        public void Bind(NotificationSetting model)
        {
            settingSwitch.CheckedChange -= SwitchCheckedChange;
            Model = model;
            settingSwitch.Text = model.Name;
            settingSwitch.Checked = model.IsChecked;
            settingSwitch.CheckedChange += SwitchCheckedChange;
        }

        private void SwitchCheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            Model.IsChecked = e.IsChecked;
            valueChangedListener.OnNotificationSettingChanged(Model);
        }

        public interface IValueChangedListener
        {
            void OnNotificationSettingChanged(NotificationSetting model);
        }
    }
}