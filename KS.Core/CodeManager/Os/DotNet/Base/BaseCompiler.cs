using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using System.IO;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using KS.Core.GlobalVarioable;
using KS.Core.Model.Develop;
using System.Web;
using KS.Core.Exceptions;
using KS.Core.FileSystemProvide.Base;
using KS.Core.Localization;
using KS.Core.UI.Configuration;

namespace KS.Core.CodeManager.Os.DotNet.Base
{
    
    public abstract class BaseCompiler
    {
        protected IWebConfigManager WebConfigManager { get; set; }
        private readonly IFileSystemManager _fileSystemManager;
        private const string VisualBasic = "VisualBasic";
        private const string CSharp = "CSharp";
        private const string RoslynCompiler = "RoslynCompiler";
        private const string Temp = "TEMP";
        protected BaseCompiler(IWebConfigManager webConfigManager, IFileSystemManager fileSystemManager)
        {
            WebConfigManager = webConfigManager;
            _fileSystemManager = fileSystemManager;
        }
        public virtual void Compile(CompileOption compileOption, string path)
        {
            CompilerResults results;

           
            

            var parameters = new CompilerParameters();
            parameters.ReferencedAssemblies.AddRange(compileOption.LibraryRefrences);
            parameters.CompilerOptions = "/appconfig:\"" + (_fileSystemManager.RelativeToAbsolutePath("~/") + "web.config\"")
                .Replace("//","/");
            parameters.GenerateInMemory = false;
            parameters.GenerateExecutable = compileOption.GenerateExecutable;
            parameters.TreatWarningsAsErrors = false;
            parameters.OutputAssembly = path;
            //parameters.CoreAssemblyFileName = "System.dll";
            parameters.IncludeDebugInformation = compileOption.IncludeDebugInformation;

            parameters.TempFiles = HttpContext.Current.Request.IsLocal ?
                new TempFileCollection(Environment.GetEnvironmentVariable(Temp), true) 
                : new TempFileCollection(Environment.GetEnvironmentVariable(Temp),false);


            if (compileOption.CodeProvider == SourceType.Csharp)
            {
                if (Convert.ToBoolean(WebConfigManager.GetSettingByOption(RoslynCompiler).Value))
                {
                    //roslyn compiler
                    var cSharpCodeProvider = new Microsoft.CodeDom.Providers.DotNetCompilerPlatform.CSharpCodeProvider();

                    results = cSharpCodeProvider.CompileAssemblyFromSource(parameters, compileOption.Code);
                }
                else
                {


                    //old compiler
                    var cSharpCodeProvider = CodeDomProvider.CreateProvider(CSharp);

                    results = cSharpCodeProvider.CompileAssemblyFromSource(parameters, compileOption.Code);
                }
               
            }
            else
            {
                if (Convert.ToBoolean(WebConfigManager.GetSettingByOption(RoslynCompiler).Value))
                {

                    //roslyn compiler
                    var vBCodeProvider = new Microsoft.CodeDom.Providers.DotNetCompilerPlatform.VBCodeProvider();
                    results = vBCodeProvider.CompileAssemblyFromSource(parameters, compileOption.Code);
                }
                else
                {

                    //old compiler
                    var vBCodeProvider = CodeDomProvider.CreateProvider(VisualBasic);
                    results = vBCodeProvider.CompileAssemblyFromSource(parameters, compileOption.Code);
                }
              
            }

            if (results.Errors.HasErrors)
            {
                var warnings = new StringBuilder();
                var errors = new StringBuilder();
                foreach (CompilerError error in results.Errors)
                {
                    if(error.IsWarning)
                        warnings.AppendLine($"Warning ({error.ErrorNumber}): {error.ErrorText}  , Line : {error.Line} , Column : {error.Column} . <br>");
                    else
                    {
                        errors.AppendLine( $"Error ({error.ErrorNumber}): {error.ErrorText} , Line : {error.Line} , Column : {error.Column} . <br>");
                    }
                }

                throw new KhodkarInvalidException(errors.ToString() + warnings);
            }
        }


