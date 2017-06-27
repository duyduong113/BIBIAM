using Kendo.Mvc;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using ServiceStack.OrmLite;

namespace ERPAPD.Helpers
{
    public static class KendoApplyFilter
    {
        public static string ApplyFilter(IFilterDescriptor filter, string id)
        {
            var filters = string.Empty;
            if (filter is CompositeFilterDescriptor)
            {
                filters += "(";
                var compositeFilterDescriptor = (CompositeFilterDescriptor)filter;
                foreach (IFilterDescriptor childFilter in compositeFilterDescriptor.FilterDescriptors)
                {
                    filters += ApplyFilter(childFilter, id);
                    filters += string.Format(" {0} ", compositeFilterDescriptor.LogicalOperator.ToString());
                }
            }
            else
            {
                string filterDescriptor = String.Empty;
                var descriptor = (FilterDescriptor)filter;
                var filterMember = descriptor.Member;
                var filterValue = descriptor.Value.ToString().Replace("'", "''");

                DateTime temp;

                switch (descriptor.Operator)
                {
                    case FilterOperator.IsEqualTo:
                        if (filterMember.Contains('.'))
                            filterDescriptor += String.Format("{0} = N'{1}'", id + filterMember, filterValue);
                        else
                            filterDescriptor += String.Format("{0} = N'{1}'", id + "[" + filterMember + "]", filterValue);
                        break;
                    case FilterOperator.IsNotEqualTo:
                        if (filterMember.Contains('.'))
                            filterDescriptor += String.Format("{0} <> N'{1}'", id + filterMember, filterValue);
                        else
                            filterDescriptor += String.Format("{0} <> N'{1}'", id + "[" + filterMember + "]", filterValue);
                        break;
                    case FilterOperator.StartsWith:
                        if (filterMember.Contains('.'))
                            filterDescriptor += String.Format("{0} COLLATE Latin1_General_CI_AI LIKE N'{1}%'", id + filterMember, filterValue);
                        else
                            filterDescriptor += String.Format("{0} COLLATE Latin1_General_CI_AI LIKE N'{1}%'", id + "[" + filterMember + "]", filterValue);

                        break;
                    case FilterOperator.Contains:
                        if (filterMember.Contains('.'))
                            filterDescriptor += String.Format("{0} COLLATE Latin1_General_CI_AI LIKE N'%{1}%'", id + filterMember, filterValue);
                        else
                            filterDescriptor += String.Format("{0} COLLATE Latin1_General_CI_AI LIKE N'%{1}%'", id + "[" + filterMember + "]", filterValue);

                        break;
                    case FilterOperator.EndsWith:
                        if (filterMember.Contains('.'))
                            filterDescriptor += String.Format("{0} COLLATE Latin1_General_CI_AI LIKE N'%{1}'", id + filterMember, filterValue);
                        else
                            filterDescriptor += String.Format("{0} COLLATE Latin1_General_CI_AI LIKE N'%{1}'", id + "[" + filterMember + "]", filterValue);
                        break;
                    case FilterOperator.IsLessThanOrEqualTo:
                        if (DateTime.TryParse(filterValue.ToString(), out temp))
                        {
                            filterDescriptor += String.Format("{0} <='{1}'", id + "[" + filterMember + "]", filterValue);
                        }
                        else
                        {
                            filterDescriptor += String.Format("{0} <={1}", id + "[" + filterMember + "]", filterValue);
                        }
                        break;
                    case FilterOperator.IsLessThan:
                        if (DateTime.TryParse(filterValue.ToString(), out temp))
                        {
                            filterDescriptor += String.Format("{0}<'{1}'", id + "[" + filterMember + "]", filterValue);
                        }
                        else
                        {
                            filterDescriptor += String.Format("{0}<{1}", id + "[" + filterMember + "]", filterValue);
                        }


                        break;
                    case FilterOperator.IsGreaterThanOrEqualTo:
                        if (DateTime.TryParse(filterValue.ToString(), out temp))
                        {
                            filterDescriptor += String.Format("{0}>='{1}'", id + "[" + filterMember + "]", filterValue);
                        }
                        else
                        {
                            filterDescriptor += String.Format("{0}>={1}", id + "[" + filterMember + "]", filterValue);
                        }

                        break;
                    case FilterOperator.IsGreaterThan:
                        if (DateTime.TryParse(filterValue.ToString(), out temp))
                        {
                            filterDescriptor += String.Format("{0}>'{1}'", id + "[" + filterMember + "]", filterValue);
                        }
                        else
                        {
                            filterDescriptor += String.Format("{0}>{1}", id + "[" + filterMember + "]", filterValue);
                        }

                        break;
                }



                filters = filterDescriptor;
            }

            filters = filters.EndsWith("And ") == true ? string.Format("{0})", filters.Substring(0, filters.Length - 4)) : filters;
            filters = filters.EndsWith("Or ") == true ? string.Format("{0})", filters.Substring(0, filters.Length - 4)) : filters;

            return filters;
        }

