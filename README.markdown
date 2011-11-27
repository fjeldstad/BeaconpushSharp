BeaconpushSharp is a C# wrapper for the [Beaconpush REST API](http://beaconpush.com/guide/rest-api/).

Examples
--------

    // Create a Beaconpush client
    var beacon = new Beacon("myApiKey", "mySecretKey");
    
    // Enumerate through all the users in a channel
    User[] users = beacon.Channel("myChannel").Users();
    foreach (var user in users) 
    {
        Console.WriteLine(user.Username);
    }
    
    // Send a message to a channel
    beacon.Channel("myChannel").Send(new 
    { 
        someMessage = "Hello again!",
        awesomenessFactor = 1.0,
        realtimeMagic = true
    });
    
    // Get the total number of online users
    long userCount = beacon.OnlineUserCount();
    
    // Check if a user is online
    bool userIsOnline = beacon.User("someUser").IsOnline();
    
    // Force sign-out for a specific user
    beacon.User("someUnfortunateUser").ForceSignOut();
    
    // Send a message to a user
    beacon.User("luckyWinner").Send(new 
    {
        amount = 1000,
        currency = "EUR"
    });
    
    