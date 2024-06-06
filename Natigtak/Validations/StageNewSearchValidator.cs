using FluentValidation;
using Natigtak.Models;

namespace Natigtak.Validations
{
	public class StageNewSearchValidator : AbstractValidator<StageNewSearch>
	{
		public StageNewSearchValidator()
		{
			RuleFor(s => s.SeatingNo).NotEmpty().WithMessage("من فضلك أدخل رقم الجلوس!!");
		}

	}
}
