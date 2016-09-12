# Bag O'Utils

This project is a collection of .NET projects that provide a set of utilities. The utilities are broken
up into small, discreet projects, each with a NuGet package. This allows for targeted inclusion of just
the necessary utilities in a project. Tools such as ILMerge or [LibZ](https://github.com/MiloszKrajewski/LibZ)
can then be used to reduce the number of _dlls_ that need to be deployed.

## Overview of Utilities

The following utilities are provided. See the _readme_ under each project for more information.

Project | NuGet Name       | Purpose
--------|------------------|---------------------------------------------------------------
Guards  | BagOUtils-Guards | Checks arguments are valid and if not, throws exceptions.

