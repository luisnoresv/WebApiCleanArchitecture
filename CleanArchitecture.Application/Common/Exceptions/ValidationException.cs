using System;
using System.Collections.Generic;
using System.Linq;
using CleanArchitecture.Application.Common.Constants;
using FluentValidation.Results;

namespace CleanArchitecture.Application.Common.Exceptions
{
  public class ValidationException : Exception
  {
    public IDictionary<string, string[]> Errors { get; }

    public ValidationException() : base(GlobalConstants.DEFAULT_VALIDATION_MESSAGE)
    {
      Errors = new Dictionary<string, string[]>();
    }

    public ValidationException(IEnumerable<ValidationFailure> failures)
          : this()
    {
      var failureGroups = failures
          .GroupBy(e => e.PropertyName, e => e.ErrorMessage);

      foreach (var failureGroup in failureGroups)
      {
        var propertyName = failureGroup.Key;
        var propertyFailures = failureGroup.ToArray();

        Errors.Add(propertyName, propertyFailures);
      }
    }
  }
}