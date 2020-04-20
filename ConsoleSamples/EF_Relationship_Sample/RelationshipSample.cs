using EFSamples.StudentModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleSamples.EF_Relationship_Sample
{
	public class RelationshipSample : IConsoleSample
	{
		public void Run()
		{
			Console.WriteLine("Welcome to the Entity Framework Relationship Sample.");
			Console.WriteLine();

			// Create our database context.
			using StudentModelContext Context = new StudentModelContext();
			Context.Database.Migrate();
			int numChanged = 0;

			Console.WriteLine("==============");
			Console.WriteLine("=   BEFORE   =");
			Console.WriteLine("==============");
			Console.WriteLine($"Number of records in the Students table: {Context.Students.ToList().Count}.");
			Console.WriteLine($"Number of records in the StudentAddresses table: {Context.StudentAddresses.Count()}.");
			Console.WriteLine($"Number of records in the StudentAddressFKAnnotations table: {Context.StudentAddressFKAnnotations.Count()}.");
			Console.WriteLine($"Number of records in the StudentAddressUseFluents table: {Context.StudentAddressUseFluents.Count()}.");
			Console.WriteLine($"Number of records in the Grades table: {Context.Grades.ToList().Count}.");

			List<Grade> theGrades = Context.Grades.ToList();
			Console.WriteLine($"Here are the existing grades");
			Console.WriteLine($"============================");
			foreach(Grade grade in theGrades)
			{
				Console.WriteLine($"[{grade.Id:D3}]  {grade.Name} - {grade.Section}");
			}

			Grade grade1 = new Grade
			{
				Name = Guid.NewGuid().ToString(),
				Section = Guid.NewGuid().ToString(),
			};
			// Note that EF Core is not yet aware of this object.

			// =================
			// Create a Student.
			// =================
			Student s1 = new Student()
			{
				StudentName = "John",
				// Specify Grade in the Student.
				Grade = theGrades[0],
			};
			Context.Students.Add(s1);
			numChanged = Context.SaveChanges();
			Console.WriteLine($"Number of records changed: {numChanged}.");

			// =================
			// Create a Student.
			// =================
			Student s2 = new Student()
			{
				StudentName = "Neil",
				// EF Core will see that this is a new Grade and
				// will automatically add it to the Grades table.
				Grade = grade1,
			};

			// Create a StudentAddress.
			StudentAddress add2 = new StudentAddress()
			{
				HomeAddress = new Address
				{
					Address1 = "2112 Main St",
					City = "Toronto",
					State = "ON",
					Country = "Canada",
				},
			};
			s2.Address = add2;
			Context.Students.Add(s2);
			numChanged = Context.SaveChanges();
			Console.WriteLine($"Number of records changed: {numChanged}.");

			// =================
			// Create a Student.
			// =================
			Student s3 = new Student()
			{
				StudentName = "Geddy",
				//Grade = grade1,
			};
			// Specify Grade by adding Student to Grade's collection.
			grade1.Students.Add(s3);

			Context.Students.Add(s3);

			// Create a StudentAddressFKAnnotation.
			StudentAddressFKAnnotation add3 = new StudentAddressFKAnnotation()
			{
				HomeAddress = new Address
				{
					Address1 = "La Villa Strangiato",
					City = "Xanadu",
					State = "China",
					Country = "Cygnus X-1",
					ZipCode = 226868,
				},
				// Specify the Student that "owns" this address.
				Student = s3,
			};
			// Tell the Context about this new address. We need to do this because
			// the Context has no other way of knowing about this object (which
			// is different from the previous example, where the Context can see
			// the new address via the new Student object it is INSERTing).
			// Note that the statement:
			//   Context.StudentAddressFKAnnotations.Add(add3);
			// will do the same thing.
			Context.Add<StudentAddressFKAnnotation>(add3);
			numChanged = Context.SaveChanges();
			Console.WriteLine($"Number of records changed: {numChanged}.");

			// =================
			// Create a Student.
			// =================
			Student s4 = new Student()
			{
				StudentName = "Alex",
				Grade = grade1,
			};
			Context.Add<Student>(s4);

			// Create a StudentAddressFKAnnotation.
			StudentAddressUseFluent add4 = new StudentAddressUseFluent()
			{
				HomeAddress = new Address
				{
					Address1 = "123 Main St.",
					City = "Cityville",
					State = "Altered",
					Country = "And Western",
				},
			};
			// NOTE: By setting the reference in the principal entity, the
			// properties in the dependent entity will automatically be
			// set to the proper values.
			s4.FluentAddress = add4;
			numChanged = Context.SaveChanges();
			Console.WriteLine($"Number of records changed: {numChanged}.");

#if false
			// Cannot persist a StudentAddress without a Student.
			// Create a StudentAddress.
			StudentAddress addConvAlone = new StudentAddress()
			{
				Address1 = "117 2nd Ave",
				City = "Nowheresville",
				State = "AA",
				Country = "Hyboria",
			};
			Context.StudentAddresses.Add(addConvAlone);
			numChanged = Context.SaveChanges();
			Console.WriteLine($"Number of records changed: {numChanged}.");

			// Cannot persist a StudentAddressFKAnnotation without a Student.
			// Create a StudentAddressFKAnnotation.
			StudentAddressFKAnnotation addAnnoAlone = new StudentAddressFKAnnotation()
			{
				Address1 = "131 2nd Ave",
				City = "Nowheresville",
				State = "AA",
				Country = "Hyboria",
			};
			Context.StudentAddressFKAnnotations.Add(addAnnoAlone);
			numChanged = Context.SaveChanges();
			Console.WriteLine($"Number of records changed: {numChanged}.");

			// Cannot persist a StudentAddressFKAnnotation without a Student.
			// Create a StudentAddressFKAnnotation.
			StudentAddressUseFluent addFluentAlone = new StudentAddressUseFluent()
			{
				Address1 = "131 2nd Ave",
				City = "Nowheresville",
				State = "AA",
				Country = "Hyboria",
			};
			Context.StudentAddressUseFluents.Add(addFluentAlone);
			numChanged = Context.SaveChanges();
			Console.WriteLine($"Number of records changed: {numChanged}.");
#endif

#if false
			// Cannot reuse a StudentAddress because it is the dependent entity
			// in a 1:1 relationship with Student.
			Student s3 = new Student()
			{
				StudentName = "Geddy",
				Address = add2,
			};
			Context.Students.Add(s3);
			// This will throw an exception because add2 is being used in
			// a second Student.
			numChanged = Context.SaveChanges();
			Console.WriteLine($"Number of records changed: {numChanged}.");
#endif
			// We are done with Context.
			Context.Dispose();
			// Use a new Context to make sure we are seeing what is in the database.
			using StudentModelContext ContextAfter = new StudentModelContext();

			Console.WriteLine();
			Console.WriteLine("==============");
			Console.WriteLine("=   AFTER    =");
			Console.WriteLine("==============");
			Console.WriteLine($"Number of records in the Students table: {ContextAfter.Students.ToList().Count}.");
			Console.WriteLine($"Number of records in the StudentAddresses table: {ContextAfter.StudentAddresses.Count()}.");
			Console.WriteLine($"Number of records in the StudentAddressFKAnnotations table: {ContextAfter.StudentAddressFKAnnotations.Count()}.");
			Console.WriteLine($"Number of records in the StudentAddressUseFluents table: {ContextAfter.StudentAddressUseFluents.Count()}.");
			Console.WriteLine($"Number of records in the Grades table: {ContextAfter.Grades.ToList().Count}.");

			theGrades = ContextAfter.Grades.ToList();
			Console.WriteLine($"Here are the grades");
			Console.WriteLine($"===================");
			foreach (Grade grade in theGrades)
			{
				Console.WriteLine($"[{grade.Id:D3}]  {grade.Name} - {grade.Section}");
				Console.WriteLine($"       Number of students in this grade: {grade.Students.Count}");
			}

			List<Student> students = ContextAfter.Students
				.Include(s => s.Address)
				.Include(s => s.AnnotationAddress)
				.Include(s => s.FluentAddress)
				.ToList();
			Console.WriteLine($"Here are the students");
			Console.WriteLine($"=====================");
			foreach (Student s in students)
			{
				Console.WriteLine($"[{s.Id:D3}] Name: {s.StudentName}");
				Console.WriteLine($"Grade: {s.Grade.Name} - {s.Grade.Section}");
				if (s.Address == null)
					Console.WriteLine("  No conventional address on file.");
				else
				{
					Console.Write($"  [{s.Address.Id:D3}] Address: {s.Address.HomeAddress.Address1} / {s.Address.HomeAddress.Address2}, ");
					Console.Write($"{s.Address.HomeAddress.City}, ");
					Console.Write($"{s.Address.HomeAddress.State}, ");
					Console.Write($"{s.Address.HomeAddress.Country}, ");
					Console.WriteLine($"{s.Address.HomeAddress.ZipCode}  --  for Student {s.Address.StudentId}:{s.Address.Student.Id}");
				}
				if (s.AnnotationAddress == null)
					Console.WriteLine("  No annotation address on file.");
				else
				{
					Console.Write($"  [{s.AnnotationAddress.Id:D3}] Address: {s.AnnotationAddress.HomeAddress.Address1} / {s.AnnotationAddress.HomeAddress.Address2}, ");
					Console.Write($"{s.AnnotationAddress.HomeAddress.City}, ");
					Console.Write($"{s.AnnotationAddress.HomeAddress.State}, ");
					Console.Write($"{s.AnnotationAddress.HomeAddress.Country}, ");
					Console.WriteLine($"{s.AnnotationAddress.HomeAddress.ZipCode}  --  for Student {s.AnnotationAddress.Id}:{s.AnnotationAddress.Student.Id}");
				}
				if (s.FluentAddress == null)
					Console.WriteLine("  No Fluent address on file.");
				else
				{
					Console.Write($"  [{s.FluentAddress.Id:D3}] Address: {s.FluentAddress.HomeAddress.Address1} / {s.FluentAddress.HomeAddress.Address2}, ");
					Console.Write($"{s.FluentAddress.HomeAddress.City}, ");
					Console.Write($"{s.FluentAddress.HomeAddress.State}, ");
					Console.Write($"{s.FluentAddress.HomeAddress.Country}, ");
					Console.WriteLine($"{s.FluentAddress.HomeAddress.ZipCode}  --  for Student { s.FluentAddress.StudentForeignKey}:{ s.FluentAddress.Student.Id}");
				}
			}

			Console.WriteLine();
			Console.WriteLine("This is the end of the sample.");
		}
	}
}
