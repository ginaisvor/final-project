using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace TechEvent.Service
{
    public class PageSlugConstraintService : IRouteConstraint
    {
        public bool Match(HttpContext httpContext, IRouter route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            var pages = new string[] { "edit", "details", "delete", "index", "create" };

            return !pages.Contains(values["pageslug"]);
        }
    }

    public class EditionConstraintService : IRouteConstraint
    {/*
        private readonly IEditionService editionService;

        public EditionConstraintService(IEditionService editionService)
        {
            this.editionService = editionService;
        }*/
        public bool Match(HttpContext httpContext, IRouter route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            var years = new string[] { "2018", "2019", "2020" };

            return years.Contains(values["year"]);
        }
    }
}