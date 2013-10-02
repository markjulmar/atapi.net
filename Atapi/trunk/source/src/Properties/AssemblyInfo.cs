using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("Atapi")]
[assembly: AssemblyDescription(".NET Assembly to interact with Telephony system")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("JulMar Technology, Inc.")]
[assembly: AssemblyProduct("Atapi")]
[assembly: AssemblyCopyright("Copyright © JulMar Technology, Inc. 2008-2013")]
[assembly: AssemblyTrademark("ATAPI")]
[assembly: AssemblyCulture("")]
[assembly: CLSCompliant(true)]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("4fe5e41e-a00d-45a6-b6a5-d4f5a294b63e")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Revision and Build Numbers 
// by using the '*' as shown below:
[assembly: AssemblyVersion("1.4.1.0")]
[assembly: AssemblyFileVersion("1.4.1.0")]

// Avoid TypeLoad exceptions under .NET4
[assembly: SecurityRules(SecurityRuleSet.Level1)]
