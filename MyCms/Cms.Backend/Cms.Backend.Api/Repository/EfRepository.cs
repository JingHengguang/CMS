using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Cms.Backend.Db;
using Microsoft.EntityFrameworkCore;
using System;
using Cms.Backend.Api.Model;

namespace Cms.Backend.Api.Repository
{
    public class EfRepository<T> : IRepository<T> where T : BaseModel
    {
       
        private CmsBackendDb _db;

        //构造函数，初始化中间件
        public EfRepository(CmsBackendDb db)
        {
            _db = db;
        }
  
      //私有的 
        private DbSet<T> _entity;
        protected DbSet<T> Entity
        {
            get
            {
                if (_entity == null)
                {
                    _entity = _db.Set<T>();
                }
                return _entity;
            }
        }


        //集成查询返回一个表数据
        public IQueryable<T> Table
        {
            get
            {
                //AsQueryable，将一个IEnumerable转化为IQueryable（对应的实现类型为EnumerableQuery）。
                return Entity.AsQueryable<T>();
            }
        }

       //删除
        public void Delete(object Id)
        {

            var t = Entity.Find(Id);

            if (t == null)
            {
                //抛出空值的提示
                // throw new System.ArgumentNullException(nameof(t));

            }

            //删除通过id查找的的记录
            _db.Remove(t);

            //把数据保存到数据库
            _db.SaveChanges();
        }


        public T GetById(object Id)
        {
            //通过查找id获取到对应的实体对象
            return Entity.Find(Id);
        }

      //添加
        public void Insert(T entity)
        {
            if (entity == null)
            {
                throw new System.ArgumentNullException();
            }

            //设置创建初始值
            entity.IsActived = true;
            entity.IsDeleted = false;
            entity.CreatedTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            entity.UpdatedTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            
            // 插入数据
            Entity.Add(entity);

            _db.SaveChanges();

        }

        //更新数据
        public void Update(T entity)
        {

            if (entity == null)
            {
                throw new System.ArgumentNullException();
            }

            entity.UpdatedTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            //将修改实体对象entity 更新到数据库
            Entity.Update(entity);

            _db.SaveChanges();
        }
         

        /// <summary>
        /// 根据实体异步插入到数据库中
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task InsertAsync(T entity)
        {
            if (entity==null)
            {
                throw new System.ArgumentNullException();
            }

            //设置创建初始值
            entity.IsActived = true;
            entity.IsDeleted = false;
            entity.CreatedTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            entity.UpdatedTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            await Entity.AddAsync(entity);
            await _db.SaveChangesAsync();
        }
    }

}