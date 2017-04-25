namespace Fanex.Razorbill.Module.Common.Models
{
    public class ProductModel : ActivationGroupModel
    {
        public string ViewPTLink { get; set; }

        public string ViewCommLink { get; set; }

        public bool HasReport { get; set; }

        public bool ShowPTIcon { get; set; }

        public string CustomerSettingLink { get; set; }
    }
}
