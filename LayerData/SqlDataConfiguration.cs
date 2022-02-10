using LayerModels;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;

namespace LayerData
{
    public class SqlDataConfiguration
    {
        #region CADENA DE CONEXION A BASE DE DATOS.
        //SIEMPRE DEBE SER ESTATICA ESTA VARIABLE PARA KE NO SE PIERDA SU VALOR !!!!!
        //se debe modificar el "Starup.cs" archivo de la API.
        //y poner la cadena de conexion en "appsettings.json"

        /// <summary>
        /// Cadena de conexion de la Base de datos.
        /// </summary>
        public static string StringDatabaseConnection { get; set; }

        public SqlDataConfiguration()
        {
        }


        public SqlDataConfiguration(string strConnection)
        {
            StringDatabaseConnection = strConnection;
        }
        #endregion

        /// <summary>
        /// Valida usuario y contraseña en la bd.
        /// </summary>
        /// <param name="strUser"></param>
        /// <param name="strPassword"></param>
        /// <returns></returns>
        public bool ValidateLoginAccessData(string strUser, string strPassword)
        {
            bool booResult = false;

            try
            {
                using (OracleConnection dbConn = new OracleConnection(StringDatabaseConnection))
                {
                    using (OracleCommand cmd = dbConn.CreateCommand())
                    {
                        cmd.Connection = dbConn;
                        dbConn.Open();
                        
                        OracleDataAdapter da = new OracleDataAdapter($"SELECT USERSTORE, PASSWORDSTORE FROM USERSTORESHOPPING WHERE USERSTORE='{strUser}'", dbConn);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        if (dt != null && dt.Rows != null && dt.Rows.Count > 0 && dt.Rows[0]["PASSWORDSTORE"].ToString() == strPassword)
                        {
                            booResult = true;
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

            return booResult;
        }

        /// <summary>
        /// Indica que esta loggeado.
        /// </summary>
        /// <param name="strUser"></param>
        public void UpdLoggedStore(string strUser)
        {
            bool booResult = false;

            try
            {
                using (OracleConnection dbConn = new OracleConnection(StringDatabaseConnection))
                {
                    dbConn.Open();

                    using (OracleCommand cmd = dbConn.CreateCommand())
                    {
                        cmd.CommandText = $"UPDATE USERSTORESHOPPING SET LOGGEDSTORE=1 WHERE USERSTORE= '{strUser}'";
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
        /// Llena la entidad de usuario que se logueo.
        /// </summary>
        /// <param name="strUser"></param>
        /// <returns></returns>
        public UserStore GetFillUserLoging(string strUser)
        {
            UserStore usResult = new UserStore();

            try
            {
                using (OracleConnection dbConn = new OracleConnection(StringDatabaseConnection))
                {
                    using (OracleCommand cmd = dbConn.CreateCommand())
                    {
                        cmd.Connection = dbConn;
                        dbConn.Open();

                        OracleDataAdapter da = new OracleDataAdapter($"SELECT USERSTORE, PASSWORDSTORE, TYPEUSERSTORE, LOGGEDSTORE FROM USERSTORESHOPPING WHERE USERSTORE='{strUser}'", dbConn);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        if (dt != null && dt.Rows != null && dt.Rows.Count > 0)
                        {
                            usResult.UsuStore = strUser;
                            usResult.PwdStore = dt.Rows[0]["PASSWORDSTORE"].ToString();
                            usResult.TypeUsStore = dt.Rows[0]["TYPEUSERSTORE"].ToString().Equals("A") ? enTypeUser.Administrator : enTypeUser.Buyer;
                            usResult.Logged = dt.Rows[0]["LOGGEDSTORE"].ToString().Equals("1");
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

            return usResult;
        }
    }
}
