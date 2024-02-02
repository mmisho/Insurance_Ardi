using Domain.CustomerManagement.Repository;
using Domain.Shared;
using MediatR;

namespace Application.CustomerManagement.Commands.Delete
{
    public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCustomerCommandHandler(
            ICustomerRepository customerRepository,
            IUnitOfWork unitOfWork)
        {
            _customerRepository = customerRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.OfIdAsync(request.CustomerId);

            if (customer == null)
            {
                throw new KeyNotFoundException($"{nameof(customer)} was not found for Id: {request.CustomerId}");
            }

            _customerRepository.Delete(customer);
            await _unitOfWork.SaveAsync();

            return Unit.Value;
        }
    }
}