        public static string ApplyFreeTextFilter(IFilterDescriptor filter, string id)
        {
            var filters = string.Empty;
            if (filter is CompositeFilterDescriptor)
            {
                filters += "(";
                var compositeFilterDescriptor = (CompositeFilterDescriptor)filter;
                foreach (IFilterDescriptor childFilter in compositeFilterDescriptor.FilterDescriptors)
                {
                    filters += ApplyFilter(childFilter, id);
                    filters += string.Format(" {0} ", compositeFilterDescriptor.LogicalOperator.ToString());
                }
            }
            else
            {
                string filterDescriptor = String.Empty;
                var descriptor = (FilterDescriptor)filter;
                var filterMember = descriptor.Member;
                var filterValue = descriptor.Value.ToString().Replace("'", "''");

                DateTime temp;

                filterDescriptor = String.Format(@" CONTAINS((Meta, MetaOCM),'""*{0}*""')", filterValue);
                filters = filterDescriptor;
            }

            filters = filters.EndsWith("And ") == true ? string.Format("{0})", filters.Substring(0, filters.Length - 4)) : filters;
            filters = filters.EndsWith("Or ") == true ? string.Format("{0})", filters.Substring(0, filters.Length - 4)) : filters;

            return filters;
        }


        public static string ApplyFilterExclude(IFilterDescriptor filter, string id, List<String> exclude)
        {
            var filters = string.Empty;
            if (filter is CompositeFilterDescriptor)
            {
                filters += "(";
                var compositeFilterDescriptor = (CompositeFilterDescriptor)filter;
                foreach (IFilterDescriptor childFilter in compositeFilterDescriptor.FilterDescriptors)
                {
                    filters += ApplyFilterExclude(childFilter, id, exclude);
                    filters += string.Format(" {0} ", compositeFilterDescriptor.LogicalOperator.ToString());
                }
            }
            else
            {

                string filterDescriptor = String.Empty;
                var descriptor = (FilterDescriptor)filter;
                var filterMember = descriptor.Member;
                var filterValue = descriptor.Value;
                if (!exclude.Contains(descriptor.Member))
                {
                    DateTime temp;

                    switch (descriptor.Operator)
                    {
                        case FilterOperator.IsEqualTo:
                            filterDescriptor += String.Format("{0} = N'{1}'", id + "[" + filterMember + "]", filterValue);
                            break;
                        case FilterOperator.IsNotEqualTo:
                            filterDescriptor += String.Format("{0} <> N'{1}'", id + "[" + filterMember + "]", filterValue);
                            break;
                        case FilterOperator.StartsWith:
                            filterDescriptor += String.Format("{0} like N'{1}%'", id + "[" + filterMember + "]", filterValue);
                            break;
                        case FilterOperator.Contains:
                            filterDescriptor += String.Format("{0} like N'%{1}%'", id + "[" + filterMember + "]", filterValue);
                            break;
                        case FilterOperator.EndsWith:
                            filterDescriptor += String.Format("{0} like N'%{1}'", id + "[" + filterMember + "]", filterValue);
                            break;
                        case FilterOperator.IsLessThanOrEqualTo:
                            if (DateTime.TryParse(filterValue.ToString(), out temp))
                            {
                                filterDescriptor += String.Format("{0} <='{1}'", id + "[" + filterMember + "]", filterValue);
                            }
                            else
                            {
                                filterDescriptor += String.Format("{0} <={1}", id + "[" + filterMember + "]", filterValue);
                            }


                            break;
                        case FilterOperator.IsLessThan:
                            if (DateTime.TryParse(filterValue.ToString(), out temp))
                            {
                                filterDescriptor += String.Format("{0}<'{1}'", id + "[" + filterMember + "]", filterValue);
                            }
                            else
                            {
                                filterDescriptor += String.Format("{0}<{1}", id + "[" + filterMember + "]", filterValue);
                            }


                            break;
                        case FilterOperator.IsGreaterThanOrEqualTo:
                            if (DateTime.TryParse(filterValue.ToString(), out temp))
                            {
                                filterDescriptor += String.Format("{0}>='{1}'", id + "[" + filterMember + "]", filterValue);
                            }
                            else
                            {
                                filterDescriptor += String.Format("{0}>={1}", id + "[" + filterMember + "]", filterValue);
                            }

                            break;
                        case FilterOperator.IsGreaterThan:
                            if (DateTime.TryParse(filterValue.ToString(), out temp))
                            {
                                filterDescriptor += String.Format("{0}>'{1}'", id + "[" + filterMember + "]", filterValue);
                            }
                            else
                            {
                                filterDescriptor += String.Format("{0}>{1}", id + "[" + filterMember + "]", filterValue);
                            }

                            break;
                    }
                }
                else
                {
                    filterDescriptor += "1=1";
                }

                filters = filterDescriptor;
            }

            filters = filters.EndsWith("And ") == true ? string.Format("{0})", filters.Substring(0, filters.Length - 4)) : filters;
            filters = filters.EndsWith("Or ") == true ? string.Format("{0})", filters.Substring(0, filters.Length - 4)) : filters;

            return filters;
        }

