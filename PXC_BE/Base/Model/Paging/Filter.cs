using System.Collections.Generic;
using System.Data;

namespace FTS.Base.Model.Paging
{
    /// <summary>
    /// Đối tượng filter
    /// </summary>
    /// Created by: MTLUC - 05/01/2022 
    public class Filter
    {
        /// <summary>
        /// Tên trường tìm kiếm
        /// </summary>
        public string Field { get; set; }

        /// <summary>
        /// Điều kiện lọc
        /// </summary>
        public string Operator { get; set; }

        /// <summary>
        /// Giá trị lọc
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// DbType (trường này không cần truyền từ client)
        /// </summary>
        public DbType DbType { get; set; }

        /// <summary>
        /// Tên param (trường này không cần truyền từ client)
        /// </summary>
        public string ParamName { get; set; }
    }

    public class FilterGroup
    {
        public List<Filter> Filters { get; set; }
        public string Logic { get; set; } = "AND";
    }
}
