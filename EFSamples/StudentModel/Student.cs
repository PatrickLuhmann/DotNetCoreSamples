using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EFSamples.StudentModel
{
	// Based on tutorial: https://www.entityframeworktutorial.net/efcore/entity-framework-core.aspx
	public class Student
	{
		public int Id { get; set; }
		public string StudentName { get; set; }
		public StudentAddress Address { get; set; }
		public StudentAddressFKAnnotation AnnotationAddress { get; set; }
		public StudentAddressUseFluent FluentAddress { get; set; }
	}
}
