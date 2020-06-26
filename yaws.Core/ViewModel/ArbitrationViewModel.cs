using Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace yaws.Core.ViewModel
{
    public class ArbitrationViewModel : ExpirableViewModel
    {
        public override StatType StatType => StatType.Arbitration;
        public override string Name => "Arbitration";

        public string Enemy { get; set; }
        public string Type { get; set; }
        public bool IsArchwing { get; set; }
        public bool IsSharkwing { get; set; }
        public string Node { get; set; }

        public ArbitrationViewModel(Arbitration model) : base(model)
        {
            Id = model.Id;
            Enemy = model.Enemy;
            Type = model.Type;
            IsArchwing = model.IsArchwing;
            IsSharkwing = model.IsSharkwing;
            Node = model.Node;
        }

    }
}
