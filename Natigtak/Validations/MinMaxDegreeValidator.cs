using FluentValidation;
using Natigtak.Models;

namespace Natigtak.Validations
{
	public class MinMaxDegreeValidator : AbstractValidator<MinMaxDegree>
	{

		public MinMaxDegreeValidator()
		{
			RuleFor(a => a.Min).NotEmpty().WithMessage("من فضلك أدخل الحد الأدني!!")
					.InclusiveBetween(0, 410).WithMessage("من فضلك أدخل رقم من 0 الي 410")
					;





			RuleFor(a => a.Max).NotEmpty().WithMessage("من فضلك أدخل الحد الأقصي!!")
					.GreaterThan(50).WithMessage("ya raaaaaab")
                 .InclusiveBetween(0, 410).WithMessage("من فضلك أدخل رقم من 0 الي 410");
		}
	}
}
