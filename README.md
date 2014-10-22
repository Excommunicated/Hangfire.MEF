Hangfire.MEF
============

[Hangfire](http://hangfire.io) background job activator based on 
[MEF](http://msdn.microsoft.com/en-us/library/dd460648(v=vs.110).aspx) IoC Container. It allows you to use instance
methods of classes that define parametrised constructors:

```csharp
[Export]
public class EmailService
{
	private DbContext _context;
    private IEmailSender _sender;
	
	[ImportingConstructor]
	public EmailService(DbContext context, IEmailSender sender)
	{
		_context = context;
		_sender = sender;
	}
	
	public void Send(int userId, string message)
	{
		var user = _context.Users.Get(userId);
		_sender.Send(user.Email, message);
	}
}	

// Somewhere in the code
BackgroundJob.Enqueue<EmailService>(x => x.Send(1, "Hello, world!"));
```

Improve the testability of your jobs without static factories!

Installation
--------------

Hangfire.MEF is available as a NuGet Package. Type the following
command into NuGet Package Manager Console window to install it:

```
Install-Package Hangfire.MEF
```

Usage
------

The package provides an extension method for [OWIN bootstrapper](http://docs.hangfire.io/en/latest/users-guide/getting-started/owin-bootstrapper.html):

```csharp
AggregateCatalog catalog = new AggregateCatalog();
catalog.Catalogs.Add(new AssemblyCatalog(Assembly.GetExecutingAssembly()));
// add other assembly, directory, aggregate catalogs etc. here
var container = new CompositionContainer(catalog);

app.UseHangfire(config =>
{
    config.UseMEFActivator(container);
});
```

In order to use the library outside of web application, set the static `JobActivator.Current` property:

```csharp
var container = new CompositionContainer();
JobActivator.Current = new MEFJobActivator(container);
```

HTTP Request warnings
-----------------------

Services registered with `InRequestScope()` directive **will be unavailable**
during job activation, you should re-register these services without this
hint.

`HttpContext.Current` is also **not available** during the job performance. 
Don't use it!

Credits
--------

This project and this readme is based on the [Hangfire.Ninject](https://github.com/HangfireIO/Hangfire.Ninject) 
project by [Sergey Odinokov](https://github.com/odinserj) Many thanks go out to him for his great work on the
 [Hangfire](http://hangfire.io) project as well.
