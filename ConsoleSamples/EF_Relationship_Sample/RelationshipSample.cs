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

			// Create a Student.
			Student s1 = new Student()
			{
				StudentName = "John",
				// Specify Grade in the Student.
				Grade = theGrades[0],
			};
			Context.Students.Add(s1);
			numChanged = Context.SaveChanges();
			Console.WriteLine($"Number of records changed: {numChanged}.");

			// Create a Student.
			Student s2 = new Student()
			{
				StudentName = "Neil",
			};
			// Specify Grade by adding Student to Grade's collection.
			theGrades[0].Students.Add(s2);

			// Create a StudentAddress.
			StudentAddress add2 = new StudentAddress()
			{
				Address1 = "2112 Main St",
				City = "Toronto",
				State = "ON",
				Country = "Canada",
			};
			s2.Address = add2;
			Context.Students.Add(s2);
			numChanged = Context.SaveChanges();
			Console.WriteLine($"Number of records changed: {numChanged}.");

			// Create a Student.
			Student s3 = new Student()
			{
				StudentName = "Geddy",
				Grade = theGrades[0],
			};
			Context.Students.Add(s3);
			// NOTE: We do not have to save here. We can wait until we
			// attach the address. For the sake of efficient, don't do it.
			//numChanged = Context.SaveChanges();
			//Console.WriteLine($"Number of records changed: {numChanged}.");

			// Create a StudentAddressFKAnnotation.
			StudentAddressFKAnnotation add3 = new StudentAddressFKAnnotation()
			{
				Address1 = "La Villa Strangiato",
				City = "Xanadu",
				State = "China",
				Country = "Cygnus X-1",
				ZipCode = 226868,
				// Just setting Student here is not enough, so don't bother.
				// It will be set automatically when added correctly.
				//Student = s3,
			};
			// NOTE: By setting the reference in the principal entity, the
			// properties in the dependent entity will automatically be
			// set to the proper values.
			s3.AnnotationAddress = add3;
			numChanged = Context.SaveChanges();
			Console.WriteLine($"Number of records changed: {numChanged}.");

			// Create a Student.
			Student s4 = new Student()
			{
				StudentName = "Alex",
				Grade = theGrades[0],
			};
			Context.Students.Add(s4);
			// NOTE: We do not have to save here. We can wait until we
			// attach the address. For the sake of efficient, don't do it.
			//numChanged = Context.SaveChanges();
			//Console.WriteLine($"Number of records changed: {numChanged}.");

			// Create a StudentAddressFKAnnotation.
			StudentAddressUseFluent add4 = new StudentAddressUseFluent()
			{
				Address1 = "123 Main St.",
				City = "Cityville",
				State = "Altered",
				Country = "And Western",
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

			Console.WriteLine();
			Console.WriteLine("==============");
			Console.WriteLine("=   AFTER    =");
			Console.WriteLine("==============");
			Console.WriteLine($"Number of records in the Students table: {Context.Students.ToList().Count}.");
			Console.WriteLine($"Number of records in the StudentAddresses table: {Context.StudentAddresses.Count()}.");
			Console.WriteLine($"Number of records in the StudentAddressFKAnnotations table: {Context.StudentAddressFKAnnotations.Count()}.");
			Console.WriteLine($"Number of records in the StudentAddressUseFluents table: {Context.StudentAddressUseFluents.Count()}.");
			Console.WriteLine($"Number of records in the Grades table: {Context.Grades.ToList().Count}.");

			List<Student> students = Context.Students
				.Include(s => s.Address)
				.Include(s => s.AnnotationAddress)
				.Include(s => s.FluentAddress)
				.ToList();
			foreach (Student s in students)
			{
				Console.WriteLine($"[{s.Id:D3}] Name: {s.StudentName}");
				Console.WriteLine($"Grade: {s.Grade.Name} - {s.Grade.Section}");
				if (s.Address == null)
					Console.WriteLine("  No conventional address on file.");
				else
				{
					Console.Write($"  [{s.Address.Id:D3}] Address: {s.Address.Address1} / {s.Address.Address2}, ");
					Console.Write($"{s.Address.City}, ");
					Console.Write($"{s.Address.State}, ");
					Console.Write($"{s.Address.Country}, ");
					Console.WriteLine($"{s.Address.ZipCode}");
				}
				if (s.AnnotationAddress == null)
					Console.WriteLine("  No annotation address on file.");
				else
				{
					Console.Write($"  [{s.AnnotationAddress.Id:D3}] Address: {s.AnnotationAddress.Address1} / {s.AnnotationAddress.Address2}, ");
					Console.Write($"{s.AnnotationAddress.City}, ");
					Console.Write($"{s.AnnotationAddress.State}, ");
					Console.Write($"{s.AnnotationAddress.Country}, ");
					Console.WriteLine($"{s.AnnotationAddress.ZipCode}");
				}
				if (s.FluentAddress == null)
					Console.WriteLine("  No Fluent address on file.");
				else
				{
					Console.Write($"  [{s.FluentAddress.Id:D3}] Address: {s.FluentAddress.Address1} / {s.FluentAddress.Address2}, ");
					Console.Write($"{s.FluentAddress.City}, ");
					Console.Write($"{s.FluentAddress.State}, ");
					Console.Write($"{s.FluentAddress.Country}, ");
					Console.WriteLine($"{s.FluentAddress.ZipCode}");
				}
			}

			Console.WriteLine();
			Console.WriteLine("This is the end of the sample.");
		}
	}
}
