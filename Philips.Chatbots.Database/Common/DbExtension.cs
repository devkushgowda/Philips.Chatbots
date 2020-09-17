using MongoDB.Driver;
using Philips.Chatbots.Data.Models.Interfaces;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Philips.Chatbots.Database.Common
{
    /// <summary>
    /// Common DB extensions.
    /// </summary>
    public static class DbExtension
    {
        /// <summary>
        /// Get any specific field of the document mentioned in the expression.
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="collection"></param>
        /// <param name="id"></param>
        /// <param name="fieldExpression"></param>
        /// <returns></returns>
        public static async Task<TValue> GetFieldValue<TModel, TValue>(this IMongoCollection<TModel> collection, string id, Expression<Func<TModel, TValue>> fieldExpression) where TModel : IDataModel
        {
            var propertyValue = await collection
                .Find(d => d._id == id)
                .Project(new ProjectionDefinitionBuilder<TModel>().Expression(fieldExpression))
                .FirstOrDefaultAsync();
            return propertyValue;
        }
    }
}
