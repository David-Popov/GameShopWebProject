using CarShopWebProject.Data;
using CarShopWebProject.Services;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarShopWebProject.Filters
{
    public class PlatformsFilter : ResultFilterAttribute
    {
        private readonly IProductService service;
        private readonly GameShopDbContext db;
        public PlatformsFilter()
        {

        }
        
        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            
        }

        //public override void OnResultExecuting(ResultExecutingContext context)
        //{
        //    context.HttpContext.Response.Headers.Add("Platforms", service.GetProductPlatforms())

        //    base.OnResultExecuting(context);
        //}
    }
}
