using System;
using System.Collections.Generic;
using AtraccionCola.Modelos;
using AtraccionCola.Servicios;

namespace AtraccionCola
{
    class Program
    {
        // Numero inicial para los tickets
        private static int contadorTickets = 1001;

        static void Main(string[] args)
        {
            // Cambiar titulo arriba de la consola
            try { Console.Title = "Simulación de Cola para Atracción - Parque de Diversiones"; } catch { }

            // Creamos los objetos de la montaña rusa y la cola de espera
            Atraccion montanaRusa = new Atraccion("Montaña Rusa Extrem-X");
            ColaEspera colaDeEspera = new ColaEspera();

            bool salir = false;

            // Bucle principal para que el menu se repita hasta presionar la opcion 9 de Salir
            while (!salir)
            {
                MostrarMenu();
                string opcionInput = Console.ReadLine(); // lee lo que escribe el usuario

                switch (opcionInput)
                {
                    case "1":
                        RegistrarNuevoVisitante(colaDeEspera);
                        break;
                    case "2":
                        MostrarReporteCola(colaDeEspera);
                        break;
                    case "3":
                        SimularLlenadoAutomatico(colaDeEspera);
                        break;
                    case "4":
                        montanaRusa.Abordar(colaDeEspera);
                        break;
                    case "5":
                        montanaRusa.MostrarAsientos();
                        break;
                    case "6":
                        montanaRusa.IniciarViaje();
                        break;
                    case "7":
                        BuscarVisitante(colaDeEspera);
                        break;
                    case "8":
                        MostrarEstadisticas(colaDeEspera, montanaRusa);
                        break;
                    case "9":
                        salir = true;
                        Console.WriteLine("\n¡Gracias por utilizar el sistema del Parque de Diversiones!");
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("\n[ERROR] Opción no válida. Por favor, intente de nuevo.");
                        Console.ResetColor();
                        break;
                }

                // Pausa antes de volver a dibujar el menu
                if (!salir)
                {
                    Console.WriteLine("\nPresione cualquier tecla para volver al menú...");
                    try { Console.ReadKey(); } catch { }
                }
            }
        }

        // Metodo para imprimir el menu de opciones sin parentesis
        static void MostrarMenu()
        {
            try { Console.Clear(); } catch { } // limpia la pantalla de consola
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("==================================================================");
            Console.WriteLine("          SISTEMA DE CONTROL DE FILAS - PARQUE DE DIVERSIONES     ");
            Console.WriteLine("==================================================================");
            Console.ResetColor();
            Console.WriteLine("1. Registrar visitante en la cola - Encolar");
            Console.WriteLine("2. Visualizar estado de la cola de espera - Reporte");
            Console.WriteLine("3. Simulación: Llenar la cola con 30 personas automáticamente");
            Console.WriteLine("4. Abordar atracción - Desencolar 30 personas a los asientos");
            Console.WriteLine("5. Mostrar mapa de asientos de la atracción - Reporte de abordaje");
            Console.WriteLine("6. Iniciar viaje de la atracción - Simulación del juego");
            Console.WriteLine("7. Consultar posición de visitante en la cola - Búsqueda");
            Console.WriteLine("8. Ver estadísticas generales de la jornada");
            Console.WriteLine("9. Salir del programa");
            Console.WriteLine("------------------------------------------------------------------");
            Console.Write("Seleccione una opción (1-9): ");
        }

        // Metodo para registrar una persona escribiendo los datos por teclado
        static void RegistrarNuevoVisitante(ColaEspera cola)
        {
            Console.WriteLine("\n--- REGISTRO MANUAL DE VISITANTE ---");
            
            Console.Write("Ingrese el nombre completo: ");
            string nombre = Console.ReadLine();
            if (string.IsNullOrEmpty(nombre))
            {
                Console.WriteLine("[ERROR] El nombre no puede estar vacío.");
                return;
            }

            Console.Write("Ingrese el número de cédula - C.I.: ");
            string cedula = Console.ReadLine();
            if (string.IsNullOrEmpty(cedula))
            {
                Console.WriteLine("[ERROR] La cédula no puede estar vacía.");
                return;
            }

            Console.Write("Ingrese la edad: ");
            int edad;
            // Valida que el texto ingresado sea un numero valido para la edad
            if (!int.TryParse(Console.ReadLine(), out edad) || edad <= 0 || edad > 120)
            {
                Console.WriteLine("[ERROR] Edad inválida. Debe ser un número positivo.");
                return;
            }

            // Creamos el visitante y lo metemos a la cola
            Visitante nuevo = new Visitante(nombre, cedula, edad, contadorTickets);
            cola.Encolar(nuevo);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n[ÉXITO] Visitante registrado correctamente.");
            Console.WriteLine("Se le asignó el Ticket #" + contadorTickets + ".");
            Console.ResetColor();

            contadorTickets++; // aumentamos el contador para la siguiente persona
        }

        // Metodo para imprimir la fila de espera en pantalla
        static void MostrarReporteCola(ColaEspera cola)
        {
            Console.WriteLine("\n--- REPORTE DE LA COLA DE ESPERA ---");
            if (cola.EstaVacia())
            {
                Console.WriteLine("La cola de espera se encuentra actualmente vacía.");
                return;
            }

            List<Visitante> lista = cola.ObtenerLista();
            Console.WriteLine("Total de personas esperando: " + lista.Count);
            Console.WriteLine("------------------------------------------------------------------");
            
            int posicion = 1;
            foreach (Visitante v in lista)
            {
                Console.WriteLine("Posición #" + posicion.ToString("00") + " | " + v.ObtenerInformacion());
                posicion++;
            }
            Console.WriteLine("------------------------------------------------------------------");
        }

