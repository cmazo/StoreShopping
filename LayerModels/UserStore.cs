using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayerModels
{
    public class UserStore
    {
        /// <summary>
        /// Codigo de Usuario
        /// </summary>
        public string UsuStore { get; set; }

        /// <summary>
        /// Password de usuario
        /// </summary>
        public string PwdStore { get; set; }

        /// <summary>
        /// Typo de Usuario  A-Administrador : C:Comprador
        /// </summary>
        public enTypeUser TypeUsStore { get; set; }

        /// <summary>
        /// Logueado
        /// </summary>
        public bool Logged { get; set; }
    }

    /// <summary>
    ///  A-Administrador
    ///  C:Comprador
    /// </summary>
    public enum enTypeUser
    {
        Administrator,
        Buyer
    }
}
