using System;
using System.Threading;
using AtraccionCola.Modelos;

namespace AtraccionCola.Servicios
{
    // Clase para controlar la atraccion del parque y sus asientos
    public class Atraccion
    {
        private string nombre;
        private const int CAPACIDAD = 30; // Numero fijo de 30 asientos
        private Asiento[] asientos;
        private int viajesRealizados;
        private bool enViaje;

        // Constructor para crear la atraccion
        public Atraccion(string nombre)
        {
            this.nombre = nombre;
            this.viajesRealizados = 0;
            this.enViaje = false;

            // Creamos los 30 asientos uno por uno
            asientos = new Asiento[CAPACIDAD];
            for (int i = 0; i < CAPACIDAD; i++)
            {
                asientos[i] = new Asiento(i + 1); // les ponemos numero de asiento de forma ordenada
            }
        }

        // Propiedades basicas de lectura
        public string Nombre
        {
            get { return nombre; }
        }

        public int Capacidad
        {
            get { return CAPACIDAD; }
        }

        public int ViajesRealizados
        {
            get { return viajesRealizados; }
        }

        public bool EnViaje
        {
            get { return enViaje; }
        }

        // Metodo para sentar a las primeras 30 personas de la fila en los asientos
        public bool Abordar(ColaEspera cola)
        {
            // Validacion importante: solo se puede iniciar si la cola tiene 30 personas o mas
            if (cola.ObtenerCantidad() < CAPACIDAD)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n[ERROR] No se puede iniciar el abordaje. Hay " + cola.ObtenerCantidad() + " personas en la cola.");
                Console.WriteLine("Se necesitan exactamente " + CAPACIDAD + " personas para llenar la atracción.");
                Console.ResetColor();
                return false;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n--- INICIANDO PROCESO DE ABORDAJE EN LA ATRACCIÓN '" + nombre + "' ---");
            Console.ResetColor();

            // Sacamos 30 personas de la fila y las sentamos en los asientos del 1 al 30
            for (int i = 0; i < CAPACIDAD; i++)
            {
                Visitante visitante = cola.Desencolar();
                asientos[i].AsignarPasajero(visitante);
                Console.WriteLine("Asiento #" + asientos[i].NumeroAsiento + ": " + visitante.Nombre + " ha abordado con éxito.");
            }

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n[ÉXITO] ¡Todos los 30 asientos han sido asignados! La atracción está lista para partir.");
            Console.ResetColor();
            return true;
        }

        // Metodo para simular el viaje en la montana rusa
        public void IniciarViaje()
        {
            // Verificamos si hay gente sentada en el primer asiento
            if (!asientos[0].Ocupado)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n[ADVERTENCIA] No hay pasajeros a bordo. Ejecute la opción de abordaje primero.");
                Console.ResetColor();
                return;
            }

            enViaje = true;
            try { Console.Clear(); } catch { }
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("==================================================================");
            Console.WriteLine("      ¡¡INICIA EL VIAJE EN LA ATRACCIÓN: " + nombre.ToUpper() + "!!");
            Console.WriteLine("==================================================================");
            Console.ResetColor();

            // Mensajes sencillos para simular la montaña rusa
            string[] marcosAnimacion = new string[] {
                "   [  🎢  ]  Subiendo la primera cuesta... lento... lento...",
                "   [  🎢  ]  ¡En la cima!... Preparándose para la caída libre...",
                "   [  🎢  ]  ¡¡¡¡¡ WUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUU !!!!! - Giro de 360 grados",
                "   [  🎢  ]  Curva peligrosa a la derecha... ¡sujétense fuerte!",
                "   [  🎢  ]  Entrando al túnel oscuro... ¡¡gritos por todas partes!!",
                "   [  🎢  ]  Frenando suavemente... ingresando a la estación final."
            };

            // Recorremos los mensajes haciendo una pausa de 1.2 segundos en cada uno
            for (int i = 0; i < marcosAnimacion.Length; i++)
            {
                Console.WriteLine(marcosAnimacion[i]);
                Thread.Sleep(1200); // Pausa de 1200 milisegundos
            }

            viajesRealizados++;
            enViaje = false;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n[FIN] El viaje ha concluido con éxito y seguridad.");
            Console.WriteLine("Todos los pasajeros desembarcan felices. Asientos liberados para la siguiente tanda.");
            Console.ResetColor();

            // Vaciamos los asientos al terminar el viaje
            LiberarAsientos();
        }

        // Metodo para desocupar todos los asientos
        public void LiberarAsientos()
        {
            for (int i = 0; i < CAPACIDAD; i++)
            {
                asientos[i].LiberarAsiento();
            }
        }

        // Metodo para imprimir el mapa de asientos en consola
        public void MostrarAsientos()
        {
            Console.WriteLine("\n==========================================");
            Console.WriteLine("      MAPA DE ASIENTOS - " + nombre.ToUpper());
            Console.WriteLine("==========================================");

            // Muestra los asientos en formato de filas y columnas, 5 filas de 6 columnas para los 30 asientos
            int index = 0;
            for (int fila = 0; fila < 5; fila++)
            {
                for (int col = 0; col < 6; col++)
                {
                    Asiento asiento = asientos[index];
                    string celda = asiento.Ocupado ? "[O-" + asiento.NumeroAsiento.ToString("00") + "]" : "[ -" + asiento.NumeroAsiento.ToString("00") + "]";
                    
                    if (asiento.Ocupado)
                    {
                        Console.ForegroundColor = ConsoleColor.Red; // Ocupado sale rojo
                        Console.Write(celda + "  ");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Green; // Libre sale verde
                        Console.Write(celda + "  ");
                    }
                    Console.ResetColor();
                    index++;
                }
                Console.WriteLine();
            }

            Console.WriteLine("\nLeyenda: [O-XX] Asiento Ocupado | [ -XX] Asiento Libre");
            Console.WriteLine("------------------------------------------");
            
            // Listamos los pasajeros sentados en pantalla
            bool hayPasajeros = false;
            foreach (var asiento in asientos)
            {
                if (asiento.Ocupado)
                {
                    if (!hayPasajeros)
                    {
                        Console.WriteLine("Pasajeros a bordo actualmente:");
                        hayPasajeros = true;
                    }
                    Console.WriteLine(" - Asiento #" + asiento.NumeroAsiento + ": " + asiento.Pasajero.Nombre);
                }
            }

            if (!hayPasajeros)
            {
                Console.WriteLine("La atracción se encuentra vacía en la estación.");
            }
            Console.WriteLine("==========================================");
        }
    }
}
