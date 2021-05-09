using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A881457.Actividad03
{
    class PlanDeCuentas
    {
        private static readonly Dictionary<int, Cuenta> DicCuentas;
        const string nombreArchivo = "Plan de Cuentas.txt";

        static PlanDeCuentas()
        {
            DicCuentas = new Dictionary<int, Cuenta>();

            FileInfo FI = new FileInfo(nombreArchivo);

            if (FI.Exists)
            {
                using (var SR = new StreamReader(nombreArchivo))
                {
                    while (!SR.EndOfStream)
                    {
                        string Linea = SR.ReadLine();
                        var cuenta = new Cuenta(Linea);
                        DicCuentas.Add(cuenta.Codigo, cuenta);
                    }
                }
            }
        }

        public static void Agregar(Cuenta cuenta)
        {
            DicCuentas.Add(cuenta.Codigo, cuenta);
            GrabarCuenta();
        }

        public static Cuenta SeleccionarCuenta()
        {
            var modelo = Cuenta.ModelodeBusqueda();

            foreach (var cuenta in DicCuentas.Values)
            {
                if (cuenta.CoincideCon(modelo))
                {
                    return cuenta;
                }
            }

            Console.WriteLine("No se encontró la cuenta seleccionada.");
            return null;
        }

        public static void Baja(Cuenta cuenta)
        {
            DicCuentas.Remove(cuenta.Codigo);
            GrabarCuenta();
        }

        public static bool Existe(int codigo)
        {
            return DicCuentas.ContainsKey(codigo);
        }

        public static void GrabarCuenta()
        {
            using (var writer = new StreamWriter(nombreArchivo, append: false))
            {
                foreach (var cuenta in DicCuentas.Values)
                {
                    var linea = cuenta.ObtenerLineaDatos();
                    writer.WriteLine(linea);
                }
            }
        }
    }
}
