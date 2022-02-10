using LayerData;
using LayerModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiServicesStoreShopping.Controllers
{
    public class ProductsSalesCartController : Controller
    {
        SqlDataProducts sqConfi = new SqlDataProducts();

        /// <summary>
        /// Obtiene el listado de todos los productos
        /// </summary>
        /// <returns></returns>
        [Route("api/[controller]/GetListProductsAll")]
        [HttpGet]
        public async Task<IActionResult> GetListProductsAll()
        {
            List<ProductsStore> result = sqConfi.ListProductsAllSales();
            return Ok(result);
        }

        /// <summary>
        /// Inserta los productos en la bd - ProductStoreShopping
        /// </summary>
        /// <param name="CodeProduct"></param>
        /// <param name="NameProduct"></param>
        /// <param name="DescriptionProduct"></param>
        /// <param name="PriceProduct"></param>
        /// <param name="PathImageProduct"></param>
        /// <returns></returns>
        [Route("api/[controller]/PostInsertProduct/{CodeProduct}/{NameProduct}/{DescriptionProduct}/{PriceProduct}/{PathImageProduct}")]
        [HttpGet]
        public async Task<IActionResult> PostInsertProduct(string CodeProduct, string NameProduct, string DescriptionProduct, string PriceProduct, string PathImageProduct)
        {
            sqConfi.InsProductStore(CodeProduct, NameProduct, DescriptionProduct, PriceProduct, PathImageProduct);
            return Ok(true);
  
        }

        /// <summary>
        /// Actualiza la cantidad vendida de un producto
        /// </summary>
        /// <param name="CodeProduct"></param>
        /// <param name="AmountProduct"></param>
        /// <returns></returns>
        [Route("api/[controller]/PutSaleProduct/{CodeProduct}/{AmountProduct}")]
        [HttpGet]
        public async Task<IActionResult> PutSaleProduct(string CodeProduct, int AmountProduct)
        {
            sqConfi.UpdSaleProductStore(CodeProduct, AmountProduct);
            return Ok(true);

        }

        /// <summary>
        /// Obtiene los productos de mayor a menor venta.
        /// </summary>
        /// <returns></returns>
        [Route("api/[controller]/GetTopBestSellingProducts")]
        [HttpGet]
        public async Task<IActionResult> GetTopBestSellingProducts()
        {
            List<ProductsStore> result = sqConfi.TopBestSellingOrderProducts();
            return Ok(result);
        }

    }
}
