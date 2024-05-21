using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using hotel_management.Models;
using System.Data.SqlClient;
using System.Data;

namespace hotel_management.DAO
{
    public abstract class StandardDAO<T> where T : StandardViewModel
    {

        protected abstract SqlParameter[] CreateParameters(T model);
        protected abstract T MountModel(DataRow row);

        protected string Table { get; set; } = "";

        public virtual void Insert(T model)
        {
            HelperDAO.ExecutaProc("spInsert_" + Table, CreateParameters(model));
        }

        public virtual void Update(T model)
        {
            HelperDAO.ExecutaProc("spUpdate_" + Table, CreateParameters(model));
        }

        public virtual void Delete(int id, string id_route)
        {

            //TODO - Improve this delete method

            var p = new SqlParameter[]{
                new SqlParameter(id_route, id),
            };
            
            HelperDAO.ExecutaProc("spDelete_" + Table, p);
        }

        public virtual T? Get(int id)
        {
            var p = new SqlParameter[]{
                new SqlParameter("id", id),
            };

            var table = HelperDAO.ExecutaProcSelect("spGet_" + Table, p);

            if (table.Rows.Count != 0)
                return MountModel(table.Rows[0]);
            else
                return null;
        }

        public virtual List<T> GetAll()
        {

            var table = HelperDAO.ExecutaProcSelect("spGetAll_" + Table, null);
            List<T> list = new List<T>();
            
            foreach (DataRow row in table.Rows)
                list.Add(MountModel(row));

            return list;

        }
        
    }
}