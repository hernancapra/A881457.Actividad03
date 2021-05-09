using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A881457.Actividad03
{
    class Cuenta
    {
        public int Codigo { get; set; }
        public string Nombre { get; set; }
        public string Tipo { get; set; }
        public Cuenta()
        {

        }
        public Cuenta(string linea)
        {
            var datos = linea.Split('|');

            Codigo = int.Parse(datos[0]);
            Nombre = datos[1];
            Tipo = datos[2];
        }

        public static Cuenta IngresarNueva()
        {
            var cuenta = new Cuenta();

            Console.WriteLine();
            Console.WriteLine("Se desea ingresar una nueva cuenta al Plan de Cuentas....");

            cuenta.Codigo = IngresarCodigoCuenta(obligatorio: true);
            cuenta.Nombre = IngresarNombreCuenta("Ingrese el nombre de la cuenta.");
            Console.WriteLine();
            cuenta.Tipo = IngresarTipoCuenta("Ingrese el tipo de cuenta (Activo, Pasivo o PatrimonioNeto).");

            return cuenta;
        }

        public object ObtenerLineaDatos()
        {
            return $"{Codigo}|{Nombre}|{Tipo}";
        }

        public bool CoincideCon(Cuenta modelo)
        {
            if ((modelo.Codigo != 0) && (Codigo != modelo.Codigo))
            {
                return false;
            }

            if ((!string.IsNullOrWhiteSpace(modelo.Nombre)) && (Nombre != modelo.Nombre))
            {
                return false;
            }
            if ((!string.IsNullOrWhiteSpace(modelo.Tipo)) && (Tipo != modelo.Tipo))
            {
                return false;
            }

            return true;
        }

        public static Cuenta ModelodeBusqueda()
        {
            var modelo = new Cuenta();

            modelo.Codigo = IngresarCodigoCuenta(obligatorio: false);
            modelo.Nombre = IngresarNombreCuenta("Ingrese el nombre de la cuenta que desea buscar: ", obligatorio: false);
            modelo.Tipo = IngresarTipoCuenta("Ingrese el tipo de la cuenta que desea buscar: ", obligatorio: false);

            return modelo;
        }

        private static int IngresarCodigoCuenta(bool obligatorio = true)
        {
            var titulo = "Ingrese el código de la cuenta (mayor a 11 y menor a 35)";

            if (!obligatorio)
            {
                titulo += " o presione [ENTER] para continuar.";
            }

            Console.WriteLine(titulo);
            do
            {
                String codigoIngreso = Console.ReadLine();

                if (!obligatorio && string.IsNullOrWhiteSpace(codigoIngreso))
                {
                    return 0;
                }

                if (!int.TryParse(codigoIngreso, out int codigo))
                {
                    Console.WriteLine("No ingresó un código válido.");
                    continue;
                }

                if (codigo < 11 || codigo > 34)
                {
                    Console.WriteLine("No ingresó un código válido.");
                    continue;
                }

                if (PlanDeCuentas.Existe(codigo))
                {
                    Console.WriteLine("El código ingresado ya existe en el Plan de Cuentas.");
                    continue;
                }
                return codigo;

            } while (true);
        }

        private static string IngresarNombreCuenta(string titulo, bool obligatorio = true)
        {
            string nombreIngreso;

            do
            {
                Console.WriteLine();
                Console.WriteLine(titulo);
                nombreIngreso = Console.ReadLine();

                if (!obligatorio && string.IsNullOrWhiteSpace(nombreIngreso))
                {
                    return null;
                }

                if (obligatorio && string.IsNullOrWhiteSpace(nombreIngreso))
                {
                    Console.WriteLine("No ingresó un nombre válido. Por favor, inténtelo nuevamente.");
                    continue;
                }

                if (nombreIngreso.Any(c => Char.IsDigit(c)))
                {
                    Console.WriteLine("El nombre de la cuenta no puede contener números. Por favor, inténtelo nuevamente.");
                    continue;
                }

                break;

            } while (true);

            return nombreIngreso;
        }

        private static string IngresarTipoCuenta(string titulo, bool obligatorio = true)
        {
            string tipoIngreso;

            do
            {
                Console.WriteLine(titulo);
                tipoIngreso = Console.ReadLine();

                if (!obligatorio && string.IsNullOrWhiteSpace(tipoIngreso))
                {
                    return null;
                }

                if (obligatorio && string.IsNullOrWhiteSpace(tipoIngreso))
                {
                    Console.WriteLine("No ingresó un tipo válido.");
                    continue;
                }

                if ((tipoIngreso.ToUpper() == "ACTIVO") || (tipoIngreso.ToUpper() == "PASIVO") || (tipoIngreso.ToUpper() == "PATRIMONIONETO"))
                {
                    return tipoIngreso;
                }
                else
                {
                    Console.WriteLine("El tipo de cuenta debe ser alguno de los siguientes: Activo, Pasivo o PatrimonioNeto.");
                    continue;
                }
            } while (true);
        }

        public void MostrarDatos()
        {
            Console.WriteLine();
            Console.WriteLine($"Código de cuenta: {Codigo}");
            Console.WriteLine($"Nombre de cuenta: {Nombre}");
            Console.WriteLine($"Tipo de cuenta: {Tipo}");
            Console.WriteLine();
        }

        public void Modificar()
        {
            Console.WriteLine($"Nombre de cuenta: {Nombre}. Presione M para modificar o cualquier otra tecla para continuar.");
            var key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.M)
            {
                Nombre = Ingreso("Ingrese el nuevo nombre de la cuenta: ");
            }

            Console.WriteLine($"Tipo de cuenta: {Tipo}. Presione M para modificar o cualquier otra tecla para continuar.");
            key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.M)
            {
                do
                {
                    var tipoIngreso = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(tipoIngreso))
                    {
                        Console.WriteLine("No ingresó un tipo válido.");
                        break;
                    }

                    if (tipoIngreso.Any(Char.IsDigit))
                    {
                        Console.WriteLine("No ingresó un tipo válido");
                    }

                    if ((tipoIngreso.ToUpper() == "ACTIVO") || (tipoIngreso.ToUpper() == "PASIVO") || (tipoIngreso.ToUpper() == "PATRIMONIONETO"))
                    {
                        Tipo = tipoIngreso;
                    }
                    else
                    {
                        Console.WriteLine("El tipo de cuenta debe ser alguno de los siguientes: Activo, Pasivo o PatrimonioNeto.");
                        break;
                    }
                } while (true);
            }
            PlanDeCuentas.GrabarCuenta();
        }

        public static string Ingreso(string titulo)
        {
            string ingreso;

            Console.WriteLine(titulo);

            do
            {
                ingreso = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(ingreso))
                {
                    Console.WriteLine("No ingresó una opción válida.");
                    break;
                }

                if (ingreso.Any(c => Char.IsDigit(c)))
                {
                    Console.WriteLine("El nombre de la cuenta no puede tener números.");
                    break;
                }

            } while (true);

            return ingreso;
        }
    }
}
