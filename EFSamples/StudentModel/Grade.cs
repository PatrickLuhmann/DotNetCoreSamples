using System;
using System.Collections.Generic;
using System.Text;

namespace EFSamples.StudentModel
{
	// Based on tutorial: https://www.entityframeworktutorial.net/efcore/entity-framework-core.aspx
	public class Grade
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Section { get; set; }
		public List<Student> Students { get; set; } = new List<Student>();
	}
}
