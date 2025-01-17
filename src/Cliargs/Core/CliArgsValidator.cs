﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Cliargs
{
	static class CliArgsValidator 
	{
        internal static IList<ICliArgsValidationResult> Validate(ICliArgsContainer container)
        {
            if (container == null)
                throw new ArgumentNullException(nameof(container));

            var containerInstance = container;
            var args = containerInstance.GetArgs().ToList();
            foreach (var arg in args)
            {
                if (!arg.Info.RequiresValue)
                    continue;
                var results = arg.Validate();
                if (!results.Any())
                    continue;

                return results.ToList();
            }

            return new List<ICliArgsValidationResult>();
        }

        
    }
}

