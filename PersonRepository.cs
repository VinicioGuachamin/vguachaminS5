using SQLite;
using vguachaminS5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace vguachaminS5
{
    public class PersonRepository
    {
        string dbPath;

        private SQLiteConnection conn;
        public string StatusMessage { get; set; }

        public void Init()
        {
            if (conn is not null)
                return;
            conn = new(dbPath);
            conn.CreateTable<Persona>();
        }

        public PersonRepository(string dbPath1)
        {
            dbPath = dbPath1;
        }

        public void AddNewPerson(string nombre)
        {
            int result = 0;
            try
            {
                Init();
                if (string.IsNullOrEmpty(nombre))
                    throw new Exception("Nombre Requerido");
                Persona persona = new Persona()
                {
                    Name = nombre,
                };
                result = conn.Insert(persona);
                StatusMessage = string.Format("Filas agregadas", result, nombre);
            }
            catch (Exception ex)
            {

                StatusMessage = string.Format("Error al insertar", nombre, ex.Message);
            }

        }

        public List<Persona> GetAllPerson()
        {
            try
            {
                Init();
                return conn.Table<Persona>().ToList();
            }
            catch (Exception ex)
            {

                StatusMessage = string.Format("Error al listar los datos", ex.Message);
            }
            return new List<Persona>();
        }

        public void DeletePerson(int personId)
        {
            try
            {
                Init();
                int result = conn.Delete<Persona>(personId);
                StatusMessage = string.Format("Filas eliminadas: {0}", result);
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Error al eliminar la persona con ID {0}: {1}", personId, ex.Message);
            }
        }

        public void UpdatePerson(Persona updatedPerson)
        {
            try
            {
                Init();
                if (updatedPerson == null)
                    throw new Exception("Persona actualizada no puede ser nula");

                int result = conn.Update(updatedPerson);
                StatusMessage = string.Format("Filas actualizadas: {0}", result);
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Error al actualizar la persona: {0}", ex.Message);
            }
        }
    }
}
