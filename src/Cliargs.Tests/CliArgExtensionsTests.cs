﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cliargs.Tests
{
	[TestClass]
	public class CliArgExtensionsTests
	{
        class ZeroValidationRule : CliArgsValidationRule<int>
        {
            public override string GetValidationError()
            {
				return "Value must be zero";
            }

            public override bool IsValid(int value)
            {
				return value == 0;
            }
        }

        [TestMethod]
		public void CreateSampleInstanceWithDefaultInfo()
        {
			const string argName = "test";
			var arg = CliArg.New<int>(argName);
			Assert.IsNotNull(arg);
			Assert.IsNotNull(arg.Info);
			Assert.AreEqual(argName, arg.Name);
			Assert.IsFalse(arg.Info.Optional);
			Assert.AreEqual(string.Empty, arg.Info.ShortName);
			Assert.AreEqual(string.Empty, arg.Info.Description);
			Assert.AreEqual(string.Empty, arg.Info.Usage);
		}

		[TestMethod]
		public void CreateSampleInstanceWithCustomConverter()
		{
			const string argName = "test";
			var arg = CliArg.New<int>(argName).ValueConvertedWith(new StringToEnumConverter());
			Assert.IsNotNull(arg.Converter);
			Assert.AreEqual(typeof(StringToEnumConverter), arg.Converter.GetType());
		}

		[TestMethod]
		public void CreateSampleInstanceWithDefaultInfoAsNoValueRequired()
		{
			const string argName = "test";
			var arg = CliArg.New(argName);
			Assert.IsNotNull(arg);
			Assert.IsNotNull(arg.Info);
			Assert.AreEqual(argName, arg.Name);
			Assert.IsFalse(arg.Info.RequiresValue);
		}



		[TestMethod]
		public void CreateSampleInstanceWithShortname()
		{
			const string argName = "test";
			const string shortName = "t";
			var arg = CliArg.New<int>(argName).WithShortName(shortName);
			Assert.IsNotNull(arg);
			Assert.IsNotNull(arg.Info);
			Assert.AreEqual(shortName, arg.Info.ShortName);
		}

		[TestMethod]
		public void CreateSampleInstanceWithShortnameAndNoValueRequired()
		{
			const string argName = "test";
			const string shortName = "t";
			var arg = CliArg.New(argName).WithShortName(shortName);
			Assert.IsNotNull(arg);
			Assert.IsNotNull(arg.Info);
			Assert.AreEqual(shortName, arg.Info.ShortName);
		}

		[TestMethod]
		public void CreateSampleInstanceWithDescription()
		{
			const string argName = "test";
			const string description = "sample description";
			var arg = CliArg.New<int>(argName).WithDescription(description);
			Assert.IsNotNull(arg);
			Assert.IsNotNull(arg.Info);
			Assert.AreEqual(description, arg.Info.Description);
		}

		[TestMethod]
		public void CreateSampleInstanceWithDescriptionAsNoValueRequired()
		{
			const string argName = "test";
			const string description = "sample description";
			var arg = CliArg.New(argName).WithDescription(description);
			Assert.IsNotNull(arg);
			Assert.IsNotNull(arg.Info);
			Assert.AreEqual(description, arg.Info.Description);
		}

		[TestMethod]
		public void CreateSampleInstanceWithUsage()
		{
			const string argName = "test";
			const string usage = "sample usage";
			var arg = CliArg.New<int>(argName).WithUsage(usage);
			Assert.IsNotNull(arg);
			Assert.IsNotNull(arg.Info);
			Assert.AreEqual(usage, arg.Info.Usage);
		}

		[TestMethod]
		public void CreateSampleInstanceWithUsageAsNoValueRequired()
		{
			const string argName = "test";
			const string usage = "sample usage";
			var arg = CliArg.New(argName).WithUsage(usage);
			Assert.IsNotNull(arg);
			Assert.IsNotNull(arg.Info);
			Assert.AreEqual(usage, arg.Info.Usage);
		}

		[TestMethod]
		public void CreateSampleInstanceAsRequired()
		{
			const string argName = "test";
			const bool optional = false;
			var arg = CliArg.New<int>(argName).AsRequired();
			Assert.IsNotNull(arg);
			Assert.IsNotNull(arg.Info);
			Assert.AreEqual(optional, arg.Info.Optional);
		}

		[TestMethod]
		public void CreateSampleInstanceAsOptional()
		{
			const string argName = "test";
			const bool optional = true;
			var arg = CliArg.New<int>(argName).AsOptional();
			Assert.IsNotNull(arg);
			Assert.IsNotNull(arg.Info);
			Assert.AreEqual(optional, arg.Info.Optional);
		}

		[TestMethod]
		public void CreateSampleInstanceAsOptionalWithDefaultValue()
		{
			const string argName = "test";
			const bool optional = true;
			var arg = CliArg.New<int>(argName).AsOptional(6);
			Assert.IsNotNull(arg);
			Assert.IsNotNull(arg.Info);
			Assert.AreEqual(optional, arg.Info.Optional);
			Assert.AreEqual(6, arg.DefaultValue);
		}

		[TestMethod]
		public void CreateSampleInstanceAsOptionalNoValueRequired()
		{
			const string argName = "test";
			const bool optional = true;
			var arg = CliArg.New(argName).AsOptional();
			Assert.IsNotNull(arg);
			Assert.IsNotNull(arg.Info);
			Assert.AreEqual(optional, arg.Info.Optional);
		}

		[TestMethod]
		public void CreateSampleInstanceWithValidationRule()
		{
			const string argName = "test";
			ZeroValidationRule rule = new ZeroValidationRule();
			var arg = CliArg.New<int>(argName).ValidatedWithRule(rule);
			Assert.IsNotNull(arg);
			Assert.IsTrue(arg.ValidationRules.Any());
			Assert.AreEqual(1, arg.ValidationRules.Count);
		}

		[TestMethod]
		public void CreateSampleInstanceWithValidationRules()
		{
			const string argName = "test";
			ZeroValidationRule rule = new ZeroValidationRule();
			var arg = CliArg.New<int>(argName).ValidatedWithRules(new [] { rule });
			Assert.IsNotNull(arg);
			Assert.IsTrue(arg.ValidationRules.Any());
			Assert.AreEqual(1, arg.ValidationRules.Count);
		}

		[TestMethod]
		public void CreateSampleInstanceWithValidationRulesCollection()
		{
			const string argName = "test";
			ZeroValidationRule rule = new ZeroValidationRule();
			var arg = CliArg.New<int>(argName).ValidatedWithRules(new List<ICliArgsValidationRule<int>>(){ rule });
			Assert.IsNotNull(arg);
			Assert.IsTrue(arg.ValidationRules.Any());
			Assert.AreEqual(1, arg.ValidationRules.Count);
		}
	}
}

