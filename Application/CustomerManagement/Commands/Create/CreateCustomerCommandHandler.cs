using Domain.CustomerManagement;
using Domain.CustomerManagement.Repository;
using Domain.Shared;
using MediatR;

namespace Application.CustomerManagement.Commands.Create
{
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateCustomerCommandHandler(
            ICustomerRepository customerRepository,
            IUnitOfWork unitOfWork)
        {
            _customerRepository = customerRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = new Customer(request.FirstName, request.LastName, request.Email, request.PersonalNumber, request.DateOfBirth.ToUniversalTime(), request.Gender);

            await _customerRepository.InsertAsync(customer);
            await _unitOfWork.SaveAsync();

            return Unit.Value;
        }
    }
}
