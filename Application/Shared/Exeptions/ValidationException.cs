#nullable disable

using FluentValidation.Results;

namespace Application.Shared.Exceptions
{
    [Serializable]
    public class ValidationException : Exception
    {
        public ValidationException()
        {
            Failures = new Dictionary<string, string[]>();
        }

        public ValidationException(IList<ValidationFailure> failures)
            : this()
        {
            var propertyNames = failures
                .Select(e => e.PropertyName)
                .Distinct();

            foreach (var propertyName in propertyNames)
            {
                var propertyFailures = failures
                    .Where(e => e.PropertyName == propertyName)
                    .Select(e => e.ErrorMessage)
                    .ToArray();

                Failures.Add(propertyName, propertyFailures);
            }
        }

        public IDictionary<string, string[]> Failures { get; }
    }
}
