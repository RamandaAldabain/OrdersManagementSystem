//using Microsoft.EntityFrameworkCore;
//using System.ComponentModel.DataAnnotations;

//public class UniqueValueAttribute : ValidationAttribute
//{
//	private readonly string _propertyName;

//	public UniqueValueAttribute(string propertyName)
//	{
//		_propertyName = propertyName;
//	}

//	protected override ValidationResult IsValid(object value, ValidationContext validationContext)
//	{
//		var dbContext = (DbContext)validationContext.GetService(typeof(DbContext));
//		var property = validationContext.ObjectType.GetProperty(_propertyName);
//		var propertyValue = property.GetValue(validationContext.ObjectInstance);

//		var existingValue = dbContext.Set(validationContext.ObjectType)
//			.AsQueryable()
//			.Any(e => e != validationContext.ObjectInstance && property.GetValue(e).Equals(propertyValue));

//		if (existingValue)
//		{
//			return new ValidationResult(ErrorMessage ?? $"{_propertyName} must be unique.");
//		}

//		return ValidationResult.Success;
//	}
//}
