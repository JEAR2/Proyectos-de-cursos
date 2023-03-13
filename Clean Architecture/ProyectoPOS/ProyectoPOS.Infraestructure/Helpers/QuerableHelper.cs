using POS.Infraestructure.Commons.Bases.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Infraestructure.Helpers
{
    public static class QuerableHelper
    {
        public static IQueryable<T> Paginate<T>(this IQueryable<T> querable, BasePaginationRequest request)
        {
            return querable.Skip((request.NumPage - 1) * request.Records).Take(request.Records);
        }
    }
}