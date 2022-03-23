using Movie_database.App.Abstract;
using Movie_database.Domain.Common;

namespace Movie_database.App.Common
{
    public class BaseService<T> : IService<T> where T : BaseEntity
    {
        public List<T> Items { get; set; }

        public BaseService()
        {
            Items = new List<T>();
        }

        public int GetLastId()
        {
            if (Items.Any())
            {
                return Items.OrderBy(x => x.Id).LastOrDefault().Id;
            }
            else
                return 0;
        }

        public int Add(T item)
        {
            Items.Add(item);
            return item.Id;
        }

        public List<T> GetAll()
        {
            return Items;
        }

        public virtual void Remove(T item)
        {
            Items.Remove(item);
        }

        public int Update(T item)
        {
            var entity = Items.FirstOrDefault(p => p.Id == item.Id);
            if(entity != null)
            {
                entity = item;
            }
            return entity.Id;
        }
    }
}
