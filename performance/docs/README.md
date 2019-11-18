# Performance optimization
#### Approach
* Non-functional requirements
* Performance reflects in architecture
* Develop with real data
* Avoid premature and micro optimization
* Environment - maximum close to production or better to use production
* Test data - real data
* Build Configuration - Release
* Performance benchmarking tools (Load UI, Apache, JMeter)

#### Tools
##### Visual Studio Performance Profiler
* https://msdn.microsoft.com/en-us/library/ms182372.aspx
* https://msdn.microsoft.com/en-us/library/mt210448.aspx
* https://docs.microsoft.com/en-us/visualstudio/profiling/profiling-feature-tour
* https://channel9.msdn.com/Shows/Visual-Studio-Toolbox/Visual-Studio-Toolbox-Performance-Profiling
##### JetBrains dotTrace
* https://www.jetbrains.com/profiler/documentation/documentation.html
##### Redgate ANTS
* http://www.red-gate.com/products/dotnet-development/ants-performance-profiler/


#### BOTTLENECKS IDENTIFICATION

* Sub-Optimal Design
* Resources usage   
  CPU   
  Memory     
  IO: File System   
  IO: Network   
* Tools   
  Task Manager    
  Resource Monitor    
  Performance Monitor   
* Server Monitoring   
  Application Insights    
  New Relic   
  Custom Logging    
  Etc.
  
#### PERFORMANCE. TIPS AND TRICKS
* Logical Problems / Not optimal code
* Data Structure misuse – IEnumerable, List, Array, Dictionary, HashSet
* Cache – cached repositories, etc.
* Concurrent or Asynchronous code – async/await, TPL, Parallel.For
* Use StringBuilder
* Avoid Exceptions
* Use XML Readers instead of LINQ to XML
* Avoid Reflection/dynamic
* Use memoization – if(lookup.TryGetValue(key, out value)) return value; lookup[key] = value.ToLower();
* Override GetHashCode and Equals
* Use **as** operator instead **is**
* Use **ref** or **out** when you need copy structure by reference
* Reduce methods size
* Prefer local variables over fields
* Use static readonly fields and constants
* Use static methods and fields
* Dictonary order, SortedDictionary
* Make classes closed using paramater **sealed**

#### Performance Resources
* https://www.pluralsight.com/courses/measuring-dotnet-performance
* https://www.pluralsight.com/courses/dotnet-performance-optimization-profiling-jetbrains-dottrace
* .Net Performance Testing and Optimization - https://www.red-gate.com/library/net-performance-testing-and-optimization-the-complete-guide
* Pro .NET Performance: Optimize Your C# Applications - http://www.apress.com/us/book/9781430244585
* Writing High-Performance .NET Code - http://www.writinghighperf.net/
* Advanced .NET Debugging - http://advanceddotnetdebugging.com/
* .Net Internals and Advanced Debugging Techniques - book  https://www.amazon.ca/Net-Internals-Advanced-Debugging-Techniques/dp/0321934717  or course https://www.pluralsight.com/courses/dotnet-internals-adv-debug
* Performance Tips - https://msdn.microsoft.com/en-us/library/ms973839.aspx
* 52 Perf Tricks - https://www.red-gate.com/library/52-tips-tricks-to-boost-net-performance 
* Optimization - https://www.dotnetperls.com/optimization


# Memory optimization
#### Tools
##### Visual Studio Managed Memory Debugger
* https://msdn.microsoft.com/en-us/library/d5zhxt22.aspx
* https://blogs.msdn.microsoft.com/devops/2013/10/16/net-memory-analysis-enhancements-in-visual-studio-2013/
* https://channel9.msdn.com/Shows/Visual-Studio-Toolbox/Managed-Memory-Analysis-in-Visual-Studio-2013
##### Visual Studio Diagnostic Tools
* https://docs.microsoft.com/en-us/visualstudio/profiling/memory-usage
* https://channel9.msdn.com/Shows/Visual-Studio-Toolbox/Visual-Studio-2015-Diagnostic-Tools
* JetBrains dotMemory
##### https://www.jetbrains.com/dotmemory/
* Redgate ANTS Memory Profiler
* http://www.red-gate.com/products/dotnet-development/ants-memory-profiler/
* Scitech .NET Memory Profiler
* https://memprofiler.com/
##### Concurrency Visualizer for Visual Studio 2017
* Look how often GC runned.
##### PerfView

#### Memory Tips and Tricks
* Files, Connections, etc. – IDisposable – Dispose(), Close(), using
* Clear events subscriptions – if you see +=, then there must be -=
* Use StringBuilder
* Static fields/properties
* Define collections capacity – var list = new List(products.Length);
* Use structs
* Do not call GC.Collect() explicitly
* Boxing/Unboxing 
* Use WeekReferences, ConditionalWeakTable

#### Resources
* Pluralsight\idisposable-best-practices-csharp-developers
* Pluralsight\making-dotnet-applications-even-faster
* Under the Hood of .NET Memory Management - https://www.amazon.com/Under-Hood-NET-Memory-Management/dp/1906434751

# Database
#### Tools
##### MS SQL Profiler
* https://docs.microsoft.com/en-us/sql/tools/sql-server-profiler/sql-server-profiler
* https://www.red-gate.com/simple-talk/sql/performance/how-to-identify-slow-running-queries-with-sql-profiler/
* https://www.mssqltips.com/sqlservertutorial/272/profiler-and-server-side-traces/

##### MS SQL Management Studio
* https://technet.microsoft.com/en-us/library/ms191227(v=sql.105).aspx
* https://www.mssqltips.com/sqlservertip/2170/more-intuitive-tool-for-reading-sql-server-execution-plans/
* http://sqlmag.com/t-sql/understanding-query-plans

##### Redgate SQL Monitor
* http://www.red-gate.com/products/dba/sql-monitor/

#### Tips and Tricks
* Reduce number of queries
* Use Indexes
* Choose proper transaction isolation level
* Retrieve only needed records - return context.Products.ToList().FirstOrDefault(); 
* Don’t select unneeded columns – SELECT * FROM Products
* Entity Framework – rewrite with stored procedures
* Bulk Operations – use SQLBulkCopy class
* Use Cache
* Use IQueryable
* Do not track changes – AutoDetectChangesEnabled; db.Products.Where(p => p.InStock). AsNoTracking().ToList(); 
* Define length for nvarchar columns
* Seek vs Scan, avoid functions in WHERE
* Estimated vs Actual Query Plan
* Update Statistics
* Parameters Sniffing – local variables, OPTION (RECOMPILE)
* Avoid transactions
* Avoid cursors
* Normalization\Denormalization
* Partitioning

#### Resources
* Pluralsight\sqlserver-sqltrace
* Pluralsight\sqlserver-query-plan-analysis
* https://www.mssqltips.com/sql-server-tip-category/9/performance-tuning/
* http://download.red-gate.com/ebooks/SQL/sql-server-execution-plans.pdf

# Web optimization
#### Resources
* Pluralsight\web-performance
* https://www.udacity.com/course/website-performance-optimization--ud884
* https://developers.google.com/web/fundamentals/performance/
* https://dou.ua/lenta/digests/wpo-digest-0/
* https://www.keycdn.com/blog/website-performance-optimization/
* https://medium.com/airbnb-engineering/performance-tuning-e10ac94916df

### Tools
* Fiddler - http://www.telerik.com/fiddler
* https://www.keycdn.com/blog/website-speed-test-tools/
* http://www.softwaretestinghelp.com/performance-testing-tools-load-testing-tools/