        public virtual string GenrateWcfCode(string xsdlUrl,string language)
        {
            //"/develop/code/os/dotnet/WebService/GetWcfGenreatedCode/" + wcfGuid
            Uri mexAddress = new Uri(xsdlUrl);
            MetadataExchangeClientMode mexMode = MetadataExchangeClientMode.HttpGet;

            // Get Metadata file from service
            MetadataExchangeClient mexClient = new MetadataExchangeClient(new EndpointAddress(mexAddress));
            mexClient.ResolveMetadataReferences = true;
            MetadataSet metaSet = mexClient.GetMetadata(mexAddress, mexMode);



            //Import all contracts and endpoints
            WsdlImporter importer = new WsdlImporter(metaSet);

            Collection<ContractDescription> contracts = importer.ImportAllContracts();
            ServiceEndpointCollection allEndpoints = importer.ImportAllEndpoints();

            //Generate type information for each contract
            ServiceContractGenerator generator = new ServiceContractGenerator();
           // var endpointsForContracts = new Dictionary<string, IEnumerable<ServiceEndpoint>>();

            foreach (ContractDescription contract in contracts)
            {
                generator.GenerateServiceContractType(contract);
                // Keep a list of each contract's endpoints
              //  endpointsForContracts[contract.Name] = allEndpoints.Where(se => se.Contract.Name == contract.Name).ToList();
            }

            if (generator.Errors.Count != 0)
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.WebServiceCodeGenratorError));

            // Generate a code file for the contracts 
            CodeGeneratorOptions options = new CodeGeneratorOptions();
            options.BracingStyle = "C";
            //CodeDomProvider codeDomProvider = CodeDomProvider.CreateProvider(language == "csharp" ? "C#": "VisualBasic");




            //      System.CodeDom.Compiler.IndentedTextWriter textWriter
            //= new System.CodeDom.Compiler.IndentedTextWriter(new System.IO.StreamWriter(outputFilePath));
            StringBuilder stringBuilder = new StringBuilder();
            StringWriter textWriter = new StringWriter(stringBuilder);



            if (language == "csharp")
            {


                if (Convert.ToBoolean(WebConfigManager.GetSettingByOption(RoslynCompiler).Value))
                {
                    //roslyn compiler
                    var cSharpCodeProvider = new Microsoft.CodeDom.Providers.DotNetCompilerPlatform.CSharpCodeProvider();


                    cSharpCodeProvider.GenerateCodeFromCompileUnit(
                        generator.TargetCompileUnit, textWriter, options
                    );
                }
                else
                {


                    //old compiler
                    var cSharpCodeProvider = CodeDomProvider.CreateProvider(CSharp);


                    cSharpCodeProvider.GenerateCodeFromCompileUnit(
                        generator.TargetCompileUnit, textWriter, options
                    );
                }


            }
            else
            {

                if (Convert.ToBoolean(WebConfigManager.GetSettingByOption(RoslynCompiler).Value))
                {

                    //roslyn compiler
                    var vBCodeProvider = new Microsoft.CodeDom.Providers.DotNetCompilerPlatform.VBCodeProvider();
                    vBCodeProvider.GenerateCodeFromCompileUnit(
                         generator.TargetCompileUnit, textWriter, options
                     );
                }
                else
                {

                    //old compiler
                    var vBCodeProvider = CodeDomProvider.CreateProvider(VisualBasic);
                    vBCodeProvider.GenerateCodeFromCompileUnit(
                        generator.TargetCompileUnit, textWriter, options
                    );
                }


            }

            textWriter.Close();

            return stringBuilder.ToString();



            // File.WriteAllText(outputFilePath, outputFile, Encoding.UTF8);


















            //// Compile the code file to an in-memory assembly
            //// Don't forget to add all WCF-related assemblies as references
            //CompilerParameters compilerParameters = new CompilerParameters(
            //    new string[] {
            //        "System.dll", "System.ServiceModel.dll",
            //        "System.Runtime.Serialization.dll" });
            //compilerParameters.GenerateInMemory = true;

            //CompilerResults results = codeDomProvider.CompileAssemblyFromDom(compilerParameters, generator.TargetCompileUnit);

            //if (results.Errors.Count > 0)
            //{
            //    throw new Exception("There were errors during generated code compilation");
            //}

            //// Find the proxy type that was generated for the specified contract
            //// (identified by a class that implements 
            //// the contract and ICommunicationbject)
            //Type clientProxyType = results.CompiledAssembly.GetTypes().First(t => t.IsClass && t.GetInterface(contractName)
            //!= null && t.GetInterface(typeof(ICommunicationObject).Name) != null);

            //// Get the first service endpoint for the contract
            //ServiceEndpoint serviceEndPoint = endpointsForContracts[contractName].First();
            //object instance = results.CompiledAssembly.CreateInstance(clientProxyType.Name,
            //    false, System.Reflection.BindingFlags.CreateInstance, null, new object[] { serviceEndPoint.Binding, serviceEndPoint.Address }, CultureInfo.CurrentCulture, null);
        }
    }
}
