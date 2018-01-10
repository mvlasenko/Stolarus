using System.Threading.Tasks;
using Contentful.Core;
using Contentful.Core.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Stolarus.Models;

namespace Stolarus.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IContentfulClient _client;
        private bool appsettingsEmpty = false;

        public ProductsController(IContentfulClient client, IOptions<ContentfulOptions> contentfulOptions)
        {
            _client = client;

            if(string.IsNullOrEmpty(contentfulOptions.Value.SpaceId) || string.IsNullOrEmpty(contentfulOptions.Value.DeliveryApiKey))
            {
                appsettingsEmpty = true;
            }
        }

        public async Task<IActionResult> Index()
        {
            if (appsettingsEmpty)
            {
                return View("NoAppSettings");
            }

            var products = await _client.GetEntries<Product>();

            return View(products);
        }
    }
}