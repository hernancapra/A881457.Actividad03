using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace _888067.Actividad03
{
    class Program
    {
        static void Main(string[] args)
        {
            bool salir = false;

            do
            {
                Console.WriteLine();
                Console.WriteLine("MENU PRINCIPAL");
                Console.WriteLine();
                Console.WriteLine("1. Alta de cuenta");
                Console.WriteLine("2. Baja de cuenta");
                Console.WriteLine("3. Modificación de cuenta");
                Console.WriteLine("9. Salir");
                Console.WriteLine();
                Console.WriteLine("Elija una opción del menú y presione [ENTER] para continuar.");

                var ingreso = Console.ReadLine();
                switch (ingreso)
                {
                    case "1":
                        AltaCuenta();
                        break;

                    case "2":
                        BajaCuenta();
                        break;

                    case "3":
                        ModificarCuenta();
                        break;

                    case "9":
                        salir = true;
                        break;

                    default:
                        Console.WriteLine("No se ingresó ninguna opción del menú.");
                        break;
                }
            } while (!salir);
        }

        private static void AltaCuenta()
        {
            var cuenta = Cuenta.IngresarNueva();
            PlanDeCuentas.Agregar(cuenta);
            Console.WriteLine("Se dió de alta la cuenta ingresada.");
        }

        private static void BajaCuenta()
        {
            var cuenta = PlanDeCuentas.SeleccionarCuenta();

            if (cuenta == null)
            {
                return;
            }

            cuenta.MostrarDatos();

            Console.WriteLine("¿Desea confirmar la baja de la cuenta? [S/N]");
            var respuesta = Console.ReadKey(true);

            if (respuesta.Key == ConsoleKey.S)
            {
                PlanDeCuentas.Baja(cuenta);
                Console.WriteLine("Se dió de baja la cuenta seleccionada.");
            }
        }

        private static void ModificarCuenta()
        {
            var cuenta = PlanDeCuentas.SeleccionarCuenta();
            if (cuenta == null)
            {
                return;
            }
            cuenta.MostrarDatos();
            cuenta.Modificar();
        }
    }
}