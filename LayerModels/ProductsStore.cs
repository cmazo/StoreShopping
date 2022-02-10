using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayerModels
{
    public class ProductsStore
    {
        /// <summary>
        /// Codigo del Producto
        /// </summary>
        public string CodeProduct { get; set; }
        /// <summary>
        /// Nombre del producto
        /// </summary>
        public string NameProduct { get; set; }

        /// <summary>
        /// Descripcion del Producto
        /// </summary>
        public string DescriptionProduct { get; set; }

        /// <summary>
        /// Ruta Imagen Producto
        /// </summary>
        public string PathImageProduct { get; set; }

        /// <summary>
        /// Cantidad a Comprar
        /// </summary>
        public int AmountProduct { get; set; }

        /// <summary>
        /// Valor Producto
        /// </summary>
        public int PriceProduct { get; set; }

    }
}
