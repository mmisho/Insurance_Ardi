using Domain.CustomerManagement.Repository;
using Domain.PolicyManagement;
using Domain.PolicyManagement.Repository;
using Domain.ProductManagement.Repository;
using Domain.Shared;
using MediatR;

namespace Application.PolicyManagement.Commands.Create
{
    public class CreatePolicyCommandHandler : IRequestHandler<CreatePolicyCommand>
    {
        private readonly IPolicyRepository _policyRepository;
        private readonly IProductRepository _productRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreatePolicyCommandHandler(
            IPolicyRepository policyRepository,
            IProductRepository productRepository,
            ICustomerRepository customerRepository,
            IUnitOfWork unitOfWork)
        {
            _policyRepository = policyRepository;
            _productRepository = productRepository;
            _customerRepository = customerRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(CreatePolicyCommand request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.OfIdAsync(request.CustomerId);

            if (customer == null)
            {
                throw new KeyNotFoundException($"{nameof(customer)} was not found for Id: {request.CustomerId}");
            }

            var product = await _productRepository.OfIdAsync(request.ProductId);

            if (product == null)
            {
                throw new KeyNotFoundException($"{nameof(product)} was not found for Id: {request.ProductId}");
            }

            var policy = new Policy(request.Premium, customer, product, request.StartDate.ToUniversalTime(), request.EndDate.ToUniversalTime());

            await _policyRepository.InsertAsync(policy);
            await _unitOfWork.SaveAsync();

            return Unit.Value;
        }
    }
}
