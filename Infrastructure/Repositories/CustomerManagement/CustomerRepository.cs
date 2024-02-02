using Dapper;
using Domain.CustomerManagement;
using Domain.CustomerManagement.Repository;
using Domain.PolicyManagement;
using Infrastructure.DataAccess;
using Domain.ProductManagement;

namespace Infrastructure.Repositories.CustomerManagement
{
    public class CustomerRepository : BaseRepository<EFDbContext, DapperDbContext, Customer>, ICustomerRepository
    {
        private const string ConstQuery = @" SELECT Customers.*, Policies.*,  Products.* 
                                              FROM 
                                              Customers
                                              LEFT JOIN
                                              Policies ON Customers.Id = Policies.CustomerId
                                              LEFT JOIN 
                                              Products ON Policies.ProductId = Products.Id
                                              WHERE 1=1";

        public CustomerRepository(EFDbContext eFDbContext, DapperDbContext dapperDbContext) : base(eFDbContext, dapperDbContext)
        {
        }

        public new async Task<IEnumerable<Customer>> GetAllAsync()
        {
            var query = ConstQuery;

            using (var connection = _dapperContext.CreateConnection())
            {
                var result = new List<Customer>();

                var queryResult = await connection.QueryAsync<Customer, Policy, Product, Customer>(
                    query,
                    (customer, policy, product) =>
                    {
                        var existingCustomer = result.FirstOrDefault(c => c.Id == customer.Id);

                        if (existingCustomer == null)
                        {
                            existingCustomer = customer;
                            existingCustomer.Policies = new List<Policy>();
                            result.Add(existingCustomer);
                        }

                        if (policy != null)
                        {
                            policy.Product = product;
                            existingCustomer.Policies?.Add(policy);
                        }

                        return existingCustomer;
                    },
                    splitOn: "Id,Id"
                );

                result = queryResult.Distinct().ToList();

                return result;
            }
        }

        /// აქ არ ვიყენებ მეთოდ ჰაიდინგს, პოლისის შექმის დროს უბრალოდ გასაჩეკად არსებობს თუ არა ქასთუმერი ბეიზ რეპოზიტორის OfIdAsync მეთოდს ვიყენებ
        public async Task<Customer?> OfIdWithNavAsync(Guid id)
        {
            using (var connection = _dapperContext.CreateConnection())
            {
                var query = ConstQuery;
                query += " AND Customers.Id = @Id";


                var result = await connection.QueryAsync<Customer, Policy, Product, Customer>(
                    query,
                    (customer, policy, product) =>
                    {
                        if (policy != null)
                        {
                            customer.Policies = new List<Policy> { policy };
                            policy.Product = product;
                        }

                        return customer;
                    },
                    new { Id = id },
                    splitOn: "Id,Id"
                );

                return result.FirstOrDefault();
            }
        }
    }
}


