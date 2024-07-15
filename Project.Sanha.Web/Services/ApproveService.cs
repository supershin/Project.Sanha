using Microsoft.CodeAnalysis;
using Microsoft.VisualBasic;
using Project.Sanha.Web.Common;
using Project.Sanha.Web.Data;
using Project.Sanha.Web.Models;
using Project.Sanha.Web.Repositories;

namespace Project.Sanha.Web.Services
{
    public class ApproveService : IApprove
    {
        private readonly SanhaDbContext _context;
       
        public ApproveService( SanhaDbContext context)
        {
            _context = context;
          
           
        }

        public dynamic GetTransList(DTParamModel param, ApproveModel criteria)
        {
            //throw new NotImplementedException();
         
                var totalRecord = 0;
                bool asc = param.sortDirection.ToUpper().Contains("ASC");
            //variable = (condition) ? expressionTrue :  expressionFalse;
            criteria.strSearch = (criteria.strSearch == null) ? string.Empty :  criteria.strSearch.ToString() ?? string.Empty;

                var query = from u in _context.master_unit //.Where(e => e.FlagActive == true)
                            
                            select new
                            {
                                //unit_id =u.ID,
                                //customer_name = u.Name,
                                //create_date =u.CreateDate
                                ID = u.id,
                                unit_id = u.unit_id,
                                customer_name = u.customer_name,
                                create_date = u.create_on
                            };
                //query = query.Where(e => e.LastSaleOrder.QuotationNumber.Contains(criteria.strSearch)
                //                    || e.LastSaleOrder.ContracNumber.Contains(criteria.strSearch)
                //                    || e.LastSaleOrder.BookingNumber.Contains(criteria.strSearch)
                //                    || e.UnitCode.Contains(criteria.strSearch)
                //                    || e.AddrNo.Contains(criteria.strSearch)
                //                    || e.Build.Contains(criteria.strSearch)
                //                    || e.Floor.Contains(criteria.strSearch)
                //                    || criteria.strSearch == string.Empty
                //                    || criteria.strSearch == null || criteria.strSearch == string.Empty);

                var result = query.Page(param.start, param.length, i => i.create_date, param.SortColumnName, asc, out totalRecord);
                param.TotalRowCount = totalRecord;
                return result.AsEnumerable().Select(e => new
                {
                    e.ID,
                    e.unit_id,
                    e.customer_name,
                    e.create_date,
                   
                }).ToList();
            
        }
    }
}
