using System;
using System.Collections.Generic;
using AtraccionCola.Modelos;

namespace AtraccionCola.Servicios
{
    // Clase para hacer la fila o cola de espera de la gente.
    // Usamos una lista interna pero la manejamos como cola FIFO donde el primero que entra es el primero que sale
    public class ColaEspera
    {
        // Lista para ir guardando los visitantes en orden
        private List<Visitante> listaVisitantes;

        // Constructor para inicializar la lista
        public ColaEspera()
        {
            listaVisitantes = new List<Visitante>();
        }

        // Metodo para meter a alguien al final de la fila - Encolar
        public void Encolar(Visitante visitante)
        {
            listaVisitantes.Add(visitante); // lo agrega al final de la lista
        }

        // Metodo para sacar al primero de la fila - Desencolar
        public Visitante Desencolar()
        {
            // Si no hay nadie, no hace nada y devuelve null
            if (EstaVacia())
            {
                return null;
            }

            // Agarramos al primero de todos, en la posicion 0
            Visitante primerVisitante = listaVisitantes[0];
            
            // Lo borramos de la lista para que ya no este en la fila
            listaVisitantes.RemoveAt(0);
            
            // Devolvemos al visitante que sacamos
            return primerVisitante;
        }

        // Metodo para saber si la fila esta vacia
        public bool EstaVacia()
        {
            return listaVisitantes.Count == 0;
        }

        // Metodo para saber cuanta gente hay en la fila ahora mismo
        public int ObtenerCantidad()
        {
            return listaVisitantes.Count;
        }

        // Metodo para pasar la lista a otros lados y poder imprimirla
        public List<Visitante> ObtenerLista()
        {
            // Creamos una nueva lista igual para no dañar la original
            return new List<Visitante>(listaVisitantes);
        }

        // Metodo para buscar una cedula y ver en que posicion de la fila esta
        public int BuscarPosicionPorCedula(string cedula)
        {
            for (int i = 0; i < listaVisitantes.Count; i++)
            {
                if (listaVisitantes[i].Cedula == cedula)
                {
                    return i + 1; // Le sumamos 1 porque para la gente empieza en 1 y no en 0
                }
            }
            return -1; // Devuelve -1 si no encontro nada
        }

        // Metodo para buscar un nombre y ver su posicion en la fila
        public int BuscarPosicionPorNombre(string nombre)
        {
            for (int i = 0; i < listaVisitantes.Count; i++)
            {
                // ToLower es para que no importe si escriben con mayuscula o minuscula
                if (listaVisitantes[i].Nombre.ToLower().Contains(nombre.ToLower()))
                {
                    return i + 1;
                }
            }
            return -1;
        }
    }
}
