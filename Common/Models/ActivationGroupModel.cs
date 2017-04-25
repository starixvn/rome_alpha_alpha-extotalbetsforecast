namespace Fanex.Razorbill.Module.Common.Models
{
    using System.Collections.Generic;

    public class ActivationGroupModel : ActivationElementModel
    {
        public List<ActivationGroupModel> ActivationGroups { get; set; }
    }
}