        public static string ApplyFilterNotN(IFilterDescriptor filter, string id)
        {
            var filters = string.Empty;
            if (filter is CompositeFilterDescriptor)
            {
                filters += "(";
                var compositeFilterDescriptor = (CompositeFilterDescriptor)filter;
                foreach (IFilterDescriptor childFilter in compositeFilterDescriptor.FilterDescriptors)
                {
                    filters += ApplyFilterNotN(childFilter, id);
                    filters += string.Format(" {0} ", compositeFilterDescriptor.LogicalOperator.ToString());
                }
            }
            else
            {
                string filterDescriptor = String.Empty;
                var descriptor = (FilterDescriptor)filter;
                var filterMember = descriptor.Member;
                var filterValue = descriptor.Value;

                DateTime temp;

                switch (descriptor.Operator)
                {
                    case FilterOperator.IsEqualTo:
                        filterDescriptor += String.Format("{0} = '{1}'", id + "[" + filterMember + "]", filterValue);
                        break;
                    case FilterOperator.IsNotEqualTo:
                        filterDescriptor += String.Format("{0} <> '{1}'", id + "[" + filterMember + "]", filterValue);
                        break;
                    case FilterOperator.StartsWith:
                        filterDescriptor += String.Format("{0} like '{1}%'", id + "[" + filterMember + "]", filterValue);
                        break;
                    case FilterOperator.Contains:
                        filterDescriptor += String.Format("{0} like '%{1}%'", id + "[" + filterMember + "]", filterValue);
                        break;
                    case FilterOperator.EndsWith:
                        filterDescriptor += String.Format("{0} like '%{1}'", id + "[" + filterMember + "]", filterValue);
                        break;
                    case FilterOperator.IsLessThanOrEqualTo:
                        if (DateTime.TryParse(filterValue.ToString(), out temp))
                        {
                            filterDescriptor += String.Format("{0} <='{1}'", id + "[" + filterMember + "]", filterValue);
                        }
                        else
                        {
                            filterDescriptor += String.Format("{0} <={1}", id + "[" + filterMember + "]", filterValue);
                        }
                        break;
                    case FilterOperator.IsLessThan:
                        if (DateTime.TryParse(filterValue.ToString(), out temp))
                        {
                            filterDescriptor += String.Format("{0}<'{1}'", id + "[" + filterMember + "]", filterValue);
                        }
                        else
                        {
                            filterDescriptor += String.Format("{0}<{1}", id + "[" + filterMember + "]", filterValue);
                        }
                        break;
                    case FilterOperator.IsGreaterThanOrEqualTo:
                        if (DateTime.TryParse(filterValue.ToString(), out temp))
                        {
                            filterDescriptor += String.Format("{0}>='{1}'", id + "[" + filterMember + "]", filterValue);
                        }
                        else
                        {
                            filterDescriptor += String.Format("{0}>={1}", id + "[" + filterMember + "]", filterValue);
                        }
                        break;
                    case FilterOperator.IsGreaterThan:
                        if (DateTime.TryParse(filterValue.ToString(), out temp))
                        {
                            filterDescriptor += String.Format("{0}>'{1}'", id + "[" + filterMember + "]", filterValue);
                        }
                        else
                        {
                            filterDescriptor += String.Format("{0}>{1}", id + "[" + filterMember + "]", filterValue);
                        }
                        break;
                }

                filters = filterDescriptor;
            }

            filters = filters.EndsWith("And ") == true ? string.Format("{0})", filters.Substring(0, filters.Length - 4)) : filters;
            filters = filters.EndsWith("Or ") == true ? string.Format("{0})", filters.Substring(0, filters.Length - 4)) : filters;

            return filters;
        }
        public static DataSourceResult KendoData<T>([DataSourceRequest]DataSourceRequest request)
        {
            using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
            using (var dbCommand = dbConn.CreateCommand())
            {
                var data = new DataSourceResult();
                //filter
                List<string> sort = new List<string>();
                var Tname = typeof(T).Name;
                if (request.Sorts.Any())
                {
                    foreach (SortDescriptor sortDescriptor in request.Sorts)
                    {
                        if (sortDescriptor.SortDirection == ListSortDirection.Ascending)
                        {
                            sort.Add(sortDescriptor.Member + " ASC");
                        }
                        else
                        {
                            sort.Add(sortDescriptor.Member + " DESC");
                        }
                    }
                }

                string sortString = string.Join(",", sort.Select(s => s));


                if (request.Filters.Any())
                {

                    var where = KendoApplyFilter.ApplyFilter(request.Filters[0], "");
                    var order = (!String.IsNullOrEmpty(sortString) ? (" Order By " + sortString) : "");
                    if (request.PageSize > 0)
                    {
                        data.Data = dbCommand.Select<T>(q => q.Select("SELECT * FROM " + Tname + " WHERE" + where + order).Limit(skip: (request.Page - 1) * request.PageSize, rows: request.PageSize));
                        if (request.Page > 1)
                        {
                            var sql = dbCommand.CommandText.Replace(" )", " " + " WHERE" + where + ")") + order;
                            data.Data = dbConn.Select<T>(sql);
                        }
                    }
                    else
                    {
                        data.Data = dbConn.Select<T>(q => q.Select("SELECT * FROM " + Tname + " WHERE" + where + order));
                    }

                    data.Total = dbConn.Scalar<int>("SELECT COUNT(*) FROM " + Tname + " WHERE " + where);
                }
                else
                {
                    var where = (!String.IsNullOrEmpty(sortString) ? (" Order By " + sortString) : "");
                    if (request.PageSize > 0)
                        data.Data = dbConn.Select<T>(q => q.Select("SELECT * FROM " + Tname + where).Limit(skip: (request.Page - 1) * request.PageSize, rows: request.PageSize));
                    else
                        data.Data = dbConn.Select<T>(q => q.Select("SELECT * FROM " + Tname + where));
                    data.Total = dbConn.Scalar<int>("SELECT COUNT(*) FROM " + Tname);
                }
                return data;
            }
        }
        public static DataSourceResult KendoData<T>([DataSourceRequest]DataSourceRequest request, string CustomWhere)
        {
            using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
            using (var dbCommand = dbConn.CreateCommand())
            {
                var data = new DataSourceResult();
                //filter
                List<string> sort = new List<string>();
                var Tname = typeof(T).Name;
                if (request.Sorts.Any())
                {
                    foreach (SortDescriptor sortDescriptor in request.Sorts)
                    {
                        if (sortDescriptor.SortDirection == ListSortDirection.Ascending)
                        {
                            sort.Add(sortDescriptor.Member + " ASC");
                        }
                        else
                        {
                            sort.Add(sortDescriptor.Member + " DESC");
                        }
                    }
                }

                string sortString = string.Join(",", sort.Select(s => s));


                if (request.Filters.Any())
                {

                    var where = KendoApplyFilter.ApplyFilter(request.Filters[0], "") + " AND " + CustomWhere;
                    var order = (!String.IsNullOrEmpty(sortString) ? (" Order By " + sortString) : "");
                    if (request.PageSize > 0)
                    {
                        data.Data = dbConn.Select<T>(q => q.Select("SELECT * FROM " + Tname + " WHERE" + where + order).Limit(skip: (request.Page - 1) * request.PageSize, rows: request.PageSize));
                        if (request.Page > 1)
                        {
                            var sql = dbCommand.CommandText.Replace(" )", " " + " WHERE" + where + ")") + order;
                            data.Data = dbConn.Select<T>(sql);
                        }
                    }
                    else
                    {
                        data.Data = dbConn.Select<T>(q => q.Select("SELECT * FROM " + Tname + " WHERE" + where + order));
                    }

                    data.Total = dbConn.Scalar<int>("SELECT COUNT(*) FROM " + Tname + " WHERE " + where);
                }
                else
                {
                    var where = " WHERE " + CustomWhere;
                    var order = (!String.IsNullOrEmpty(sortString) ? (" Order By " + sortString) : "");
                    if (request.PageSize > 0)
                    {
                        try
                        {
                            data.Data = dbCommand.Select<T>(q => q.Select("SELECT * FROM " + Tname + where + order).Limit(skip: (request.Page - 1) * request.PageSize, rows: request.PageSize));
                            if (request.Page > 1)
                            {
                                var sql = dbCommand.CommandText.Replace(" )", " " + where + ")") + order;
                                data.Data = dbConn.Select<T>(sql);
                            }
                        }
                        catch (Exception ex)
                        {
                            data.Data = null;
                        }

                    }
                    else
                    {
                        data.Data = dbConn.Select<T>(q => q.Select("SELECT * FROM " + Tname + where + order));
                    }
                    data.Total = dbConn.Scalar<int>("SELECT COUNT(*) FROM " + Tname + where);
                }
                return data;
            }
        }


