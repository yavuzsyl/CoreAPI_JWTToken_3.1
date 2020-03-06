using CoreAPIWToken.Domain.Repositories;
using CoreAPIWToken.Domain.Response;
using CoreAPIWToken.Domain.Services;
using CoreAPIWToken.Domain.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreAPIWToken.Service
{
    public class BaseService<T> : IBaseService<T> where T : class
    {

        private readonly IUnitOfWork unitOfWork;
        private readonly IRepository<T> repository;
        public BaseService(IUnitOfWork unitOfWork, IRepository<T> repository)
        {
            this.unitOfWork = unitOfWork;
            this.repository = repository;
        }


        public async Task<Response<T>> AddEntityAsync(T entity)
        {
            try
            {
                await repository.AddEntity(entity);
                await unitOfWork.CompleteAsync();
                return new Response<T>(entity);
            }
            catch (Exception ex)
            {

                return new Response<T>($"{typeof(T).Name} couldnt add due to : {ex.Message}");
            }
        }

        public async Task<Response<T>> FindEntityById(int id)
        {
            try
            {
                var entity = await repository.GetById(id);
                if (entity == null)
                    return new Response<T>($"Not found any {typeof(T).Name} ");
                return new Response<T>(entity);
            }
            catch (Exception ex)
            {
                return new Response<T>($"{typeof(T).Name}  couldnt find due to : {ex.Message}");

            }
        }

        public async Task<Response<List<T>>> ListAsync()
        {
            try
            {
                var entityList = await repository.GetList();
                if (entityList == null)
                    return new Response<List<T>>($"Not found {typeof(T).Name} ");
                return new Response<List<T>>(entityList.ToList());
            }
            catch (Exception ex)
            {
                return new Response<List<T>>($"{typeof(T).Name} 's couldnt list due to : {ex.Message}");

            }
        }

        public async Task<Response<T>> RemoveEntityAsync(int id)
        {

            try
            {
                var entity = await repository.GetById(id);
                if (entity == null)
                    return new Response<T>($"Not found {typeof(T).Name} ");
                await repository.DeleteEntity(id);
                await unitOfWork.CompleteAsync();
                return new Response<T>(entity);
            }
            catch (Exception ex)
            {
                return new Response<T>($"{typeof(T).Name} 's couldnt list due to : {ex.Message}");
            }
        }

        public async Task<Response<T>> UpdateEntityAsync(T entity, int id)
        {
            try
            {
                repository.UpdateEntity(entity);
                await unitOfWork.CompleteAsync();
                return new Response<T>(entity);
            }
            catch (Exception ex)
            {
                return new Response<T>($"Update {typeof(T).Name}  failed due to : {ex.Message}");

            }
        }
    }
}

