using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using hotel_management.Models;

namespace hotel_management.DAO
{
    public abstract class StandardDAO<T> where T : StandardViewModel
    {

        // Atributos padrão ainda a serem adicionados

        public virtual void Insert(T model)
        {
            // Implementação do método de inserção
        }

        public virtual void Update(T model)
        {
            // Implementação do método de atualização
        }

        public virtual void Delete(int id)
        {
            // Implementação do método de exclusão
        }

        public virtual T? Get(int id)
        {
            // Implementação do método de consulta por id
            return null;
        }

        public virtual List<T> GetAll()
        {
            // Implementação do método para obter a lista de registros
            return new List<T>();
        }
        
    }
}