        public static DataSourceResult KendoDataByQuery<T>([DataSourceRequest]DataSourceRequest request, string Query, string CustomWhere)
        {
            using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
            using (var dbCommand = dbConn.CreateCommand())
            {
                var data = new DataSourceResult();
                //filter
                List<string> sort = new List<string>();
                var Tname = typeof(T).Name;
                if (request.Sorts.Any())
                {
                    foreach (SortDescriptor sortDescriptor in request.Sorts)
                    {
                        if (sortDescriptor.SortDirection == ListSortDirection.Ascending)
                        {
                            sort.Add(sortDescriptor.Member + " ASC");
                        }
                        else
                        {
                            sort.Add(sortDescriptor.Member + " DESC");
                        }
                    }
                }

                string sortString = string.Join(",", sort.Select(s => s));


                if (request.Filters.Any())
                {
                    var where = KendoApplyFilter.ApplyFilter(request.Filters[0], "") + (string.IsNullOrWhiteSpace(CustomWhere) ? "" : " AND " + CustomWhere);
                    var order = (!String.IsNullOrEmpty(sortString) ? (" Order By " + sortString) : "");
                    if (request.PageSize > 0)
                    {
                        if (request.Page > 1)
                        {
                            string newquery = @"SELECT  *
                                        FROM    ( SELECT ROW_NUMBER() OVER ( " + (!string.IsNullOrEmpty(order.Trim()) ? order : "ORDER BY (SELECT NULL)") + @") AS RowNum, * FROM(" + Query + ")s WHERE " + where + @"
                                                ) AS result
                                        WHERE   RowNum > " + (request.Page - 1) * request.PageSize + @"
                                            AND RowNum <= " + request.Page * request.PageSize + @"
                                        ORDER BY RowNum";
                            data.Data = dbConn.Query<T>(newquery);
                        }
                        else
                        {
                            string newquery = @"SELECT  TOP " + request.PageSize + @" * FROM(" + Query + ")s WHERE " + where + order;
                            data.Data = dbConn.Query<T>(newquery);
                        }

                    }
                    else
                    {
                        data.Data = dbConn.Select<T>(q => q.Select(Query).Where(where).OrderBy(order));
                    }

                    data.Total = dbConn.Scalar<int>("SELECT COUNT(*) FROM (" + Query + ") s WHERE " + where );
                }
                else
                {
                    var order = (!String.IsNullOrEmpty(sortString) ? (" Order By " + sortString) : "") + " ";
                    if (request.PageSize > 0)
                    {
                        if (request.Page > 1)
                        {
                            string newquery = @"SELECT  *
                                        FROM    ( SELECT ROW_NUMBER() OVER ( " + (!string.IsNullOrEmpty(order.Trim()) ? order : "ORDER BY (SELECT NULL)") + @") AS RowNum, * FROM(" + Query + ")s " + (string.IsNullOrWhiteSpace(CustomWhere) ? "" : " WHERE " + CustomWhere) + @"
                                                ) AS result
                                        WHERE   RowNum > " + (request.Page - 1) * request.PageSize + @"
                                            AND RowNum <= " + request.Page * request.PageSize + @"
                                        ORDER BY RowNum";
                            data.Data = dbConn.Query<T>(newquery);
                        }
                        else
                        {
                            string newquery = @"SELECT  TOP " + request.PageSize + @" * FROM(" + Query + ")s  " + (string.IsNullOrWhiteSpace(CustomWhere) ? "" : " WHERE " + CustomWhere) + order;
                            data.Data = dbConn.Query<T>(newquery);
                        }
                    }
                    else
                    {
                        data.Data = dbConn.Select<T>(q => q.Select(Query).Where(CustomWhere).OrderBy(order));
                    }

                    data.Total = dbConn.Scalar<int>("SELECT COUNT(*) FROM (" + Query + (string.IsNullOrWhiteSpace(CustomWhere) ? "" : " WHERE " + CustomWhere) + ") a");
                }
                return data;
            }
        }

