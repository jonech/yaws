using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Core.ViewModel;
using yaws.Android.Source.Dashboard.ViewHolder;

namespace yaws.Android.Source.Dashboard
{
    public class StatsRecyclerAdapter : RecyclerView.Adapter
    {
        private CompositeDisposable timeSubscriptions;
        public List<ViewModelBase> ItemSource { get; private set; }

        public StatsRecyclerAdapter()
        {
            ItemSource = new List<ViewModelBase>();
        }

        public override int ItemCount => ItemSource.Count;

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            if (holder is StatViewHolder)
            {
                var viewHolder = holder as StatViewHolder;
                viewHolder.Bind(ItemSource[position]);
            }

        }


        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            LayoutInflater inflater = LayoutInflater.From(parent.Context);
            View view = inflater.Inflate(viewType, parent, false);

            switch (viewType)
            {
                case Resource.Layout.item_cetus_cycle:
                    return new CetusCycleViewHolder(view);
                case Resource.Layout.item_arbitration:
                    return new ArbitrationViewHolder(view);
                case Resource.Layout.item_earth_cycle:
                    return new EarthCycleViewHolder(view);
                case Resource.Layout.item_vallis_cycle:
                    return new VallisCycleViewHolder(view);
                case Resource.Layout.item_sentient_outpost:
                    return new SentientOutpostViewHolder(view);

                case Resource.Layout.item_bounty:
                    return new BountyViewHolder(view);
                case Resource.Layout.item_bounty_job:
                    return new BountyJobViewHolder(view);
                default:
                    throw new InvalidOperationException("This type of layout is not supported");
            }
        }

        public override int GetItemViewType(int position)
        {
            var type = ItemSource[position].StatType;
            switch (type)
            {
                case StatType.CetusCycle:
                    return Resource.Layout.item_cetus_cycle;
                case StatType.Arbitration:
                    return Resource.Layout.item_arbitration;
                case StatType.VallisCycle:
                    return Resource.Layout.item_vallis_cycle;
                case StatType.EarthCycle:
                    return Resource.Layout.item_earth_cycle;
                case StatType.SentientOutpost:
                    return Resource.Layout.item_sentient_outpost;

                case StatType.CetusBounty:
                case StatType.VallisBounty:
                    return Resource.Layout.item_bounty;

                case StatType.BountyJob:
                    return Resource.Layout.item_bounty_job;
                default:
                    return 0;
            }

            throw new NotImplementedException();
        }

        public override void OnDetachedFromRecyclerView(RecyclerView recyclerView)
        {
            base.OnDetachedFromRecyclerView(recyclerView);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        public override void OnViewDetachedFromWindow(Java.Lang.Object holder)
        {
            base.OnViewDetachedFromWindow(holder);
        }

        public void AddItems(List<ViewModelBase> items)
        {
            var insertedPos = ItemCount;

            ItemSource.AddRange(items);
            NotifyItemInserted(insertedPos);
        }

        public void SetItems(List<ViewModelBase> items)
        {
            ItemSource.Clear();
            ItemSource.AddRange(items);

            NotifyDataSetChanged();
        }
    }
}