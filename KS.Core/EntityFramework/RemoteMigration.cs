using System;
using System.ServiceModel;
using KS.Core.DevelopServer;
using KS.Core.EntityFramework.Base;
using KS.Core.Exceptions;
using MigrationCode = KS.Core.Model.Develop.MigrationCode;
using MigrationInfo = KS.Core.Model.Develop.MigrationInfo;

namespace KS.Core.EntityFramework
{
    public class RemoteMigration : BaseMigration, IMigration
    {
        public override MigrationCode GenerateMigration(MigrationInfo migrationInfo)
        {
            var remoteMigration = CreateWcfInterface();

            try
            {
                var migrationCode = remoteMigration.GenerateMigration(new KS.Core.DevelopServer.MigrationInfo()
                {
                    ContextAssemblyName = migrationInfo.ContextAssemblyName,
                    TargetName = migrationInfo.TargetName,
                    Connection = migrationInfo.Connection,
                    Language = migrationInfo.Language == GlobalVarioable.SourceType.Csharp
                  ? CSharp
                  : VisualBasic,
                    AppDataPath = migrationInfo.AppDataPath,
                    ConfigurationTypeName = migrationInfo.ConfigurationTypeName,
                    ContextAssemblyPath = migrationInfo.ContextAssemblyPath,
                    ContextAssemblyRootNameSpace = migrationInfo.ContextAssemblyRootNameSpace,
                    Force = migrationInfo.Force,
                    NameSpaceQualifiedConnectionType = migrationInfo.NameSpaceQualifiedConnectionType,
                    SourceName = migrationInfo.SourceName,
                    WebConfigPath = migrationInfo.WebConfigPath
                });
                return new MigrationCode()
                {
                    UserCode = migrationCode.UserCode,
                    DesignerCode = migrationCode.DesignerCode,
                    Infos = migrationCode.Infos,
                    MigrationId = migrationCode.MigrationId,
                    Resources = migrationCode.Resources,
                    Verbose = migrationCode.Verbose,
                    Warnings = migrationCode.Warnings
                };
            }
            catch (FaultException<DevelopServiceException> ex)
            {
                throw new KhodkarInvalidException(ex.Detail.ErrorDetails);
            }
            catch (Exception ex)
            {
                throw new DevelopServerException(ex.Message);
            }
        }

        public override void RunMigration(MigrationInfo migrationInfo)
        {
            var remoteMigration = CreateWcfInterface();
            try
            {
                remoteMigration.RunMigration(new KS.Core.DevelopServer.MigrationInfo()
                {
                    ContextAssemblyName = migrationInfo.ContextAssemblyName,
                    TargetName = migrationInfo.TargetName,
                    Connection = migrationInfo.Connection,
                    Language = migrationInfo.Language == GlobalVarioable.SourceType.Csharp
                          ? CSharp
                          : VisualBasic,
                    AppDataPath = migrationInfo.AppDataPath,
                    ConfigurationTypeName = migrationInfo.ConfigurationTypeName,
                    ContextAssemblyPath = migrationInfo.ContextAssemblyPath,
                    ContextAssemblyRootNameSpace = migrationInfo.ContextAssemblyRootNameSpace,
                    Force = migrationInfo.Force,
                    NameSpaceQualifiedConnectionType = migrationInfo.NameSpaceQualifiedConnectionType,
                    SourceName = migrationInfo.SourceName,
                    WebConfigPath = migrationInfo.WebConfigPath
                });
            }
            catch (FaultException<DevelopServiceException> ex)
            {
                throw new KhodkarInvalidException(ex.Detail.ErrorDetails);
            }
            catch (Exception ex)
            {
                throw new DevelopServerException(ex.Message);
            }
        }


        public override string GetMigrationScript(MigrationInfo migrationInfo)
        {
            var remoteMigration = CreateWcfInterface();
            try
            {
                return remoteMigration.GetMigrationScript(new KS.Core.DevelopServer.MigrationInfo()
                {
                    ContextAssemblyName = migrationInfo.ContextAssemblyName,
                    TargetName = migrationInfo.TargetName,
                    Connection = migrationInfo.Connection,
                    Language = migrationInfo.Language == GlobalVarioable.SourceType.Csharp
                          ? CSharp
                          : VisualBasic,
                    AppDataPath = migrationInfo.AppDataPath,
                    ConfigurationTypeName = migrationInfo.ConfigurationTypeName,
                    ContextAssemblyPath = migrationInfo.ContextAssemblyPath,
                    ContextAssemblyRootNameSpace = migrationInfo.ContextAssemblyRootNameSpace,
                    Force = migrationInfo.Force,
                    NameSpaceQualifiedConnectionType = migrationInfo.NameSpaceQualifiedConnectionType,
                    SourceName = migrationInfo.SourceName,
                    WebConfigPath = migrationInfo.WebConfigPath
                });
            }
            catch (FaultException<DevelopServiceException> ex)
            {
                throw new KhodkarInvalidException(ex.Detail.ErrorDetails);
            }
            catch (Exception ex)
            {
                throw new DevelopServerException(ex.Message);
            }
        }

        private IDevlopServer CreateWcfInterface()

        {
            try
            {

                //create the binding

                var binding = new BasicHttpBinding
                {
                    Security =
                    {
                        Mode = BasicHttpSecurityMode.None,
                        Transport = {ClientCredentialType = HttpClientCredentialType.None}
                    },
                    CloseTimeout = TimeSpan.Parse("00:05:00"),
                    SendTimeout = TimeSpan.Parse("00:05:00")
                };

                //configure the binding

                //if your wcf service is windows authenticate uncomment below line

                //binding.Security.Mode = BasicHttpSecurityMode.TransportCredentialOnly;

                //binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Ntlm;



                //if (you want set proxy for connection) uncomment below line

                //binding.ProxyAddress = new Uri(("http://{0}:{1}", "samplePoroxy.yourDomain.com", "port"));

                //binding.BypassProxyOnLocal = true;

                //binding.UseDefaultWebProxy = false;



                binding.MaxBufferSize = 999999999;

                binding.MaxReceivedMessageSize = 999999999;

                binding.ReaderQuotas.MaxArrayLength = 2147483647;

                binding.ReaderQuotas.MaxBytesPerRead = 2147483647;

                binding.ReaderQuotas.MaxDepth = 2147483647;

                binding.ReaderQuotas.MaxNameTableCharCount = 2147483647;

                binding.ReaderQuotas.MaxStringContentLength = 2147483647;

                var endpointAddress = new EndpointAddress("http://localhost:51249/DevlopService");

                var channelFactory = new ChannelFactory<IDevlopServer>(binding, endpointAddress);

                // if you need to windows auth by another user uncomment below line 

                // if (channelFactory.Credentials != null)

                //  channelFactory.Credentials.Windows.ClientCredential =

                //   new NetworkCredential("username", "password", "domain");

                //foreach (var operation in channelFactory.Endpoint.Contract.Operations)

                //{

                //    var behavior = operation.Behaviors.Find<DataContractSerializerOperationBehavior>();

                //    if (behavior != null)

                //    {

                //        behavior.MaxItemsInObjectGraph = 2147483647;

                //    }

                //}

                //create the channel

                return channelFactory.CreateChannel();
            }
            catch (FaultException<DevelopServiceException> ex)
            {
                throw new KhodkarInvalidException(ex.Detail.ErrorDetails);
            }
            catch (Exception ex)
            {
                throw new DevelopServerException(ex.Message);
            }

        }
    }
}
