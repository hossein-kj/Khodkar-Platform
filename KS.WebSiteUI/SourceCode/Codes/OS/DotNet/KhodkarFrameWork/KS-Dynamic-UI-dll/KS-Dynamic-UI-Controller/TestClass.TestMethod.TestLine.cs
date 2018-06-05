 /*khodkar c# comment        //tets line
 
var service = CreateWCFInterface();

 var x = service.DoWork(1,2);

//you Must Refrence KS.Core.dll
#if DEBUG
//check client login by debug mode
if (KS.Core.UI.Setting.Settings.IsDebugMode)
{
System.Diagnostics.Debugger.Break();
}
#endif


//you Must Refrence KS.Core.dll
#if DEBUG
//check client login by debug mode
if (KS.Core.UI.Setting.Settings.IsDebugMode)
{
//new Debugger
var debugger =
new KS.Core.CodeManager.Os.DotNet.Debugger(new KS.Core.FileSystemProvide.FileSystemManager());
//log sample Object
var addedDebugInfo = debugger.AddOrUpdateDebugInfo(new KS.Core.Model.Develop.DebugInfo()
{ CodeId = 797, Data = "سلام", IntegerValue = 1 }, "@asCodePath");
//read debug by debugId
var readedDebugInfo = debugger.GetDebugInfo(addedDebugInfo.Id, "@asCodePath");
//get all debugs
var allDebugsOfCodeId = debugger.GetDebugInfos("@asCodePath", 797);
//you can use linq query on allDebugsOfCodeId
 var selectedDebug = allDebugsOfCodeId.Where(dbg => dbg.IntegerValue == 1).FirstOrDefault();
}
#endif





 
 #if DEBUG
    return "دیباگ"; 
#else
  return x.ToString();
#endif
                 khodkar c# comment*/ 