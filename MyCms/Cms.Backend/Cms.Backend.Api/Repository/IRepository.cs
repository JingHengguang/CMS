using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;


namespace Cms.Backend.Api.Repository
{

    /// <summary>
    /// 定义一个接口 用于实体类型的CRUD操作
    /// </summary>
    public interface IRepository<T>
    {
        // 用于显示可查询的表
        IQueryable<T> Table { get; }

        // 根据Id查找对应的T类型的示例，提供的id，这里可能是int、long、guid等常见的主键类型
        T GetById(object Id);

        //根据实体对象 异步 插入到数据库
        Task InsertAsync(T entity);

        // 根据实体对象插入到数据库
        void Insert(T entity);

        //通过Id删除某一行数据
        void Delete(object Id);

        void Update(T entity);


    }

}