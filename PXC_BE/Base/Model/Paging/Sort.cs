namespace FTS.Base.Model.Paging
{
    /// <summary>
    /// Đối tượng sắp xếp
    /// </summary>
    public class Sort
    {
        /// <summary>
        /// Tên trường sắp xếp
        /// </summary>
        public string Field { get; set; }

        /// <summary>
        /// Điều kiện lọc (asc|desc)
        /// </summary>
        public string Dir { get; set; }

    }
}
