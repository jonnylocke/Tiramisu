Setup
-----

To run the application please follow these steps: -

1) 
 - Go to Computer Management on you PC
 - Look in Services and Applications and see if 'Message Queuing' is there and within there 'Private Queuing'. 
 
 If this does not exist, please follow the short steps in this article in order to install MSMQ (may require a restart)
 - https://msdn.microsoft.com/en-us/library/aa967729(v=vs.110).aspx

    for example on Windows 10 I selected to install
     - MSMQ Active Directory Domain Services Integration (for computers joined to a Domain).
     - MSMQ HTTP Support.

 - Check Computer Management again to check MSMQ is installed

 - Ensure you have .Net 4.6.1 installed

 2) 
  - Unzip the the zip file to a folder destination c:\app\Tiramisu
  - Open the Visual Studio Solution (the solutin was created with Visual Studio 2017 Community Edition, however, VS2015 or better should work, lower than VS2015 will cause database issues), 
    accept the prompt about opening projects from unknown sources
  - Run a ReBuild on the whole solution (ensure you have an internet connection)
  - Open a Command Prompt with Administrator rights
  - In the command prompt change to the directory for the project called  'Restaurants.Corp.Dal' 
  - Run the 'Setup.bat' file and wait for this to finish running
        - this should run the database migration

3) 
  Serveral projects need to run all together, please check the right ones are set to start: -

 - In Visual Studio: 
  - Right click on the "Solution 'Tiramisu' (13 Projects)" and select 'Set Startup Projects...'
  - In the dialogue box, check that the select the 'Multiple startup projects:' option is selected
  - Check that the following projects are set to start, if they are not please set them to 'Start'
    - Tiramisu
    - Restaurant.Corp.Public.WebApi
    - RestaurantX.Web
    - Restaurant.Corp.Reports
    - Restaurants.Corp.Domain.Orders
    - Restaurants.Corp.Domain.StockOrders
    - Stock.Supplier.Dispatcher

  - NOTE: ENSURE THE PROJECT 'Tiramisu' IS THE FIRST ONE THAT STARTS SO MOVE THIS TO THE TOP OF THE LIST

4)
  - Now run the solution from the Start button


Using the Applications
----------------------

Use the 'RestaurantX.Web' to submit orders (every 5 orders representers 1 month of orders)
After each order is placed, you can go to the 'Reports' section in the 'Restaurant.Corp.Reports' website
 - this will show the number of eggs to order from the suppliers for each order placed

After every 5 orders placed, you can go to the 'Reports' section in the 'RestaurantX' website and it will show you total number of eggs that will be sent to the restaurant in the next shipment




Decisions and assumptions
-------------------------

In order to use some messaging but at the same time have something that can run locally (all within VS) with a low and easy amount of setup, I decided to use MSMQ. 
Due to this there is no proper Pub/Sub at work. However, instead I am sending to queues and created simple listeners to receive messsages from given queues just to show the concepts.

Again to allow everything to run locally for the data that show in the reports, I'm simply writing the data out to json files which can be read from.

The services running have been created using TopShelf which in a real scenario can scaled as needed.

There is no proper error handling in play, which would obviously be the case in a real scenario

There are some unit tests to check the logic of the amount of eggs ordered and sent. All tests should all pass.