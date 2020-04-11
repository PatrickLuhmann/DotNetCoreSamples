using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EFSamples.StudentModel
{
	// Based on tutorial: https://www.entityframeworktutorial.net/efcore/entity-framework-core.aspx

	// This is the basic implementation of a dependent entity, with both a
	// reference property and a foreign key property to the dependent entity.
	public class StudentAddress
	{
		public int Id { get; set; }
		public string Address1 { get; set; }
		public string Address2 { get; set; }
		public string City { get; set; }
		public string State { get; set; }
		public string Country { get; set; }
		public int ZipCode { get; set; }
		public int StudentId { get; set; }
		public Student Student { get; set; }
	}

	// This implementation shows how to use a data annotation to specify
	// the foreign key property.
	public class StudentAddressFKAnnotation
	{
		// Explicitly set this as the FK to create one-to-one relationship with Student.
		[ForeignKey("Student")]
		public int Id { get; set; }
		public string Address1 { get; set; }
		public string Address2 { get; set; }
		public string City { get; set; }
		public string State { get; set; }
		public string Country { get; set; }
		public int ZipCode { get; set; }
		// Instead of using the convention for the foreign key, define it with a data annotation (above).
		//public int StudentId { get; set; }
		public Student Student { get; set; }
	}

	public class StudentAddressUseFluent
	{
		public int Id { get; set; }
		public string Address1 { get; set; }
		public string Address2 { get; set; }
		public string City { get; set; }
		public string State { get; set; }
		public string Country { get; set; }
		public int ZipCode { get; set; }
		// This name does not follow the convention so we must
		// explicitly define it as the foreign key via Fluent.
		public int StudentForeignKey { get; set; }
		public Student Student { get; set; }
	}
}
