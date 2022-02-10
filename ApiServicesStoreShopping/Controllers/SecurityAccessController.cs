using LayerData;
using LayerModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ApiServicesStoreShopping.Controllers
{
    public class SecurityAccessController : Controller
    {

        SqlDataConfiguration sqConfi = new SqlDataConfiguration();

        /// <summary>
        /// Valida si el usuario y contraseña son validos.
        /// </summary>
        /// <param name="strUser"></param>
        /// <param name="strPassword"></param>
        /// <returns></returns>
        [Route("api/[controller]/GetValidateUserLoging/{strUser}/{strPassword}")]
        [HttpGet]
        public async Task<IActionResult> GetValidateUserLoging(string strUser, string strPassword)
        {
            bool booValidUser = sqConfi.ValidateLoginAccessData(strUser, strPassword);

            if (booValidUser)
            {
                UpdateLoggedStore(strUser);
                return Ok(true);
            }
            else
                return Ok(false);
        }

        /// <summary>
        /// Actualiza el loggeo del usuario.
        /// </summary>
        /// <param name="strUser"></param>
        private void UpdateLoggedStore(string strUser)
        {
            sqConfi.UpdLoggedStore(strUser);
        }

        /// <summary>
        /// Obtener los datos del usuario.s
        /// </summary>
        /// <param name="strUser"></param>
        /// <returns></returns>
        [Route("api/[controller]/FillUser/{strUser}")]
        [HttpGet]
        public async Task<IActionResult> GetFillUserLoging(string strUser)
        {
            UserStore tmpUserStore = sqConfi.GetFillUserLoging(strUser);
            return Ok(tmpUserStore);

        }
    }
}
