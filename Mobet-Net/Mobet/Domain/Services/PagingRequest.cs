namespace Mobet.Domain.Services
{
    public class PagingRequest : IRequest
    {
        /// <summary>
        /// 排序字段 
        /// </summary>
        public string Sorting { get; set; }
        /// <summary>
        /// 是否倒序
        /// </summary>
        public bool IsOrderDesc { get; set; }
        /// <summary>
        /// 当前页
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// 每页显示数量
        /// </summary>
        public int PageSize { get; set; }


    }
}