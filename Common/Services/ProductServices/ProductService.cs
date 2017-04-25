using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Fanex.Data;
using Fanex.Products;
using Fanex.Products.Configurations;
using Fanex.Razorbill.Module.Common.Criteria;
using Fanex.Razorbill.Module.Common.Helpers;
using Fanex.Razorbill.Module.Common.LocalResources;
using Fanex.Razorbill.Module.Common.Models;
using Sunplus.Agent.Model.BL.Definitions;
using Sunplus.Agent.Model.Definitions;
using Sunplus.Agent.Model.Entities.Permission;
using Sunplus.Agent.Utilities;

namespace Fanex.Razorbill.Module.Common.Services.ProductServices
{
    public class ProductService
    {
        private const string ResourceKeyProductName = "product_name_";

        private const string ResourceKeyLiveName = "live_name_";

        public static string LiveTypeName(int liveType)
        {
            return Module_Common_Lang.ResourceManager.GetString(string.Format("{0}{1}", ResourceKeyLiveName, liveType)) ?? string.Empty;
        }

        public static string ProductName(int productId)
        {
            return Module_Common_Lang.ResourceManager.GetString(string.Format("{0}{1}", ResourceKeyProductName, productId)) ?? string.Empty;
        }

        #region Products Section

        public IList<KeyValuePair<string, bool>> GetProductSPParamsOfConsolidationReportBySport(IDictionary<int, bool> permissions, int sportId)
        {
            return GetActiveReportSportIdProducts(permissions)
                .Select(p => new KeyValuePair<string, bool>(p.ReportSPParam, p.SportId == sportId))
                .ToList();
        }

        public IList<Tuple<int, int>> GetSportProductMapping(IDictionary<int, bool> permissions)
        {
            return GetActiveReportSportIdProducts(permissions)
                .Select(p => new Tuple<int, int>(p.SportId, p.Id))
                .ToList();
        }

        public string GetViewCommProductSelector(int selectedProductId)
        {
            var options = new StringBuilder(512);

            var products = GetActiveViewCommLinkProducts();

            foreach (ProductElement product in products)
            {
                options.Append(string.Format(
                "<option id=\"product-selector-{0}\" value=\"{1}\"{2}>{3}</option>",
                product.Id,
                product.ViewCommLink,
                product.Id == selectedProductId ? " selected=\"selected\"" : string.Empty,
                CacheRemotingRef.GetProductName(product.Id)));
            }

            return string.Format(
                "<select id=\"dropdown-product\" onblur=\"commProductDropdownBlur();\"" +
                " onclick=\"commView(this.value);\" onchange=\"commProductChanged(this);\">{0}</select>",
                options.ToString());
        }

        public string GetViewPTProductSelector(int selectedProductId)
        {
            var options = new StringBuilder(512);

            var products = GetActiveViewPTLinkProducts();

            foreach (ProductElement product in products)
            {
                options.Append(string.Format(
                    "<option id=\"product-selector-{0}\" value=\"{1}\"{2}>{3}</option>",
                    product.Id,
                    product.ViewPTLink,
                    product.Id == selectedProductId ? " selected=\"selected\"" : string.Empty,
                    CacheRemotingRef.GetProductName(product.Id)));
            }

            return string.Format(
                "<select id=\"dropdown-product\" onblur=\"ptProductDropdownBlur();\"" +
                " onclick=\"ptView(this.value);\" onchange=\"ptProductChanged(this);\">{0}</select>",
                options.ToString());
        }

        public IEnumerable<ProductFilter> GetProductsFilter(IDictionary<int, bool> permissions)
        {
            var checkedProducts = GetCheckedProductsFromRequest();

            return GetActiveReportProducts(permissions)
                .Select(p => new ProductFilter
                {
                    Id = p.Id,
                    Text = CacheRemotingRef.GetProductName(p.Id),
                    Checked = checkedProducts == null || checkedProducts.ContainsKey(p.Id),
                    ReportSPParam = p.ReportSPParam
                });
        }

        public IEnumerable<ProductFilter> GetProductFiltersForMatchWinLossDetail()
        {
            var checkedProducts = GetCheckedProductsFromRequest();

            var productIds = new int[]
            {
                ProductIdConstants.Sportsbook,
                ProductIdConstants.Racing,
                ProductIdConstants.NumberGame,
                ProductIdConstants.LiveCasino,
                ProductIdConstants.VirtualSports
            };

            return GetActiveReportProducts()
                .Where(p => productIds.Contains(p.Id))
                .Select(p => new ProductFilter
                {
                    Id = p.Id,
                    Text = CacheRemotingRef.GetProductName(p.Id),
                    Checked = checkedProducts == null || checkedProducts.ContainsKey(p.Id),
                    ReportSPParam = p.ReportSPParam
                });
        }

        public IDictionary<int, bool> GetProductsByGroupHeadline(int custId, int showAll)
        {
            IDictionary<int, bool> permissions = new Dictionary<int, bool>();

            var criteria = new Age_Rpt_ConsolidatedGroupProductSel
            {
                CustId = custId,
                IsShow = showAll
            };

            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                using (var reader = db.ExecuteReader(criteria.GetSpParams()))
                {
                    bool isShow;

                    while (reader.Read())
                    {
                        isShow = Convert.ToBoolean(reader["isShow"]);

                        if (criteria.IsShow == -1 || (criteria.IsShow == 1 && isShow) || (criteria.IsShow == 0 && !isShow))
                        {
                            permissions.Add(Convert.ToInt32(reader["productid"]), isShow);
                        }
                    }
                }
            }

            return permissions;
        }