        public static DataSourceResult KendoDataByQuery<T>([DataSourceRequest]DataSourceRequest request, string Query, string CustomWhere, string sortPrefix)
        {
            using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
            using (var dbCommand = dbConn.CreateCommand())
            {
                var data = new DataSourceResult();
                //filter
                List<string> sort = new List<string>();
                var Tname = typeof(T).Name;
                if (request.Sorts.Any())
                {
                    foreach (SortDescriptor sortDescriptor in request.Sorts)
                    {
                        if (sortDescriptor.SortDirection == ListSortDirection.Ascending)
                        {
                            sort.Add(sortPrefix + sortDescriptor.Member + " ASC");
                        }
                        else
                        {
                            sort.Add(sortPrefix + sortDescriptor.Member + " DESC");
                        }
                    }
                }

                string sortString = string.Join(",", sort.Select(s => s));


                if (request.Filters.Any())
                {
                    var where = KendoApplyFilter.ApplyFilter(request.Filters[0], "") + (string.IsNullOrWhiteSpace(CustomWhere) ? "" : " AND " + CustomWhere);
                    var order = (!String.IsNullOrEmpty(sortString) ? (" Order By " + sortString) : "");
                    if (request.PageSize > 0)
                    {
                        if (request.Page > 1)
                        {
                            var q = Query.Replace("SELECT", "").Replace("select", "");
                            string newquery = @"SELECT  *
                                        FROM    ( SELECT ROW_NUMBER() OVER ( " + (!string.IsNullOrEmpty(order) ? "a." + order : "ORDER BY (SELECT NULL)") + @") AS RowNum, " + q + @"
                                                ) AS result
                                        WHERE   RowNum > " + (request.Page - 1) * request.PageSize + @"
                                            AND RowNum <= " + request.Page * request.PageSize + @"
                                        ORDER BY RowNum";
                            data.Data = dbConn.Query<T>(newquery);
                        }
                        else
                        {
                            data.Data = dbConn.Select<T>(q => q.Select(Query).Where(where).OrderBy(order).Limit(skip: ((request.Page - 1) * request.PageSize), rows: request.PageSize));
                        }

                    }
                    else
                    {
                        data.Data = dbConn.Select<T>(q => q.Select(Query).Where(where).OrderBy(order));
                    }

                    data.Total = dbConn.Scalar<int>("SELECT COUNT(*) FROM (" + Query + " WHERE " + where + ") a");
                }
                else
                {
                    var order = (!String.IsNullOrEmpty(sortString) ? (" Order By " + sortString) : "") + " ";
                    if (request.PageSize > 0)
                    {
                        data.Data = dbCommand.Select<T>(q => q.Select(Query).Where(CustomWhere).OrderBy(order).Limit(skip: ((request.Page - 1) * request.PageSize), rows: request.PageSize));
                        if (request.Page > 1)
                        {
                            var sql = dbCommand.CommandText.Replace(" )", " " + " WHERE " + CustomWhere + ")") + order;
                            data.Data = dbConn.Select<T>(sql);
                        }
                    }
                    else
                    {
                        data.Data = dbConn.Select<T>(q => q.Select(Query).Where(CustomWhere).OrderBy(order));
                    }

                    data.Total = dbConn.Scalar<int>("SELECT COUNT(*) FROM (" + Query + (string.IsNullOrWhiteSpace(CustomWhere) ? "" : " WHERE " + CustomWhere) + ") a");
                }
                return data;
            }
        }

