# Memory Leak
### Subscribing to Events
```
public class MyClass
{
    public MyClass(WiFiManager wiFiManager)
    {
        wiFiManager.WiFiSignalChanged += OnWiFiChanged;
    }
 
    private void OnWiFiChanged(object sender, WifiEventArgs e)
    {
        // do something
    }
}
```
Assuming the wifiManager outlives MyClass, you have a memory leak on your hands.
Any instance of MyClass is referenced by wifiManager and will never be allocated by the garbage collector.

### Capturing members in anonymous methods
```
public class MyClass
{
    private JobQueue _jobQueue;
    private int _id;
 
    public MyClass(JobQueue jobQueue)
    {
        _jobQueue = jobQueue;
    }
 
    public void Foo()
    {
        _jobQueue.EnqueueJob(() =>
        {
            Logger.Log($"Executing job with ID {_id}");
            // do stuff 
        });
    }
}
```
In this code, the member _id is captured in the anonymous method and as a result the instance is referenced as well. 
This means that while JobQueue exists and references that job delegate, it will also reference an instance of MyClass.

The solution can be quite simple – assigning a local variable:
```
public class MyClass
{
    public MyClass(JobQueue jobQueue)
    {
        _jobQueue = jobQueue;
    }
    private JobQueue _jobQueue;
    private int _id;
 
    public void Foo()
    {
        var localId = _id;
        _jobQueue.EnqueueJob(() =>
        {
            Logger.Log($"Executing job with ID {localId}");
            // do stuff 
        });
    }
}
```
### Static Variables
public class MyClass
{
    static List<MyClass> _instances = new List<MyClass>();
    public MyClass()
    {
        _instances.Add(this);
    }
}

### Caching functionality
```
public class ProfilePicExtractor
{
    private Dictionary<int, byte[]> PictureCache { get; set; } = 
      new Dictionary<int, byte[]>();
 
    public byte[] GetProfilePicByID(int id)
    {
        // A lock mechanism should be added here, but let's stay on point
        if (!PictureCache.ContainsKey(id))
        {
            var picture = GetPictureFromDatabase(id);
            PictureCache[id] = picture;
        }
        return PictureCache[id];
    }
 
    private byte[] GetPictureFromDatabase(int id)
    {
        // ...
    }
}
```
You can do several things to solve this:
* Delete caching that wasn’t used for some time
* Limit caching size
* Use WeakReference to hold cached objects. This relies on the garbage collector to decide when to clear the cache, but might not be such a bad idea.
The GC will promote objects that are still in use to higher generations in order to keep them longer. 
hat means that objects that are used often will stay longer in cache.

### Threads that Never Terminate
```
public class MyClass
{
    public MyClass()
    {
        Timer timer = new Timer(HandleTick);
        timer.Change(TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(5));
    }
 
    private void HandleTick(object state)
    {
        // do something
    }
```

### Not de-allocating unmanaged memory
```
public class SomeClass
{
    private IntPtr _buffer;
 
    public SomeClass()
    {
        _buffer = Marshal.AllocHGlobal(1000);
    }
 
    // do stuff without freeing the buffer memory
 
}
```
To deal with such issues you can add a Dispose method that frees any unmanaged resources.
```
public class SomeClass : IDisposable
{
    private IntPtr _buffer;
 
    public SomeClass()
    {
        _buffer = Marshal.AllocHGlobal(1000);
        // do stuff without freeing the buffer memory
    }
 
    public void Dispose()
    {
        Marshal.FreeHGlobal(_buffer);
    }
}
```
### Adding Dispose without Calling it
One thing you can do is to use the using statement in C#, also you can add System.Diagnostics.Debug.Assert or a logging framework of
some sort to finilizer method and find classes which go to finilizer stage.
```
public class SomeClass : IDisposable
{
    private IntPtr _buffer;
 
    public SomeClass()
    {
        _buffer = Marshal.AllocHGlobal(1000);
        // do stuff without freeing the buffer memory
    }
    
    ~SomeClass() {
        Debug.Assert(false, "Do not rely on finalization! Use Dispose!");
        Dispose();
    }
    
    public void Dispose()
    {
        Marshal.FreeHGlobal(_buffer);
        GC.SuppressFinalize(this);
    }
}
```

# Resources
* [ways-to-cause-memory-leaks-in-dotnet](https://michaelscodingspot.com/ways-to-cause-memory-leaks-in-dotnet/)
