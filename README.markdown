# ServiceStack.MsgPack

A simple way to add support for MessagePack protocol in your ServiceStack application.

## How?

* Include the assembly in your project.
* Go to your Configure method in your AppHost class
* Add the following code

```csharp
    MessagePackFormat.Register(this);
```