        private IDictionary<int, bool> GetProductsPermissionFromSession()
        {
            IDictionary<int, bool> permissions = new Dictionary<int, bool>
            {
                { ProductIdConstants.Sportsbook, true },
                { ProductIdConstants.Racing, UserPermission.IsShowRacing },
                { ProductIdConstants.NumberGame, UserPermission.IsShowNumberGame },
                { ProductIdConstants.LiveCasino, UserPermission.IsShowLiveCasino },
                { ProductIdConstants.VirtualSports, UserPermission.IsShowVirtualSports },
                { ProductIdConstants.RNGCasino, UserPermission.IsShowCasino },
                { ProductIdConstants.Bingo, UserPermission.IsShowBingo && !AppSettings.DisabledBingoCommission },
                { ProductIdConstants.Keno, UserPermission.IsShowKenoSetting },
                { ProductIdConstants.P2P, AppSettings.IsEnableP2P },
                { ProductIdConstants.ProgressiveBonus, UserPermission.IsShowCasino },
                { ProductIdConstants.GoldDeluxe, UserPermission.IsShowGoldDeluxeSettingForCredit },
                { ProductIdConstants.ColossusBets, UserPermission.IsShowColossusBets },
                { ProductIdConstants.Sportsbook2, UserPermission.IsShowSportsbook2 },
                { ProductIdConstants.CockFighting, UserPermission.IsEnableProduct(ProductIdConstants.CockFighting) },
                { ProductIdConstants.AllBet, UserPermission.IsEnableProduct(ProductIdConstants.AllBet) && UserPermission.IsSyncedProduct(ProductIdConstants.AllBet) },
                { ProductIdConstants.Voidbridge, UserPermission.IsEnableProduct(ProductIdConstants.Voidbridge) && UserPermission.IsSyncedProduct(ProductIdConstants.Voidbridge) },
               // { ProductIdConstants.FishingWorld, UserPermission.IsEnableProduct(ProductIdConstants.FishingWorld) && UserPermission.IsSyncedProduct(ProductIdConstants.FishingWorld) }
            };

            return GetActiveNewFlowProducts(permissions);
        }

        private IDictionary<int, bool> GetActiveNewFlowProducts(IDictionary<int, bool> permissions)
        {
            foreach (var product in ProductManager.Products)
            {
                permissions.Add(product.Id, product.ShowReport);
            }

            return permissions;
        }

        private IDictionary<int, bool> GetCheckedProductsFromRequest()
        {
            var request = HttpContext.Current.Request;

            if (string.IsNullOrEmpty(request["FilterPostback"]))
            {
                return null;
            }

            string paramValue = request["productIds"];

            if (string.IsNullOrEmpty(paramValue))
            {
                return new Dictionary<int, bool>();
            }

            int productId;
            IDictionary<int, bool> checkedProductIds = new Dictionary<int, bool>(16);

            string[] productIdsArray = paramValue.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string item in productIdsArray)
            {
                if (!int.TryParse(item, out productId))
                {
                    Logs.WriteInfo("Invalid request parameter: ProductIds=" + paramValue);
                    break;
                }

                checkedProductIds[productId] = true;
            }

            return checkedProductIds;
        }

        private IEnumerable<ProductElement> GetActiveViewCommLinkProducts()
        {
            return GetActiveProductElements()
                .Where(p => !string.IsNullOrEmpty(p.ViewCommLink));
        }

        private IEnumerable<ProductElement> GetActiveViewPTLinkProducts()
        {
            var permissions = GetProductsPermissionFromSession();

            // View PT of NG2 must check IsSync.
            if (permissions.ContainsKey(ProductIdConstants.NG2))
            {
                permissions[ProductIdConstants.NG2] = ProductManager.IsShowSettings(ProductIdConstants.NG2);
            }

            return ProductSection.Instance.Products
               .Cast<ProductElement>()
               .Where(p => p.Active && permissions.ContainsKey(p.Id) && permissions[p.Id] && !string.IsNullOrEmpty(p.ViewPTLink));
        }

        private IEnumerable<ProductElement> GetActiveReportProducts(IDictionary<int, bool> permissions = null)
        {
            return GetActiveProductElements(permissions)
                .Where(p => !string.IsNullOrEmpty(p.ReportSPParam));
        }

        private IEnumerable<ProductElement> GetActiveReportSportIdProducts(IDictionary<int, bool> permissions)
        {
            return GetActiveProductElements(permissions)
                .Where(p => !string.IsNullOrEmpty(p.ReportSPParam) && p.SportId != -1);
        }

        private IEnumerable<ProductElement> GetActiveProductElements(IDictionary<int, bool> permissions = null)
        {
            if (permissions != null)
            {
                return GetActiveProductElementsOfConsolidationReport(permissions);
            }

            permissions = GetProductsPermissionFromSession();

            return ProductSection.Instance.Products
                .Cast<ProductElement>()
                .Where(p => p.Active && permissions.ContainsKey(p.Id) && permissions[p.Id]);
        }

        private IEnumerable<ProductElement> GetActiveProductElementsOfConsolidationReport(IDictionary<int, bool> permissions)
        {
            IList<ProductElement> activeProductElements = new List<ProductElement>(16);

            var productElements = ProductSection.Instance.Products
                    .Cast<ProductElement>()
                    .Where(p => permissions.ContainsKey(p.Id) && permissions[p.Id]);

            foreach (var product in productElements)
            {
                if (product.Active)
                {
                    activeProductElements.Add(product);
                }
                else if (product.Id == ProductIdConstants.NG2 && permissions.ContainsKey(ProductIdConstants.NG2) && permissions[ProductIdConstants.NG2])
                {
                    activeProductElements.Add(product);
                }
            }

            return activeProductElements;
        }

        #endregion
    }
}