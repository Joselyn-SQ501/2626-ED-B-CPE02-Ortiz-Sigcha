using System;

namespace AtraccionCola.Modelos
{
    // Clase para representar un asiento fisico del juego
    public class Asiento
    {
        // Variables para guardar los datos del asiento
        private int numeroAsiento;
        private bool ocupado;
        private Visitante pasajero; // Aca guardamos el objeto del visitante que se sienta

        // Constructor para crear el asiento vacio
        public Asiento(int numeroAsiento)
        {
            this.numeroAsiento = numeroAsiento;
            this.ocupado = false; // empieza desocupado
            this.pasajero = null; // no hay nadie sentado todavia
        }

        // Propiedad para saber que numero de asiento es
        public int NumeroAsiento
        {
            get { return numeroAsiento; }
        }

        // Propiedad para saber si esta ocupado o no
        public bool Ocupado
        {
            get { return ocupado; }
        }

        // Propiedad para obtener el visitante sentado
        public Visitante Pasajero
        {
            get { return pasajero; }
        }

        // Metodo para sentar a una persona en este asiento
        public void AsignarPasajero(Visitante visitante)
        {
            this.pasajero = visitante;
            this.ocupado = true; // ahora si esta ocupado
        }

        // Metodo para vaciar el asiento cuando se bajen del juego
        public void LiberarAsiento()
        {
            this.pasajero = null;
            this.ocupado = false; // queda libre de nuevo
        }
    }
}
