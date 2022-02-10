using LayerModels;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayerData
{
    public class SqlDataProducts
    {
        #region CADENA DE CONEXION A BASE DE DATOS.
        //SIEMPRE DEBE SER ESTATICA ESTA VARIABLE PARA KE NO SE PIERDA SU VALOR !!!!!
        //se debe modificar el "Starup.cs" archivo de la API.
        //y poner la cadena de conexion en "appsettings.json"

        /// <summary>
        /// Cadena de conexion a la base de datos.
        /// </summary>
        public static string StringDatabaseConnection { get; set; }

        public SqlDataProducts()
        {
        }


        public SqlDataProducts(string strConnection)
        {
            StringDatabaseConnection = strConnection;
        }
        #endregion

        /// <summary>
        /// Listado de todos los productos a vender.
        /// </summary>
        /// <returns></returns>
        public List<ProductsStore> ListProductsAllSales()
        {
            List<ProductsStore> lstProducts = new List<ProductsStore>();

            try
            {
                using (OracleConnection dbConn = new OracleConnection(StringDatabaseConnection))
                {
                    using (OracleCommand cmd = dbConn.CreateCommand())
                    {
                        cmd.Connection = dbConn;
                        dbConn.Open();

                        OracleDataAdapter da = new OracleDataAdapter("SELECT CodeStore, NameStore, DescriptionStore, PathStore, AmountStore, PriceStore  FROM PRODUCTSTORESHOPPING", dbConn);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        if (dt != null && dt.Rows != null && dt.Rows.Count > 0)
                        {
                            for (int fil = 0; fil < dt.Rows.Count; fil++)
                            {
                                lstProducts.Add(new ProductsStore { CodeProduct = dt.Rows[fil]["CodeStore"].ToString(), NameProduct = dt.Rows[fil]["NameStore"].ToString(), 
                                DescriptionProduct = dt.Rows[fil]["DescriptionStore"].ToString(), PathImageProduct = dt.Rows[fil]["PathStore"].ToString(),
                                AmountProduct = 0,//Convert.ToInt32(dt.Rows[fil]["AmountStore"]), 
                                PriceProduct = Convert.ToInt32(dt.Rows[fil]["PriceStore"])
                                });
                            }
                        }
                        dbConn.Close();
                    }
                }
            }
            catch (Exception er)
            {
                LayerLogs.FileLogger.Log(er.Message);
                throw er;
            }

            return lstProducts;
        }

        /// <summary>
        /// Inserta el producto en la BD.
        /// </summary>
        /// <param name="CodeProduct"></param>
        /// <param name="NameProduct"></param>
        /// <param name="DescriptionProduct"></param>
        /// <param name="PriceProduct"></param>
        /// <param name="PathImageProduct"></param>
        public void InsProductStore(string CodeProduct, string NameProduct, string DescriptionProduct, string PriceProduct, string PathImageProduct)
        {
            bool booResult = false;

            try
            {
                using (OracleConnection dbConn = new OracleConnection(StringDatabaseConnection))
                {
                    dbConn.Open();

                    using (OracleCommand cmd = dbConn.CreateCommand())
                    {
                        string query = $"INSERT INTO PRODUCTSTORESHOPPING (CodeStore, NameStore, DescriptionStore, PathStore, AmountStore, PriceStore) " +
                                 $"values ('{CodeProduct}','{NameProduct}','{DescriptionProduct}','image/{PathImageProduct}',0,{PriceProduct})";
                        
                        cmd.CommandText = query;
                        int result = cmd.ExecuteNonQuery();
                    }

                    dbConn.Close();
                }
            }
            catch (Exception er)
            {
                LayerLogs.FileLogger.Log(er.Message);
                throw er;
            }
        }

        /// <summary>
        /// Actualiza la cantidad vendida de un producto por el codigo de producto
        /// </summary>
        /// <param name="CodeProduct"></param>
        /// <param name="AmountProduct"></param>
        public void UpdSaleProductStore(string CodeProduct, int AmountProduct)
        {
            bool booResult = false;

            try
            {
                using (OracleConnection dbConn = new OracleConnection(StringDatabaseConnection))
                {
                    dbConn.Open();

                    using (OracleCommand cmd = dbConn.CreateCommand())
                    {
                        OracleTransaction transaction = dbConn.BeginTransaction(IsolationLevel.ReadCommitted);
                        cmd.Transaction = transaction;

                        string query = $"UPDATE PRODUCTSTORESHOPPING SET AMOUNTSTORE = AMOUNTSTORE + {AmountProduct} WHERE CODESTORE='{CodeProduct}' ";
                        cmd.CommandText = query;
                        int result = cmd.ExecuteNonQuery();

                        transaction.Commit();
                    }

                    
                    dbConn.Close();
                }
            }
            catch (Exception er)
            {
                LayerLogs.FileLogger.Log(er.Message);
                throw er;
            }
        }

        /// <summary>
        /// Retorna el listado de los productos del mas vendido al menor.
        /// </summary>
        /// <returns></returns>
        public List<ProductsStore> TopBestSellingOrderProducts()
        {
            List<ProductsStore> lstProducts = new List<ProductsStore>();

            try
            {
                using (OracleConnection dbConn = new OracleConnection(StringDatabaseConnection))
                {
                    using (OracleCommand cmd = dbConn.CreateCommand())
                    {
                        cmd.Connection = dbConn;
                        dbConn.Open();

                        OracleDataAdapter da = new OracleDataAdapter("SELECT CodeStore, NameStore, AmountStore FROM PRODUCTSTORESHOPPING ORDER BY AMOUNTSTORE DESC, NAMESTORE", dbConn);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        if (dt != null && dt.Rows != null && dt.Rows.Count > 0)
                        {
                            for (int fil = 0; fil < dt.Rows.Count; fil++)
                            {
                                lstProducts.Add(new ProductsStore
                                {
                                    CodeProduct = dt.Rows[fil]["CodeStore"].ToString(),
                                    NameProduct = dt.Rows[fil]["NameStore"].ToString(),
                                    AmountProduct = Convert.ToInt32(dt.Rows[fil]["AmountStore"]), 
                                });
                            }
                        }
                        dbConn.Close();
                    }
                }
            }
            catch (Exception er)
            {
                LayerLogs.FileLogger.Log(er.Message);
                throw er;
            }

            return lstProducts;
        }
    }
}
