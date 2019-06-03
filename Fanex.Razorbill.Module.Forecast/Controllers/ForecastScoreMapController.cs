namespace Fanex.Razorbill.Module.Forecast.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Alpha.Web.Helpers;
    using Fanex.Razorbill.Module.Core.Web.Controllers;
    using Fanex.Razorbill.Module.Forecast.Criteria;
    using Fanex.Razorbill.Module.Forecast.Models;
    using Fanex.Razorbill.Module.Forecast.Services;
    using global::Resources;
    using Sunplus.Agent.Model.Entities;

    /// <summary>
    /// Forecast Score Map controller.
    /// </summary>
    public class ForecastScoreMapController : BaseController
    {
        private readonly IForecastService _forecastService;

        public ForecastScoreMapController()
            : this(null)
        {
        }

        public ForecastScoreMapController(IForecastService forecastService)
        {
            _forecastService = forecastService ?? new ForecastService();
        }

        /// <summary>
        /// Indexes the specified league id.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>
        /// The action result.
        /// </returns>
        public ActionResult Index(GetForecastScoreMapCriteria criteria)
        {
            IList<ForecastScoreMap> scoreMaps = null;
            var bettypes = _forecastService.GetScoreMapBetTypes();
            // Link from BetList.
            if (!string.IsNullOrEmpty(criteria.MatchIds))
            {
                ViewBag.IsRefresh = criteria.IsRefresh;
                criteria.Bettypes_Live = GetBettypeIds(criteria.Bettypes_Live, bettypes[0]);
                criteria.Bettypes_Dead = GetBettypeIds(criteria.Bettypes_Dead, bettypes[1]);

                scoreMaps = _forecastService.GetScoreMapResults(criteria);
            }
            else
            {
                RenderLeagues();
            }

            ViewBag.Bettypes = bettypes;
            return View("~/Areas/Forecast/Views/ScoreMap/Index.cshtml", scoreMaps);
        }

        public ActionResult RefreshLeagues()
        {
            var leagues = _forecastService.GetScoreMapRunningLeagues(new GetScoreMapRunningLeagueCriteria());
            return Json(leagues, JsonRequestBehavior.AllowGet);
        }

        public ActionResult MatchesByLeague(GetScoreMapMatchByLeagueCriteria criteria)
        {
            var matches = _forecastService.GetScoreMapMatches(criteria);
            return Json(matches, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Results the specified criteria.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The score map result.</returns>
        public ActionResult Results(GetForecastScoreMapCriteria criteria)
        {
            ViewBag.IsRefresh = criteria.IsRefresh;
            var bettypes = _forecastService.GetScoreMapBetTypes();
            criteria.Bettypes_Live = GetBettypeIds(criteria.Bettypes_Live, bettypes[0]);
            criteria.Bettypes_Dead = GetBettypeIds(criteria.Bettypes_Dead, bettypes[1]);

            var scoreMaps = _forecastService.GetScoreMapResults(criteria);
            return PartialView("~/Areas/Forecast/Views/ScoreMap/Results.cshtml", scoreMaps);
        }

        public ActionResult BetTypesInfo()
        {
            var bettypesInfo = _forecastService.GetScoreMapBetTypeInfo();
            return View("~/Areas/Forecast/Views/BetTypesInfo/Index.cshtml", bettypesInfo);
        }

        private void RenderLeagues()
        {
            var leagues = _forecastService.GetScoreMapRunningLeagues(new GetScoreMapRunningLeagueCriteria());
            ViewBag.Leagues = leagues.Select(s => new SelectListItem { Value = s.Id.ToString(), Text = s.Name }).ToList();
        }

        /// <summary>
        /// Get the bet type id.
        /// </summary>
        /// <param name="id">The id of live/dead.</param>
        /// <param name="bettypes">The bet type list.</param>
        /// <returns>The string of bet type id.</returns>
        private static string GetBettypeIds(string id, IList<ScoreMapBetType> bettypes)
        {
            if (id != "-1")
            {
                foreach (ScoreMapBetType item in bettypes)
                {
                    if (item.FollowedBetTypes.Contains(id))
                    {
                        return string.Join(",", item.FollowedBetTypes);
                    }
                }
            }

            return id;
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var allowViewTotalBets = UserSession.Current.SubPageAccess.ViewTotalBets || UserSession.Current.SubPageAccess.GetPermission(PageAccessType.ViewBetLists);
            if (!allowViewTotalBets)
            {
                if (Request.IsAjaxRequest())
                {
                    var response = filterContext.HttpContext.Response;
                    response.Clear();
                    response.Write("{ \"message\": \" " + Lang.notAccessSubAccount + " }");
                    response.TrySkipIisCustomErrors = true;
                    response.End();
                }
                else
                {
                    filterContext.Result = new RedirectResult(MicroSites.OldMainUrl("~/_Authorization/SignIn.aspx"));
                }
            }
        }
    }
}
