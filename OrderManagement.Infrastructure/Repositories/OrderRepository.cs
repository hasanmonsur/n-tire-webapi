using Dapper;
using OrderManagement.Domain.Entities;
using OrderManagement.Domain.Repositories;
using OrderManagement.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DapperContext _dapperContext;

        public OrderRepository(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }


        public async Task<Order> GetOrderByIdAsync(int id)
        {
            var query = "SELECT * FROM Orders WHERE Id = @Id";
            using (var connection = _dapperContext.CreateConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<Order>(query, new { Id = id });
            }
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            var query = "SELECT * FROM Orders";
            using (var connection = _dapperContext.CreateConnection())
            {
                return await connection.QueryAsync<Order>(query);
            }
        }

        public async Task AddOrderAsync(Order order)
        {
            var query = "INSERT INTO Orders (CustomerName, TotalAmount, OrderDate) VALUES (@CustomerName, @TotalAmount, @OrderDate)";
            using (var connection = _dapperContext.CreateConnection())
            {
                await connection.ExecuteAsync(query, new
                {
                    order.CustomerName,
                    order.TotalAmount,
                    order.OrderDate
                });
            }
        }

        public async Task UpdateOrderAsync(Order order)
        {
            var query = "UPDATE Orders SET CustomerName = @CustomerName, TotalAmount = @TotalAmount WHERE Id = @Id";
            using (var connection = _dapperContext.CreateConnection())
            {
                await connection.ExecuteAsync(query, new
                {
                    order.Id,
                    order.CustomerName,
                    order.TotalAmount
                });
            }
        }

        public async Task DeleteOrderAsync(int id)
        {
            var query = "DELETE FROM Orders WHERE Id = @Id";
            using (var connection = _dapperContext.CreateConnection())
            {
                await connection.ExecuteAsync(query, new { Id = id });
            }
        }
    }
}
