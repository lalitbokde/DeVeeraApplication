using CRM.Core.Infrastructure;
using CRM.Core;
using DeVeeraApp.ViewModels;
using System;
using System.Collections.Generic;

namespace DeVeeraApp.Utils
{
    /// <summary>
    /// Represents the extensions to map entity to model and vise versa
    /// </summary>
    public static class MappingExtensions
    {
        #region Utilities

        /// <summary>
        /// Execute a mapping from the source object to a new destination object. The source type is inferred from the source object
        /// </summary>
        /// <typeparam name="TDestination">Destination object type</typeparam>
        /// <param name="source">Source object to map from</param>
        /// <returns>Mapped destination object</returns>
        private static TDestination Map<TDestination>(this object source)
        {
            //use AutoMapper for mapping objects
            return AutoMapperConfiguration.Mapper.Map<TDestination>(source);
        }

        /// <summary>
        /// Execute a mapping from the source object to the existing destination object
        /// </summary>
        /// <typeparam name="TSource">Source object type</typeparam>
        /// <typeparam name="TDestination">Destination object type</typeparam>
        /// <param name="source">Source object to map from</param>
        /// <param name="destination">Destination object to map into</param>
        /// <returns>Mapped destination object, same instance as the passed destination object</returns>
        private static TDestination MapTo<TSource, TDestination>(this TSource source, TDestination destination)
        {
            //use AutoMapper for mapping objects
            return AutoMapperConfiguration.Mapper.Map(source, destination);
        }

        #endregion

        #region Methods

        #region Model-Entity mapping

        /// <summary>
        /// Execute a mapping from the entity to a new model
        /// </summary>
        /// <typeparam name="TModel">Model type</typeparam>
        /// <param name="entity">Entity to map from</param>
        /// <returns>Mapped model</returns>
        public static TModel ToModel<TModel>(this BaseEntity entity) where TModel : BaseEntityModel
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            return entity.Map<TModel>();
        }

        /// <summary>
        /// Execute a mapping from the entity to the existing model
        /// </summary>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <typeparam name="TModel">Model type</typeparam>
        /// <param name="entity">Entity to map from</param>
        /// <param name="model">Model to map into</param>
        /// <returns>Mapped model</returns>
        public static TModel ToModel<TEntity, TModel>(this TEntity entity, TModel model)
            where TEntity : BaseEntity where TModel : BaseEntityModel
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            if (model == null)
                throw new ArgumentNullException(nameof(model));

            return entity.MapTo(model);
        }

        /// <summary>
        /// Execute a mapping from the entity to the existing model
        /// </summary>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <typeparam name="TModel">Model type</typeparam>
        /// <param name="entity">Entity to map from</param>
        /// <param name="model">Model to map into</param>
        /// <returns>Mapped model</returns>
        public static List<TModel> ToModelList<TEntity, TModel>(this List<TEntity> entity, List<TModel> model)
            where TEntity : BaseEntity where TModel : BaseEntityModel
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            if (model == null)
                throw new ArgumentNullException(nameof(model));

            return entity.MapTo(model);
        }

        /// <summary>
        /// Execute a mapping from the model to a new entity
        /// </summary>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <param name="model">Model to map from</param>
        /// <returns>Mapped entity</returns>
        public static TEntity ToEntity<TEntity>(this BaseEntityModel model) where TEntity : BaseEntity
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            return model.Map<TEntity>();
        }

        /// <summary>
        /// Execute a mapping from the model to the existing entity
        /// </summary>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <typeparam name="TModel">Model type</typeparam>
        /// <param name="model">Model to map from</param>
        /// <param name="entity">Entity to map into</param>
        /// <returns>Mapped entity</returns>
        public static TEntity ToEntity<TEntity, TModel>(this TModel model, TEntity entity)
            where TEntity : BaseEntity where TModel : BaseEntityModel
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            return model.MapTo(entity);
        }

        #endregion

     

        // below is the code to implement to pagination of grid

        ///// <summary>
        ///// Convert list to paged list according to paging request
        ///// </summary>
        ///// <typeparam name="T">Object type</typeparam>
        ///// <param name="list">List of objects</param>
        ///// <param name="pagingRequestModel">Paging request model</param>
        ///// <returns>Paged list</returns>
        //public static IPagedList<T> ToPagedList<T>(this IList<T> list, IPagingRequestModel pagingRequestModel)
        //{
        //    return new PagedList<T>(list, pagingRequestModel.Page - 1, pagingRequestModel.PageSize);
        //}

        ///// <summary>
        ///// Prepare passed list model to display in a grid
        ///// </summary>
        ///// <typeparam name="TListModel">List model type</typeparam>
        ///// <typeparam name="TModel">Model type</typeparam>
        ///// <typeparam name="TObject">Object type</typeparam>
        ///// <param name="listModel">List model</param>
        ///// <param name="searchModel">Search model</param>
        ///// <param name="objectList">Paged list of objects</param>
        ///// <param name="dataFillFunction">Function to populate model data</param>
        ///// <returns>List model</returns>
        //public static TListModel PrepareToGrid<TListModel, TModel, TObject>(this TListModel listModel,
        //    BaseSearchModel searchModel, IPagedList<TObject> objectList, Func<IEnumerable<TModel>> dataFillFunction)
        //    where TListModel : BasePagedListModel<TModel>
        //    where TModel : BaseNopModel
        //{
        //    if (listModel == null)
        //        throw new ArgumentNullException(nameof(listModel));

        //    listModel.Data = dataFillFunction?.Invoke();
        //    listModel.Draw = searchModel?.Draw;
        //    listModel.RecordsTotal = objectList?.TotalCount ?? 0;
        //    listModel.RecordsFiltered = objectList?.TotalCount ?? 0;

        //    return listModel;
        //}

        #endregion
    }
}
