﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cliargs.Tests
{
	[TestClass]
	public class CliArgsContainerTests
	{
		[TestMethod]
		public void DefaultFormatAppliedTest()
		{
			CliArgsContainer container = new CliArgsContainer();
			Assert.IsNotNull(container.Format);
			Assert.AreEqual(CliArgsFormat.Default.AssignationChar, container.Format.AssignationChar);
			Assert.AreEqual(CliArgsFormat.Default.LongNamePrefix, container.Format.LongNamePrefix);
			Assert.AreEqual(CliArgsFormat.Default.ShortNamePrefix, container.Format.ShortNamePrefix);
			Assert.AreEqual(CliArgsFormat.Default.GetHashCode(), container.Format.GetHashCode());
		}

		[TestMethod]
		public void RegisterArgTest()
        {
			CliArgsContainer container = new CliArgsContainer();
            CliArg arg = CliArg.New<int>("test");
            container.Register(arg);
			Assert.IsTrue(container.CliArgs.ContainsKey("test"));
        }

		[TestMethod]
		public void GetValueFromExistingArgTest()
		{
			CliArgsContainer container = new CliArgsContainer();
			CliArg arg = CliArg.New<int>("test");
			arg.InputValue = "1";
			arg.Validate();
			container.Register(arg);
			var value = container.GetValue<int>("test");
			Assert.AreEqual(1, value);
		}

		[TestMethod]
		[ExpectedException(typeof(CliArgsException))]
		public void GetValueFromExistingArgWithWrongTypeTest()
		{
			CliArgsContainer container = new CliArgsContainer();
			CliArg arg = CliArg.New<int>("test");
			arg.InputValue = "1";
			arg.Validate();
			container.Register(arg);
			var _ = container.GetValue<string>("test");
		}

		[TestMethod]
		[ExpectedException(typeof(CLIArgumentNotFoundException))]
		public void GetValueFromNonExistingArgTypeTest()
		{
            try
            {
				CliArgsContainer container = new CliArgsContainer();
				var _ = container.GetValue<int>("test");
			}
            catch (CLIArgumentNotFoundException ex)
            {
				Assert.AreEqual("test", ex.ArgumentName);
				throw;
            }
		}

		[TestMethod]
		[ExpectedException(typeof(CLIArgumentNotFoundException))]
		public void GetValueFromNonExistingArgNonGenericTypeTest()
		{
			try
			{
				CliArgsContainer container = new CliArgsContainer();
				_ = container.GetValue("test");
			}
			catch (CLIArgumentNotFoundException ex)
			{
				Assert.AreEqual("test", ex.ArgumentName);
				throw;
			}
		}

		[TestMethod, ExpectedException(typeof(ArgumentNullException))]
		public void ArgumentNullExceptionThrownIfNoFormat()
        {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            _ = new CliArgsContainer(null);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }

		[TestMethod]
		[ExpectedException(typeof(CliArgsException))]
		public void ArgRegistrationFailsOnShortnameAlreadyExist() {
			ICliArgsContainer container = new CliArgsContainer();
			container.Register(CliArg.New("First").WithShortName("f"));
			container.Register(CliArg.New("Second").WithShortName("f"));
		}

		[TestMethod]
		[ExpectedException(typeof(CliArgsException))]
		public void ArgRegistrationFailsOnLongnameAlreadyExist() {
			ICliArgsContainer container = new CliArgsContainer();
			container.Register(CliArg.New("First").WithLongName("first"));
			container.Register(CliArg.New("Second").WithLongName("first"));
		}

		[TestMethod]
		[ExpectedException(typeof(CliArgsException))]
		public void ArgRegistrationFailsOnNameAlreadyExist() {
			ICliArgsContainer container = new CliArgsContainer();
			container.Register(CliArg.New("First").WithLongName("first").WithShortName("f"));
			container.Register(CliArg.New("First").WithLongName("second").WithShortName("s"));
		}
	}
}