        public static DataSourceResult KendoDataCustomerSupport<T>([DataSourceRequest]DataSourceRequest request)
        {
            using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
            using (var dbCommand = dbConn.CreateCommand())
            {
                var data = new DataSourceResult();
                //filter
                List<string> sort = new List<string>();
                var Tname = typeof(T).Name;
                if (request.Sorts.Any())
                {
                    foreach (SortDescriptor sortDescriptor in request.Sorts)
                    {
                        if (sortDescriptor.SortDirection == ListSortDirection.Ascending)
                        {
                            sort.Add(sortDescriptor.Member + " ASC");
                        }
                        else
                        {
                            sort.Add(sortDescriptor.Member + " DESC");
                        }
                    }
                }

                string sortString = string.Join(",", sort.Select(s => s));


                if (request.Filters.Any())
                {

                    var where = KendoApplyFilter.ApplyFreeTextFilter(request.Filters[0], "") + " AND [IsDelete] = 0 ";
                    var order = (!String.IsNullOrEmpty(sortString) ? (" Order By " + sortString) : "");
                    if (request.PageSize > 0)
                    {
                        data.Data = dbCommand.Select<T>(q => q.Select("SELECT * FROM " + Tname + " WHERE" + where + order).Limit(skip: (request.Page - 1) * request.PageSize, rows: request.PageSize));
                        if (request.Page > 1)
                        {
                            var sql = dbCommand.CommandText.Replace(" )", " " + where + ")") + order;
                            data.Data = dbConn.Select<T>(sql);
                        }
                    }
                    else
                    {
                        data.Data = dbConn.Select<T>(q => q.Select("SELECT * FROM " + Tname + " WHERE" + where + order));
                    }
                    data.Total = dbConn.Scalar<int>("SELECT COUNT(*) FROM " + Tname + " WHERE " + where);
                }
                else
                {
                    var where = " [IsDelete] = 0 ";
                    var order = (!String.IsNullOrEmpty(sortString) ? (" Order By " + sortString) : "");
                    if (request.PageSize > 0)
                    {
                        data.Data = dbCommand.Select<T>(q => q.Select("SELECT * FROM " + Tname + " WHERE " + where).Limit(skip: (request.Page - 1) * request.PageSize, rows: request.PageSize));
                        if (request.Page > 1)
                        {
                            var sql = dbCommand.CommandText.Replace(" )", " WHERE " + where + ")") + order;
                            data.Data = dbConn.Select<T>(sql);
                        }
                    }
                    else
                    {
                        data.Data = dbConn.Select<T>(q => q.Select("SELECT * FROM " + Tname + " WHERE " + where));
                    }
                    data.Total = dbConn.Scalar<int>("SELECT COUNT(*) FROM " + Tname + " WHERE " + where);
                }
                return data;
            }
        }

    }

}

