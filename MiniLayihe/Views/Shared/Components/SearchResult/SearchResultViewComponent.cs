using System;
using Microsoft.AspNetCore.Mvc;
using MiniLayihe.Models;

namespace MiniLayihe.Views.Shared.Components.SearchResult
{
	public class SearchResultViewComponent : ViewComponent
	{
        public IViewComponentResult Invoke(ShopSearchVM model)
        {
            return View(model);
        }
    }
}

