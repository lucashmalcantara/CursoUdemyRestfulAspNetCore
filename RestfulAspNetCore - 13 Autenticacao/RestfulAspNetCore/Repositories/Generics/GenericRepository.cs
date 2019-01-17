using Microsoft.EntityFrameworkCore;
using RestfulAspNetCore.Model.Base;
using RestfulAspNetCore.Model.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestfulAspNetCore.Repositories.Generics
{
    public class GenericRepository<T> : IRepository<T> where T : BaseEntity
    {
        private MySqlContext _context;
        private DbSet<T> _dataset;

        public GenericRepository(MySqlContext context)
        {
            _context = context;
            _dataset = context.Set<T>();
        }

        public T Atualizar(T item)
        {
            if (!Existe(item.Id)) return null;

            var encontrado = _dataset.SingleOrDefault(p => p.Id.Equals(item.Id));

            try
            {
                _context.Entry(encontrado).CurrentValues.SetValues(item);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return item;
        }

        public T Criar(T item)
        {
            try
            {
                _dataset.Add(item);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return item;
        }

        public void Excluir(long id)
        {
            var encontrado = _dataset.SingleOrDefault(p => p.Id.Equals(id));

            try
            {
                if (encontrado == null)
                    return;

                _dataset.Remove(encontrado);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Existe(long? id)
        {
            return _dataset.Any(p => p.Id.Equals(id));
        }

        public T RetornarPorId(long id)
        {
            return _dataset.SingleOrDefault(p => p.Id.Equals(id));
        }

        public List<T> RetornarTodos()
        {
            return _dataset.ToList();
        }
    }
}
