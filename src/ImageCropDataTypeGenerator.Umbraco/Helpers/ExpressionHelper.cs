using System;
using System.Linq.Expressions;

namespace ImageCropDataTypeGenerator.Umbraco.Helpers
{
    internal static class ExpressionHelper
    {
        public static ParameterExpression Parameter<T>()
            => Expression.Parameter(typeof(T));

        public static ParameterExpression Parameter<T>(string name)
            => Expression.Parameter(typeof(T), name);

        public static string GetName<T>(Expression<Func<T>> action)
            => GetNameFromMemberExpression(action.Body);

        public static string GetNameFromMemberExpression(Expression expression)
            => expression switch
            {
                MemberExpression memberExpression => memberExpression.Member.Name,
                UnaryExpression unaryExpression => GetNameFromMemberExpression(unaryExpression.Operand),
                _ => "MemberNameUnknown"
            };
    }
}