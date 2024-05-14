using newUserdefine1.Models;
using newUserdefine1.Data;
using newUserdefine1.Filter;
using newUserdefine1.Entities;
using newUserdefine1.Logger;
using Microsoft.AspNetCore.JsonPatch;
using System.Linq.Expressions;

namespace newUserdefine1.Services
{
    /// <summary>
    /// The userinroleService responsible for managing userinrole related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting userinrole information.
    /// </remarks>
    public interface IUserInRoleService
    {
        /// <summary>Retrieves a specific userinrole by its primary key</summary>
        /// <param name="id">The primary key of the userinrole</param>
        /// <returns>The userinrole data</returns>
        UserInRole GetById(Guid id);

        /// <summary>Retrieves a list of userinroles based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of userinroles</returns>
        List<UserInRole> Get(string filters, string searchTerm, int pageNumber = 1, int pageSize = 0, string sortField = null, string sortOrder = "asc");

        /// <summary>Adds a new userinrole</summary>
        /// <param name="model">The userinrole data to be added</param>
        /// <returns>The result of the operation</returns>
        Guid Create(UserInRole model);

        /// <summary>Updates a specific userinrole by its primary key</summary>
        /// <param name="id">The primary key of the userinrole</param>
        /// <param name="updatedEntity">The userinrole data to be updated</param>
        /// <returns>The result of the operation</returns>
        bool Update(Guid id, UserInRole updatedEntity);

        /// <summary>Updates a specific userinrole by its primary key</summary>
        /// <param name="id">The primary key of the userinrole</param>
        /// <param name="updatedEntity">The userinrole data to be updated</param>
        /// <returns>The result of the operation</returns>
        bool Patch(Guid id, JsonPatchDocument<UserInRole> updatedEntity);

        /// <summary>Deletes a specific userinrole by its primary key</summary>
        /// <param name="id">The primary key of the userinrole</param>
        /// <returns>The result of the operation</returns>
        bool Delete(Guid id);
    }

    /// <summary>
    /// The userinroleService responsible for managing userinrole related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting userinrole information.
    /// </remarks>
    public class UserInRoleService : IUserInRoleService
    {
        private newUserdefine1Context _dbContext;

        /// <summary>
        /// Initializes a new instance of the UserInRole class.
        /// </summary>
        /// <param name="dbContext">dbContext value to set.</param>
        public UserInRoleService(newUserdefine1Context dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>Retrieves a specific userinrole by its primary key</summary>
        /// <param name="id">The primary key of the userinrole</param>
        /// <returns>The userinrole data</returns>
        public UserInRole GetById(Guid id)
        {
            var entityData = _dbContext.UserInRole.IncludeRelated().FirstOrDefault(entity => entity.Id == id);
            return entityData;
        }

        /// <summary>Retrieves a list of userinroles based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of userinroles</returns>/// <exception cref="Exception"></exception>
        public List<UserInRole> Get(string filters, string searchTerm, int pageNumber, int pageSize, string sortField, string sortOrder)
        {
            List<FilterCriteria> filterCriteria = null;
            if (pageSize < 1)
            {
                throw new Exception("Page size invalid!");
            }

            if (pageNumber < 1)
            {
                throw new Exception("Page mumber invalid!");
            }

            if (!string.IsNullOrEmpty(filters))
            {
                filterCriteria = JsonHelper.Deserialize<List<FilterCriteria>>(filters);
            }

            var query = _dbContext.UserInRole.IncludeRelated().AsQueryable();
            int skip = (pageNumber - 1) * pageSize;
            var result = FilterService<UserInRole>.ApplyFilter(query, filterCriteria, searchTerm);
            if (!string.IsNullOrEmpty(sortField))
            {
                var parameter = Expression.Parameter(typeof(UserInRole), "b");
                var property = Expression.Property(parameter, sortField);
                var lambda = Expression.Lambda<Func<UserInRole, object>>(Expression.Convert(property, typeof(object)), parameter);
                if (sortOrder.Equals("asc", StringComparison.OrdinalIgnoreCase))
                {
                    result = result.OrderBy(lambda);
                }
                else if (sortOrder.Equals("desc", StringComparison.OrdinalIgnoreCase))
                {
                    result = result.OrderByDescending(lambda);
                }
                else
                {
                    throw new Exception("Invalid sort order. Use 'asc' or 'desc'");
                }
            }

            var paginatedResult = result.Skip(skip).Take(pageSize).ToList();
            return paginatedResult;
        }

        /// <summary>Adds a new userinrole</summary>
        /// <param name="model">The userinrole data to be added</param>
        /// <returns>The result of the operation</returns>
        public Guid Create(UserInRole model)
        {
            _dbContext.UserInRole.Add(model);
            _dbContext.SaveChanges();
            return model.Id;
        }

        /// <summary>Updates a specific userinrole by its primary key</summary>
        /// <param name="id">The primary key of the userinrole</param>
        /// <param name="updatedEntity">The userinrole data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Update(Guid id, UserInRole updatedEntity)
        {
            _dbContext.UserInRole.Update(updatedEntity);
            _dbContext.SaveChanges();
            return true;
        }

        /// <summary>Updates a specific userinrole by its primary key</summary>
        /// <param name="id">The primary key of the userinrole</param>
        /// <param name="updatedEntity">The userinrole data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Patch(Guid id, JsonPatchDocument<UserInRole> updatedEntity)
        {
            if (updatedEntity == null)
            {
                throw new Exception("Patch document is missing!");
            }

            var existingEntity = _dbContext.UserInRole.FirstOrDefault(t => t.Id == id);
            if (existingEntity == null)
            {
                throw new Exception("No data found!");
            }

            updatedEntity.ApplyTo(existingEntity);
            _dbContext.UserInRole.Update(existingEntity);
            _dbContext.SaveChanges();
            return true;
        }

        /// <summary>Deletes a specific userinrole by its primary key</summary>
        /// <param name="id">The primary key of the userinrole</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Delete(Guid id)
        {
            var entityData = _dbContext.UserInRole.IncludeRelated().FirstOrDefault(entity => entity.Id == id);
            if (entityData == null)
            {
                throw new Exception("No data found!");
            }

            _dbContext.UserInRole.Remove(entityData);
            _dbContext.SaveChanges();
            return true;
        }
    }
}