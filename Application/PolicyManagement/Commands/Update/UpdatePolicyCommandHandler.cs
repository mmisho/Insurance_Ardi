using Domain.CustomerManagement.Repository;
using Domain.PolicyManagement.Repository;
using Domain.ProductManagement.Repository;
using Domain.Shared;
using MediatR;

namespace Application.PolicyManagement.Commands.Update
{
    public class UpdatePolicyCommandHandler : IRequestHandler<UpdatePolicyCommand>
    {
        private readonly IPolicyRepository _policyRepository;
        private readonly IProductRepository _productRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdatePolicyCommandHandler(
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

        public async Task<Unit> Handle(UpdatePolicyCommand request, CancellationToken cancellationToken)
        {
            var policy = await _policyRepository.OfIdAsync(request.Id);

            if (policy == null)
            {
                throw new KeyNotFoundException($"{nameof(policy)} was not found for Id: {request.Id}");
            }

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

            policy.ChangeDetails(request.Premium, customer, product, request.Status, request.StartDate.ToUniversalTime(), request.StartDate.ToUniversalTime());

            _policyRepository.Update(policy);
            await _unitOfWork.SaveAsync();

            return Unit.Value;
        }
    }
}
