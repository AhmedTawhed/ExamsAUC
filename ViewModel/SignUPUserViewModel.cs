using MVCExamProject.Models;
using System.ComponentModel.DataAnnotations;

namespace MVCExamProject.ViewModel
{
	public class SignUPUserViewModel
	{
		[MaxLength(25)]
		[MinLength(3)]
		[Required]
		public string Name { get; set; }
		public int? Age{get; set;}
		[Required]
		[EmailAddress]
		public string Email { get; set; }
		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }
		[MaxLength(40)]
		

		[DataType(DataType.Password)]
		[Compare("Password")]
		public string ConfirmPassword { get; set; }

		public bool IsAdmin { get; set; } = false;
		public List<UserExam>? UserExams { get; set; }
	}

}
