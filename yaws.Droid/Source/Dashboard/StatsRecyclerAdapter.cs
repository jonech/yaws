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
//using yaws.Core.ViewModel;
using yaws.Droid.Source.Dashboard.ViewHolder;
using WarframeStatService.Entity.Interface;
using WarframeStatService.Constant;

namespace yaws.Droid.Source.Dashboard
{
    public class StatsRecyclerAdapter : RecyclerView.Adapter
    {
        private CompositeDisposable timeSubscriptions;
        public List<IStat> ItemSource { get; private set; }

        public StatsRecyclerAdapter()
        {
            ItemSource = new List<IStat>();
        }

        public override int ItemCount => ItemSource.Count;

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            if (holder is StatViewHolder)
            {
                var viewHolder = holder as StatViewHolder;
                viewHolder.Bind(ItemSource[position], this);
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
                case Resource.Layout.item_fissure:
                    return new FissureViewHolder(view);
                case Resource.Layout.item_invasion:
                    return new InvasionViewHolder(view);

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
                case WFStatType.CetusCycle:
                    return Resource.Layout.item_cetus_cycle;
                case WFStatType.Arbitration:
                    return Resource.Layout.item_arbitration;
                case WFStatType.VallisCycle:
                    return Resource.Layout.item_vallis_cycle;
                case WFStatType.EarthCycle:
                    return Resource.Layout.item_earth_cycle;
                case WFStatType.SentientOutpost:
                    return Resource.Layout.item_sentient_outpost;
                case WFStatType.Fissure:
                    return Resource.Layout.item_fissure;
                case WFStatType.Invasion:
                    return Resource.Layout.item_invasion;

                case WFStatType.CetusBounty:
                case WFStatType.VallisBounty:
                    return Resource.Layout.item_bounty;

                case WFStatType.Job:
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


        public override void OnViewDetachedFromWindow(Java.Lang.Object holder)
        {
            base.OnViewDetachedFromWindow(holder);
        }

        public void AddItems(List<IStat> items)
        {
            var insertedPos = ItemCount;

            ItemSource.AddRange(items);
            NotifyItemInserted(insertedPos);
        }

        public void SetItems(List<IStat> items)
        {
            ItemSource.Clear();
            ItemSource.AddRange(items);

            NotifyDataSetChanged();
        }

        public void AddTimeSubscription(IDisposable disposable)
        {
            if (timeSubscriptions == null)
                timeSubscriptions = new CompositeDisposable();

            timeSubscriptions.Add(disposable);
        }

        public void DisposeSubscriptions()
        {
            if (timeSubscriptions != null)
                timeSubscriptions.Dispose();
        }
    }
}