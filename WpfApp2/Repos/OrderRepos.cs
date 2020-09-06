using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WpfApp2.Entity;

namespace WpfApp2.Repos
{
    public class OrderRepos : EFGenericRepository<Order_entity> 
    {
        public OrderRepos(DbContext context) : base(context)
        {
        }
        
        public List<Order_entity> GetByRoomId(int RoomId)
        {
            return _dbSet.AsNoTracking().Where( x => x.RoomsId == RoomId).ToList();
        }
        
        
        public List<Order_entity> getByClientId(int clientId)
        {
            return _dbSet.AsNoTracking().Where( x => x.ClientsId == clientId).ToList();
        }
        
        
    }
}