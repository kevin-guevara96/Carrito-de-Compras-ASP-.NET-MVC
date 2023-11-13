using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatosDAL;
using CapaEntidadDL;
using CapaNegocioDL;

namespace CapaNegocioDL
{
    public class CN_Reporte
    {
        private CD_Reporte objCapaDato = new CD_Reporte();

        public DashBoard VerDashBoard()
        {
             return objCapaDato.VerDashBoard();
        }

        public List<Reporte> Ventas(string fechaincio, string fechafin, string idtransaccion)
        {
            return objCapaDato.Ventas(fechaincio, fechafin, idtransaccion);
        }

    }
}
