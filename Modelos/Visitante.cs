using System;

namespace AtraccionCola.Modelos
{
    // Clase para guardar los datos de cada persona que va a subir a la montaña rusa
    public class Visitante
    {
        // Variables para guardar los datos personales de la gente
        private string nombre;
        private string cedula;
        private int edad;
        private int numeroTicket;
        private DateTime horaRegistro;

        // Constructor que se ejecuta cuando creamos un nuevo visitante
        public Visitante(string nombre, string cedula, int edad, int numeroTicket)
        {
            this.nombre = nombre;
            this.cedula = cedula;
            this.edad = edad;
            this.numeroTicket = numeroTicket;
            this.horaRegistro = DateTime.Now; // guarda la fecha y hora de ahora mismo
        }

        // Propiedad para poder ver y cambiar el nombre desde otros lados
        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        // Propiedad para la cedula
        public string Cedula
        {
            get { return cedula; }
            set { cedula = value; }
        }

        // Propiedad para la edad
        public int Edad
        {
            get { return edad; }
            set { edad = value; }
        }

        // Propiedad para el numero de ticket
        public int NumeroTicket
        {
            get { return numeroTicket; }
            set { numeroTicket = value; }
        }

        // Propiedad que solo deja leer la hora de registro
        public DateTime HoraRegistro
        {
            get { return horaRegistro; }
        }

        // Metodo facil para imprimir los datos del visitante de forma bonita
        public string ObtenerInformacion()
        {
            return "Ticket #" + numeroTicket + " - " + nombre + " - C.I: " + cedula + " - Edad: " + edad + " años - Registrado a las: " + horaRegistro.ToString("HH:mm:ss");
        }
    }
}