        // Metodo para generar 30 personas aleatorias rapido y probar el abordaje sin escribir a mano
        static void SimularLlenadoAutomatico(ColaEspera cola)
        {
            Console.WriteLine("\n--- SIMULACIÓN DE LLENADO AUTOMÁTICO ---");
            
            string[] nombresFicticios = new string[] {
                "Juan Pérez", "María López", "Carlos Ruiz", "Ana Gómez", "Luis Andrade", 
                "Sofía Castro", "Pedro Cevallos", "Lucía Benítez", "José Delgado", "Elena Espinoza",
                "Jorge Freire", "Diana Gutiérrez", "Miguel Herrera", "Rosa Ibarra", "Andrés Jiménez",
                "Gabriela Lara", "Fernando Medina", "Patricia Noboa", "Raúl Ortiz", "Carmen Paredes",
                "David Quevedo", "Silvia Rojas", "Manuel Salazar", "Verónica Torres", "Hugo Uvidia",
                "Lorena Valdiviezo", "William Yánez", "Paulina Zambrano", "Francisco Acosta", "Beatriz Borja"
            };

            Random random = new Random();

            Console.WriteLine("Generando 30 visitantes automáticamente...");
            for (int i = 0; i < 30; i++)
            {
                string nombre = nombresFicticios[i];
                string cedula = "09" + random.Next(10000000, 99999999).ToString();
                int edad = random.Next(12, 60); // edad entre 12 y 59 años

                Visitante v = new Visitante(nombre, cedula, edad, contadorTickets);
                cola.Encolar(v);
                contadorTickets++;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n[ÉXITO] Se han agregado 30 personas de forma rápida a la cola.");
            Console.WriteLine("Tamaño actual de la cola de espera: " + cola.ObtenerCantidad() + " personas.");
            Console.ResetColor();
        }

        // Metodo para buscar a alguien por nombre o por cedula en la cola
        static void BuscarVisitante(ColaEspera cola)
        {
            Console.WriteLine("\n--- CONSULTAR POSICIÓN DE VISITANTE ---");
            if (cola.EstaVacia())
            {
                Console.WriteLine("La cola está vacía. No hay nadie para buscar.");
                return;
            }

            Console.WriteLine("1. Buscar por número de cédula - C.I.");
            Console.WriteLine("2. Buscar por nombre o fragmento");
            Console.Write("Seleccione opción de búsqueda: ");
            string criterio = Console.ReadLine();

            if (criterio == "1")
            {
                Console.Write("Ingrese el número de cédula a buscar: ");
                string cedulaBusqueda = Console.ReadLine();
                int pos = cola.BuscarPosicionPorCedula(cedulaBusqueda);

                if (pos != -1)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\n[ENCONTRADO] El visitante está en la posición #" + pos + " de la cola.");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n[ERROR] No se encontró ningún visitante con esa cédula en la cola.");
                    Console.ResetColor();
                }
            }
            else if (criterio == "2")
            {
                Console.Write("Ingrese el nombre a buscar: ");
                string nombreBusqueda = Console.ReadLine();
                int pos = cola.BuscarPosicionPorNombre(nombreBusqueda);

                if (pos != -1)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\n[ENCONTRADO] El visitante está en la posición #" + pos + " de la cola.");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n[ERROR] No se encontró ningún visitante con ese nombre en la cola.");
                    Console.ResetColor();
                }
            }
            else
            {
                Console.WriteLine("[ERROR] Opción no válida.");
            }
        }

        // Metodo para mostrar estadisticas del dia
        static void MostrarEstadisticas(ColaEspera cola, Atraccion atraccion)
        {
            Console.WriteLine("\n=======================================================");
            Console.WriteLine("              ESTADÍSTICAS DEL SISTEMA                 ");
            Console.WriteLine("=======================================================");
            Console.WriteLine("Atracción: " + atraccion.Nombre);
            Console.WriteLine("Capacidad requerida por tanda: " + atraccion.Capacidad + " asientos");
            Console.WriteLine("Viajes completados hasta el momento: " + atraccion.ViajesRealizados);
            Console.WriteLine("Total pasajeros atendidos: " + (atraccion.ViajesRealizados * atraccion.Capacidad));
            Console.WriteLine("Personas esperando actualmente en la cola: " + cola.ObtenerCantidad());
            
            // Calculamos la edad promedio si la cola no esta vacia
            if (!cola.EstaVacia())
            {
                List<Visitante> lista = cola.ObtenerLista();
                double sumaEdades = 0;
                foreach (Visitante v in lista)
                {
                    sumaEdades += v.Edad;
                }
                double promedio = sumaEdades / lista.Count;
                Console.WriteLine("Edad promedio de las personas en cola: " + promedio.ToString("F1") + " años");
            }
            else
            {
                Console.WriteLine("Edad promedio en cola: N/A (Fila vacía)");
            }
            Console.WriteLine("=======================================================");
        }
    }
}
