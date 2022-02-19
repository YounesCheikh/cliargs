﻿using System;
namespace Cliargs
{
	static class CliArgsContainerExtensions
	{
		public static CliArg GetCliArgByName(this ICliArgsContainer container, string name)
        {
			var item= container.CliArgs.Values.SingleOrDefault(e=> e.Name == name);
			if (item == null)
				throw new CLIArgumentNotFoundException(name);
			return item;
		}

		public static CliArg GetCliArgByShortName(this ICliArgsContainer container, string shortName)
		{
			var item = container.CliArgs.Values.SingleOrDefault(e => e.Info.ShortName == shortName);
			if (item == null)
				throw new CLIArgumentNotFoundException(shortName);
			return item;
		}

		public static IEnumerable<CliArg> GetArgs(this ICliArgsContainer container)
        {
			return container.CliArgs.Values;
        }
	}
}
