 /*khodkar c# comment           //testclass
  public class TestController : BasePublicWebApiController
    {
      @TestMethod@
      
 
//you must refrence System.dll and System.ServiceModel.dll and System.Runtime.Serialization.dll

//you must use System.Net; and System.ServiceModel; and System.ServiceModel.Description;

//replace WCFInterface by your wcf Interface

public KS.Dynamic.WebService.TestWcf.ISrvTestWcf CreateWCFInterface()

{

//create the binding

 var binding = new BasicHttpBinding();

//configure the binding

//if your wcf service is windows authenticate uncomment below line

//binding.Security.Mode = BasicHttpSecurityMode.TransportCredentialOnly;

//binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Ntlm;

binding.Security.Mode = BasicHttpSecurityMode.None;

binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.None;

//if (you want set proxy for connection) uncomment below line

//binding.ProxyAddress = new Uri(string.Format("http://{0}:{1}", "samplePoroxy.yourDomain.com", "port"));

//binding.BypassProxyOnLocal = true;

//binding.UseDefaultWebProxy = false;

binding.CloseTimeout = TimeSpan.Parse("00:05:00");

binding.SendTimeout = TimeSpan.Parse("00:05:00");

binding.MaxBufferSize = 999999999;

binding.MaxReceivedMessageSize = 999999999;

binding.ReaderQuotas.MaxArrayLength = 2147483647;

binding.ReaderQuotas.MaxBytesPerRead = 2147483647;

binding.ReaderQuotas.MaxDepth = 2147483647;

binding.ReaderQuotas.MaxNameTableCharCount = 2147483647;

binding.ReaderQuotas.MaxStringContentLength = 2147483647;

var endpointAddress = new EndpointAddress("http://localhost:18591/SrvTestWcf.svc");

var channelFactory = new ChannelFactory<KS.Dynamic.WebService.TestWcf.ISrvTestWcf>(binding, endpointAddress);

// if you need to windows auth by another user uncomment below line 

// if (channelFactory.Credentials != null)

//  channelFactory.Credentials.Windows.ClientCredential =

//   new NetworkCredential("username", "password", "domain");

foreach (var operation in channelFactory.Endpoint.Contract.Operations)

{

var behavior = operation.Behaviors.Find<DataContractSerializerOperationBehavior>();

if (behavior != null)

{

 behavior.MaxItemsInObjectGraph = 2147483647;

}

}

//create the channel

return channelFactory.CreateChannel();

}




    }          khodkar c# comment*/ 