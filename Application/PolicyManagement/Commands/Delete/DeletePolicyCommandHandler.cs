using Domain.PolicyManagement.Repository;
using Domain.Shared;
using MediatR;

namespace Application.PolicyManagement.Commands.Delete
{
    public class DeletePolicyCommandHandler : IRequestHandler<DeletePolicyCommand>
    {
        private readonly IPolicyRepository _policyRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeletePolicyCommandHandler(
            IPolicyRepository policyRepository,
            IUnitOfWork unitOfWork)
        {
            _policyRepository = policyRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(DeletePolicyCommand request, CancellationToken cancellationToken)
        {
            var policy = await _policyRepository.OfIdAsync(request.Id);

            if (policy == null)
            {
                throw new KeyNotFoundException($"{nameof(policy)} was not found for Id: {request.Id}");
            }

            _policyRepository.Delete(policy);
            await _unitOfWork.SaveAsync();

            return Unit.Value;
        }
    }
}