//namespace ServiceStack.OrmLite.SqlServer
//{
//    public class MySqlServerOrmLiteDialectProvider : SqlServerOrmLiteDialectProvider
//    {
//        public override string ToSelectStatement(ModelDefinition modelDef,
//           string selectExpression,
//           string bodyExpression,
//           string orderByExpression = null,
//           int? offset = null,
//           int? rows = null)
//        {
//            var sb = new StringBuilder(selectExpression);
//            sb.Append(bodyExpression);

//            if (!offset.HasValue && !rows.HasValue)
//                return sb + orderByExpression;

//            if (offset.HasValue && offset.Value < 0)
//                throw new ArgumentException(string.Format("Skip value:'{0}' must be>=0", offset.Value));

//            if (rows.HasValue && rows.Value < 0)
//                throw new ArgumentException(string.Format("Rows value:'{0}' must be>=0", rows.Value));

//            var skip = offset.HasValue ? offset.Value : 0;
//            var take = rows.HasValue ? rows.Value : int.MaxValue;

//            var selectType = selectExpression.ToUpper().StartsWith("SELECT DISTINCT") ? "SELECT DISTINCT" : "SELECT";

//            //Temporary hack till we come up with a more robust paging sln for SqlServer
//            if (skip == 0)
//            {
//                var sql = sb + orderByExpression;

//                if (take == int.MaxValue)
//                    return sql;

//                if (sql.Length < "SELECT".Length) return sql;
//                sql = selectType + " TOP " + take + sql.Substring(selectType.Length);
//                return sql;
//            }

//            // Required because ordering is done by Windowing function
//            if (string.IsNullOrEmpty(orderByExpression))
//            {
//                if (modelDef.PrimaryKey == null)
//                    throw new ApplicationException("Malformed model, no PrimaryKey defined");

//                orderByExpression = string.Format("ORDER BY {0}",
//                    this.GetQuotedColumnName(modelDef.PrimaryKey.Name));
//            }

//            var ret = string.Format(
//                "SELECT * FROM (SELECT {0}, ROW_NUMBER() OVER ({1}) As RowNum {2}) AS RowConstrainedResult WHERE RowNum > {3} AND RowNum <= {4}",
//                selectExpression.Substring(selectType.Length),
//                orderByExpression,
//                bodyExpression,
//                skip,
//                take == int.MaxValue ? take : skip + take);

//            return ret;
//        }

//    }
//}