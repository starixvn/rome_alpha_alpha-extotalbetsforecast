using System;
using System.Collections.Generic;
using Fanex.Razorbill.Module.Common.Models;
using Fanex.Razorbill.Module.Common.Services.ProductServices;
using Sunplus.Agent.Model.Entities;

namespace Fanex.Razorbill.Module.Common
{
    public static class ProductRenderUtility
    {
        #region Consolidation Report

        public static IList<KeyValuePair<string, bool>> GetProductSPParamsOfConsolidationReportBySport(int sportId)
        {
            var productService = new ProductService();

            var permission = productService.GetProductsByGroupHeadline(UserSession.Current.CustId, 1);

            return productService.GetProductSPParamsOfConsolidationReportBySport(permission, sportId);
        }

        public static IList<Tuple<int, int>> GetSportProductMapping()
        {
            var productService = new ProductService();

            var permission = productService.GetProductsByGroupHeadline(UserSession.Current.CustId, 1);

            return productService.GetSportProductMapping(permission);
        }

        public static IDictionary<int, bool> GetProductsByGroupHeadline(int custId, int showAll = -1)
        {
            var productService = new ProductService();

            return productService.GetProductsByGroupHeadline(custId, showAll);
        }

        #endregion

        public static IEnumerable<ProductFilter> GetProductsFilter(IDictionary<int, bool> permissionDictionary = null)
        {
            var productService = new ProductService();

            return productService.GetProductsFilter(permissionDictionary);
        }

        public static IEnumerable<ProductFilter> GetProductFiltersForMatchWinLossDetail()
        {
            var productService = new ProductService();

            return productService.GetProductFiltersForMatchWinLossDetail();
        }

        public static string GetViewPTProductSelector(int selectedProductId)
        {
            var productService = new ProductService();

            return productService.GetViewPTProductSelector(selectedProductId);
        }

        public static string GetViewCommProductSelector(int selectedProductId)
        {
            var productService = new ProductService();

            return productService.GetViewCommProductSelector(selectedProductId);
        }
    }
}