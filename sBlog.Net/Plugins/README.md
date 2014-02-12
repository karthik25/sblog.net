# sBlog.Net Plugins

The folder is to hold the plugins that you want sBlog.Net to process! sBlog.Net uses Microsoft's Managed Extensibility Frameworks (MEF) to 
identify and load plugins.

To create a plugin create a c# project, add a reference to sBlog.Net.Extensibility, and add a C# file. For example:

```csharp
[Export(typeof(IPlugin))]
public class PostViewCountPlugin : IPostPlugin
{

	// ...

}
```

In the above snippet, <code>IPostPlugin</code> indicates that this
plugin will deal with an individual post and the <code>Export</code> attribute with the type passed as <code>IPlugin</code> indicates
that this is a sBlog.Net plugin. Implement the interface and finish coding. After you do a build, drop-in the <code>.dll</code> file 
in this folder, restart sBlog.Net, you are all set!

This is just to give a high level idea. More information follows soon!