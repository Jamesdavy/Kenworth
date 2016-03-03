using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper.QueryableExtensions;
using DelegateDecompiler;

namespace WebApplication.Infrastructure.Extensions.AutoMapper
{
    public static class Decompiler
    {
        public static List<TDestination> ToList<TDestination>(
            this IProjectionExpression projectionExpression)
        {
            return projectionExpression.To<TDestination>().Decompile().ToList();
        }

        public static IQueryable<TDestination> ToQueryable<TDestination>(
            this IProjectionExpression projectionExpression)
        {
            return projectionExpression.To<TDestination>().Decompile();
        }

        public static TDestination[] ToArray<TDestination>(
            this IProjectionExpression projectionExpression)
        {
            return projectionExpression.To<TDestination>().Decompile().ToArray();
        }

        public static TDestination ToSingleOrDefault<TDestination>(
            this IProjectionExpression projectionExpression)
        {
            return projectionExpression.To<TDestination>().Decompile().ToList().SingleOrDefault();
        }
    }
}