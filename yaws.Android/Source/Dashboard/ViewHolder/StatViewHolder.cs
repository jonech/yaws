using System;
using System.Collections.Generic;
using System.Text;

using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

using Core.ViewModel;

namespace yaws.Android.Source.Dashboard.ViewHolder
{

    public abstract class StatViewHolder : RecyclerView.ViewHolder
    {
        public TextView TitleTextView { get; protected set; }

        protected StatViewHolder(View itemView) : base(itemView)
        {
        }

        public abstract void Bind(ViewModelBase item, StatsRecyclerAdapter adapter);
    }
}