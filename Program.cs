using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A881457.Actividad03
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
                Console.WriteLine("--------------");
                Console.WriteLine("1. Alta cuenta");
                Console.WriteLine("2. Baja cuenta");
                Console.WriteLine("3. Modificación cuenta");
                Console.WriteLine("9. Salir");
                Console.WriteLine();

                Console.WriteLine("Ingrese una opción del menú y presione [ENTER].");

                var ingreso = Console.ReadLine();
                switch (ingreso)
                {
                    case "1":
                        AltaCuenta(); //Se da de alta una cuenta con código, nombre y tipo.
                        break;

                    case "2":
                        BajaCuenta(); //Se busca por nombre y tipo de cuenta. NO por código.
                        break;

                    case "3":
                        ModificarCuenta(); //Se busca por nombre y tipo de cuenta. NO por código.
                        break;

                    case "9":
                        salir = true; //Salir de la consola.
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
                Console.WriteLine("La cuenta seleccionada se dió de baja correctamente.");
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

        private static void BuscarCuenta()
        {
            Cuenta cuenta = PlanDeCuentas.SeleccionarCuenta();
            if (cuenta != null)
            {
                cuenta.MostrarDatos();
            }            
        }
    }
}
